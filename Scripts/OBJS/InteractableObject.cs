using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool beingLaunched = false;
    public int itemType = 0;
    public GameObject blackParticles;
    public ItemSpawner spawner;
    //this is to make sure you cant pick up the item immediatly after it is thrown
    public void InvokeRePickup() { Invoke("RePickup", 1f); }
    void RePickup() { beingLaunched = false; }

    private void Start()
    {
        GetComponent<Outline>().OutlineColor = new Color(0, 1, 0.15f);
        GetComponent<Outline>().OutlineWidth = 10;
        GetComponent<Outline>().enabled = false;
    }
    //this is to make sure the held item doesn't clip through walls
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ImpassableOBJ" && FindObjectOfType<RoomBeam>().heldObject == gameObject)
            FindObjectOfType<RoomBeam>().LetGo();
    }
    public void Phase()
    {
        StartCoroutine(PhaseOut());
    }
    IEnumerator PhaseOut()
    {
        blackParticles.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        if (GetComponent<BoxCollider>() != null)
            GetComponent<BoxCollider>().enabled = false;
        else if(GetComponent<SphereCollider>() != null)
            GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        GetComponent<RoomChecker>().inRoom = false;
        FindObjectOfType<RoomHitBox>().objsInRoom.Remove(gameObject);
        spawner.TeleportItem(gameObject);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
    }
    public void Renew()
    {
        blackParticles.SetActive(false);
        GetComponent<MeshRenderer>().enabled = true;
        if (GetComponent<BoxCollider>() != null)
            GetComponent<BoxCollider>().enabled = true;
        else if (GetComponent<SphereCollider>() != null)
            GetComponent<SphereCollider>().enabled = true;
    }
}
