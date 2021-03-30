using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject itemOriginal;
    public GameObject itemCopy;
    public void TeleportItem(GameObject item)
    {
        item.transform.position = spawnPoint.transform.position;
        if (item == itemOriginal)
            itemCopy.SetActive(true);
        else
            itemOriginal.SetActive(true);
        item.GetComponent<InteractableObject>().Renew();
    }
}
