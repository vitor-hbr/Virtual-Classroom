using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GestureObject
{
    eraser,
    pen,
    undefined
}

public enum Hand
{
    Left,
    Right
}

public class GestureHandler : MonoBehaviour
{
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;
    [SerializeField] GameObject _pen;
    [SerializeField] GameObject _eraser;
    private GameObject _leftObject;
    private GameObject _rightObject;
    private Vector3 penLeftCorrectPosition = new Vector3(-0.104f, -0.0296f, 0.0252f);
    private Vector3 penRightCorrectPosition = new Vector3(-0.104f, -0.0296f, -0.0252f);
    void Update()
    {
        if (_leftObject)
        {

            //_leftObject.transform.position = _leftHand.position;
        }
        if (_rightObject)
        {
            //_rightObject.transform.position = _rightHand.position;
        }
    }

    public void SpawnObject(GestureObject gestureObject, Hand hand)
    {
        if (hand == Hand.Left)
        {
            if (_leftObject != null) Destroy(_leftObject);
            _leftObject = Instantiate(gestureObject == GestureObject.eraser ? _eraser : _pen, penLeftCorrectPosition, Quaternion.identity, _leftHand);
            _leftObject.transform.Rotate(180f, 0f, 0f, Space.Self);
        }
        else
        {
            if (_rightObject != null) Destroy(_rightObject);
            _rightObject = Instantiate(gestureObject == GestureObject.eraser ? _eraser : _pen, penRightCorrectPosition, Quaternion.identity, _rightHand);
        }
    }

    public void RemoveObject(GestureObject gestureObject, Hand hand)
    {
        if (hand == Hand.Left)
        {
            if (_leftObject != null) Destroy(_leftObject);
        }
        else
        {
            if (_rightObject != null) Destroy(_rightObject);
        }
    }
}
