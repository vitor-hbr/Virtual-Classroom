using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    [SerializeField] private PhotonView photonView;
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;
    [SerializeField] GameObject _penPrefab;
    [SerializeField] GameObject _eraserPrefab;
    [SerializeField] public List<GameObject> Pens;
    [SerializeField] public List<GameObject> Erasers;
    private Vector3 _hiddenPosition = new Vector3(0, -10, 0);
    private GestureObject _leftObject = GestureObject.undefined;
    private GestureObject _rightObject = GestureObject.undefined;
    private Vector3 penLeftCorrectPosition = new Vector3(0.004f, 0.0296f, -0.0252f);
    private Vector3 penRightCorrectPosition = new Vector3(-0.104f, -0.0296f, -0.0252f);
    void Start()
    {
        if (PhotonNetwork.IsMasterClient && photonView.IsMine)
        {
            Pens = new List<GameObject>();
            Erasers = new List<GameObject>();

            for (int i = 0; i < 2; i++)
            {
                Pens.Add(PhotonNetwork.Instantiate("Pen", new Vector3(0, 0, 0), Quaternion.identity));
                Erasers.Add(PhotonNetwork.Instantiate("Eraser", new Vector3(0, 0, 0), Quaternion.identity));
            }
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_leftObject == GestureObject.pen)
            {
                Pens[0].transform.position = _leftHand.position + penLeftCorrectPosition;
                Pens[0].transform.rotation = _leftHand.rotation;
                Pens[0].transform.Rotate(180f, 0f, 0f, Space.Self);
            }
            else if (_leftObject == GestureObject.eraser)
            {
                Erasers[0].transform.position = _leftHand.position;
            }
            else
            {
                Pens[0].transform.position = _hiddenPosition;
                Erasers[0].transform.position = _hiddenPosition;
            }

            if (_rightObject == GestureObject.pen)
            {
                Pens[1].transform.position = _rightHand.position + penRightCorrectPosition;
                Pens[1].transform.rotation = _rightHand.rotation;
            }
            else if (_rightObject == GestureObject.eraser)
            {
                Erasers[1].transform.position = _rightHand.position;
            }
            else
            {
                Pens[1].transform.position = _hiddenPosition;
                Erasers[1].transform.position = _hiddenPosition;
            }
        }

    }

    public void PlaceObjectOnHand(GestureObject gestureObject, Hand hand)
    {
        if (hand == Hand.Left)
        {
            _leftObject = gestureObject;
        }
        else
        {
            _rightObject = gestureObject;
        }
    }
}
