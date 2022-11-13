using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject endPosition;
    public Transform cameraTransform;

    private float time;
    private Vector3 initialPosition;
    private Vector3 deltaPosition;

    void Start()
    {
        initialPosition = cameraTransform.position;
        deltaPosition = endPosition.transform.position - cameraTransform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            float speed = curve.Evaluate(time);
            cameraTransform.localPosition = (deltaPosition * speed) + initialPosition;
            time += Time.deltaTime;
        }
    }
}
