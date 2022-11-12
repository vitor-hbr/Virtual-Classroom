using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonViewAdjust : MonoBehaviour
{
    public List<GameObject> GameObjectsToDisable;
    public List<Behaviour> ComponentsToDisable;
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
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
