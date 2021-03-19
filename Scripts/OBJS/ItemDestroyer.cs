using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InteractableObject>() != null && other.GetComponent<Enemy>() == null)
        {
            FindObjectOfType<RoomBeam>().LetGo();
            other.GetComponent<InteractableObject>().Phase();
        }
    }
}
