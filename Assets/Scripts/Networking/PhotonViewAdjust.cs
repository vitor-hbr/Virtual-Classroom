using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonViewAdjust : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    public List<GameObject> MyGameObjectsToDisable;
    public List<Behaviour> MyComponentsToDisable;
    public List<GameObject> OthersGameObjectsToDisable;
    public List<Behaviour> OthersComponentsToDisable;
    void Awake()
    {
        if (photonView.IsMine)
        {
            disableMyItems();
        }
        else
        {
            disableOtherPlayersItems();
        }

    }

    void disableMyItems()
    {
        print("Disabling my items");
        foreach (Behaviour component in MyComponentsToDisable)
        {
            if (component != null)
            {
                Destroy(component);
            }
        }

        foreach (GameObject gameObject in MyGameObjectsToDisable)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    void disableOtherPlayersItems()
    {
        print("Disabling other players items");
        foreach (Behaviour component in OthersComponentsToDisable)
        {
            if (component != null)
            {
                Destroy(component);
            }
        }

        foreach (GameObject gameObject in OthersGameObjectsToDisable)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
