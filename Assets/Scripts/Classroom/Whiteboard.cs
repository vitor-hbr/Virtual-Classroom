using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D whiteboardTexture;
    public Vector2 whiteboardSize = new Vector2(2048, 2048);
    public Color boardColor = Color.white;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        whiteboardTexture = new Texture2D((int)whiteboardSize.x, (int)whiteboardSize.y);
        PaintWholeBoard();
        renderer.material.mainTexture = whiteboardTexture;
    }

    public void PaintWholeBoard()
    {
        for (int x = 0; x < whiteboardTexture.width; x++)
        {
            for (int y = 0; y < whiteboardTexture.height; y++)
            {
                whiteboardTexture.SetPixel(x, y, boardColor);
            }
        }
        whiteboardTexture.Apply();
    }
}
