using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHitBox : MonoBehaviour
{
    public List<GameObject> objsInRoom = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            other.GetComponent<Player>().inRoom = true;
        if (other.GetComponent<RoomChecker>() != null)
        {
            objsInRoom.Add(other.gameObject);
            other.GetComponent<RoomChecker>().inRoom = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
            other.GetComponent<Room>().CancelRoom();
        if (other.GetComponent<RoomChecker>() != null)
        {
            objsInRoom.Remove(other.gameObject);
            other.GetComponent<RoomChecker>().inRoom = false;
        }
    }

}
