using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform _markerTipHolder;
    [SerializeField] private Transform _markerTip;
    [SerializeField] private int _tipSize = 10;
    [SerializeField] private float _tipHeight = 0.1f;
    private Renderer _renderer;
    private Color[] _colors;
    private Whiteboard _whiteboard;
    private Vector2 _lastHitPosition;
    private bool _hitLastFrame = false;
    private Quaternion _lastHitRotation = Quaternion.identity;
    void Start()
    {
        _colors = new Color[_tipSize * _tipSize];
        _renderer = _markerTip.GetComponent<Renderer>();
        for (int i = 0; i < _tipSize * _tipSize; i++)
        {
            _colors[i] = _renderer.material.color;
        }
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(_markerTipHolder.position, Vector3.forward, out RaycastHit hit, _tipHeight))
        {
            if (hit.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null) _whiteboard = hit.transform.GetComponent<Whiteboard>();

                Vector2 _hitPosition = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

                int xPos = (int)(_hitPosition.x * _whiteboard.whiteboardSize.x - _tipSize / 2);
                int yPos = (int)(_hitPosition.y * _whiteboard.whiteboardSize.y - _tipSize / 2);

                if (xPos < 0 || yPos < 0 || xPos > _whiteboard.whiteboardSize.x || yPos > _whiteboard.whiteboardSize.y) return;
                try
                {
                    if (_hitLastFrame)
                    {
                        _whiteboard.whiteboardTexture.SetPixels(xPos, yPos, _tipSize, _tipSize, _colors);

                        for (float increment = 0.05f; increment < 1.00f; increment += 0.05f)
                        {
                            int lerpX = (int)Mathf.Lerp(_lastHitPosition.x, xPos, increment);
                            int lerpY = (int)Mathf.Lerp(_lastHitPosition.y, yPos, increment);
                            _whiteboard.whiteboardTexture.SetPixels(lerpX, lerpY, _tipSize, _tipSize, _colors);
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
