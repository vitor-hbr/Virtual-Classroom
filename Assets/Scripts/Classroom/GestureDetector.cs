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
    private bool _gettingBones = false;
    private bool _gettingGesture = false;
    private float _getBonesDelay = 2f;
    private float _gestureDelay = 0.2f;
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

            if (_gettingGesture)
            {
                return;
            }

            Gesture currentGesture = checkForGesture();

            if (_executingGesture == currentGesture.name) return;

            StartCoroutine(getGesture(currentGesture.name));
        }
        else
        {
            if (!_gettingBones) StartCoroutine(getBones());
        }
    }
    IEnumerator getBones()
    {
        _gettingBones = true;
        yield return new WaitForSeconds(_getBonesDelay);
        fingerBones = new List<OVRBone>(skeleton.Bones);
        if (fingerBones.Count > 0) _gotBones = true;
        _gettingBones = false;
    }

    IEnumerator getGesture(GestureObject newGestureName)
    {
        _gettingGesture = true;
        yield return new WaitForSeconds(_gestureDelay);
        Gesture currentGesture = checkForGesture();
        if (newGestureName == currentGesture.name)
        {
            gestureHandler.PlaceObjectOnHand(currentGesture.name, _hand);
            _executingGesture = currentGesture.name;
        }
        _gettingGesture = false;
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
        if (lowestMagnitude >= gestureThreshold || lowestMagnitude == 0)
        {
            closestMatch.name = GestureObject.undefined;
        }
        return closestMatch;
    }
}