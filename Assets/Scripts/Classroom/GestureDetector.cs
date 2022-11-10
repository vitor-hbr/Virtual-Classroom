using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
    public UnityEvent onNotRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public List<OVRBone> fingerBones;
    public bool debugMode = false;
    public float gestureThreshold = 0.142f;
    private bool _gotBones = false;
    private bool _executedGesture = false;

    void Start()
    {
        StartCoroutine(getBones());
    }
    void Update()
    {
        if (_gotBones)
        {
            if (Input.GetKeyDown(KeyCode.S) && debugMode)
            {
                print("Saving Gesture");
                saveGesture();
            }
            Gesture currentGesture = checkForGesture();

            if (currentGesture.name != null)
            {
                currentGesture.onRecognized?.Invoke();
                _executedGesture = true;
            }
            else
            {
                currentGesture.onNotRecognized?.Invoke();
                _executedGesture = false;
            }
        }
    }
    IEnumerator getBones()
    {
        yield return new WaitForSeconds(2);
        _gotBones = true;
        fingerBones = new List<OVRBone>(skeleton.Bones);
    }
    void saveGesture()
    {
        Gesture newGesture = new Gesture();
        newGesture.name = "New Gesture";
        newGesture.fingerData = new List<Vector3>();
        foreach (OVRBone finger in fingerBones)
        {
            newGesture.fingerData.Add(skeleton.transform.InverseTransformPoint(finger.Transform.position));
        }
        gestures.Add(newGesture);
    }

    Gesture checkForGesture()
    {
        Gesture closestMatch = new Gesture();
        float lowestMagnitude = gestureThreshold;
        foreach (Gesture gesture in gestures)
        {
            float totalMagnitude = 0;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                float magnitude = (gesture.fingerData[i] - skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position)).sqrMagnitude;
                totalMagnitude += magnitude;
            }
            if (totalMagnitude < lowestMagnitude && totalMagnitude < gestureThreshold * gestureThreshold)
            {
                lowestMagnitude = totalMagnitude;
                closestMatch = gesture;
            }
        }
        return closestMatch;
    }
}
