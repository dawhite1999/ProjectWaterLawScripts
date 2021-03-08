using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool beingLaunched = false;
    public void InvokeRePickup() { Invoke("RePickup", 1f); }
    void RePickup() { beingLaunched = false; }
    private void Start()
    {
        GetComponent<Outline>().OutlineColor = new Color(0, 1, 0.15f);
        GetComponent<Outline>().OutlineWidth = 10;
        GetComponent<Outline>().enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ImpassableOBJ" && FindObjectOfType<RoomBeam>().heldObject == gameObject)
            FindObjectOfType<RoomBeam>().LetGo();
    }
}
