using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardEraser : MonoBehaviour
{
    [SerializeField] private Transform _eraserHolder;
    [SerializeField] private int _eraserLength = 1170;
    [SerializeField] private int _eraserWidth = 270;

    private Color[] _colors;
    private float _effectiveDistance = 0.1f;
    private Whiteboard _whiteboard;
    private Vector2 _lastHitPosition;
    private bool _hitLastFrame = false;
    private Quaternion _lastHitRotation = Quaternion.identity;
    void Start()
    {
        _colors = new Color[_eraserLength * _eraserWidth];
        Color white = new Color(1, 1, 1, 0);

        for (int i = 0; i < _eraserLength * _eraserWidth; i++)
        {
            _colors[i] = white;
        }
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(_eraserHolder.position, Vector3.forward, out RaycastHit hit, _effectiveDistance))
        {
            if (hit.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null) _whiteboard = hit.transform.GetComponent<Whiteboard>();

                Vector2 _hitPosition = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

                int xPos = (int)(_hitPosition.x * _whiteboard.whiteboardSize.x - _eraserWidth / 2);
                int yPos = (int)(_hitPosition.y * _whiteboard.whiteboardSize.y - _eraserLength / 2);

                if (xPos < 0 || yPos < 0 || xPos > _whiteboard.whiteboardSize.x || yPos > _whiteboard.whiteboardSize.y) return;

                try
                {
                    if (_hitLastFrame)
                    {
                        _whiteboard.whiteboardTexture.SetPixels(xPos, yPos, _eraserWidth, _eraserLength, _colors);

                        for (float increment = 0.05f; increment < 1.00f; increment += 0.05f)
                        {
                            int lerpX = (int)Mathf.Lerp(_lastHitPosition.x, xPos, increment);
                            int lerpY = (int)Mathf.Lerp(_lastHitPosition.y, yPos, increment);
                            _whiteboard.whiteboardTexture.SetPixels(lerpX, lerpY, _eraserWidth, _eraserLength, _colors);
                        }

                        transform.rotation = _lastHitRotation;
                        _whiteboard.whiteboardTexture.Apply();
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }

                _lastHitPosition = new Vector2(xPos, yPos);
                _lastHitRotation = transform.rotation;
                _hitLastFrame = true;
                return;
            }
        }

        _hitLastFrame = false;
        _whiteboard = null;
    }
}
