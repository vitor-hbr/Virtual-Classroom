using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D whiteboardTexture;
    public Vector2 whiteboardSize = new Vector2(2048, 2048);

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        whiteboardTexture = new Texture2D((int)whiteboardSize.x, (int)whiteboardSize.y);
        renderer.material.mainTexture = whiteboardTexture;
    }
}
