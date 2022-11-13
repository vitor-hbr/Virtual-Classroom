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
    public List<GameObject> MyGameObjectsToEnable;
    public List<Behaviour> MyComponentsToEnable;
    public List<GameObject> OthersGameObjectsToEnable;
    public List<Behaviour> OthersComponentsToEnable;
    void Awake()
    {
        if (photonView.IsMine)
        {
            disableMyItems();
            enableMyItems();
        }
        else
        {
            disableOtherPlayersItems();
            enableOtherPlayersItems();
        }

    }

    void enableMyItems()
    {
        foreach (GameObject item in MyGameObjectsToEnable)
        {
            item.SetActive(true);
        }
        foreach (Behaviour item in MyComponentsToEnable)
        {
            item.enabled = true;
        }
    }

    void enableOtherPlayersItems()
    {
        foreach (GameObject item in OthersGameObjectsToEnable)
        {
            item.SetActive(true);
        }
        foreach (Behaviour item in OthersComponentsToEnable)
        {
            item.enabled = true;
        }
    }

    void disableMyItems()
    {
        print("Disabling my items");
        foreach (Behaviour component in MyComponentsToDisable)
        {
            if (component != null)
            {
                component.enabled = false;
            }
        }

        foreach (GameObject gameObject in MyGameObjectsToDisable)
        {
            if (gameObject != null)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
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
                component.enabled = false;
            }
        }

        foreach (GameObject gameObject in OthersGameObjectsToDisable)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
