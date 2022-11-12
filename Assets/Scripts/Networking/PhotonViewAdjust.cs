using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonViewAdjust : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    public List<GameObject> GameObjectsToDisable;
    public List<Behaviour> ComponentsToDisable;
    void Awake()
    {
        if (!photonView.IsMine)
        {
            disableNonMasterItems();
        }
    }

    void disableNonMasterItems()
    {
        print("Disabling non master items");
        foreach (Behaviour component in ComponentsToDisable)
        {
            component.enabled = false;
        }

        foreach (GameObject gameObject in GameObjectsToDisable)
        {
            gameObject.SetActive(false);
        }
    }
}
