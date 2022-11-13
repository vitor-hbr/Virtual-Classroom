using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorObjects : MonoBehaviour
{
    public List<GameObject> anchorObjects;
    public List<GameObject> targetObjects;
    void Update()
    {
        for (int i = 0; i < anchorObjects.Count; i++)
        {
            targetObjects[i].transform.position = anchorObjects[i].transform.position;
            targetObjects[i].transform.rotation = anchorObjects[i].transform.rotation;
        }
    }
}
