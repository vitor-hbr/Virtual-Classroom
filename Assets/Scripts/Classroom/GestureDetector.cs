using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public GestureObject name;
    public List<Vector3> fingerData;
}

public class GestureDetector : MonoBehaviour
{
    public GestureHandler gestureHandler;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public List<OVRBone> fingerBones;
    public bool debugMode = false;
    public float gestureThreshold = 0.07f;
    private bool _gotBones = false;
    private GestureObject _executingGesture = GestureObject.undefined;
    private Hand _hand;
    void Start()
    {
        _hand = gameObject.name.Split('-')[0] == "Left" ? Hand.Left : Hand.Right;
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

            if (currentGesture.name != GestureObject.undefined)
            {
                if (_executingGesture == currentGesture.name) return;

                gestureHandler.SpawnObject(currentGesture.name, _hand);
                _executingGesture = currentGesture.name;
            }
            else
            {
                gestureHandler.RemoveObject(currentGesture.name, _hand);
                _executingGesture = GestureObject.undefined;
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
        newGesture.name = GestureObject.undefined;
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
        if (debugMode) print("Closest Match: " + closestMatch.name + " with magnitude: " + lowestMagnitude);
        if (lowestMagnitude == gestureThreshold)
        {
            closestMatch.name = GestureObject.undefined;
        }
        return closestMatch;
    }
}
