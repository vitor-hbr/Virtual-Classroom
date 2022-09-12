using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    [SerializeField] bool debugCamera = false;
    void Start()
    {
        if (debugCamera) CreateDebugCamera();
    }

    void CreateDebugCamera()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        Camera newCamera = gameObject.AddComponent<Camera>();
        newCamera.fieldOfView = 85;
        gameObject.tag = "MainCamera";

        transform.position = new Vector3(0, 1.6f, 0);
    }
}
