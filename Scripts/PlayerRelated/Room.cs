using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject roomHolder;
    public GameObject room;
    [SerializeField] KeyCode RoomButton = KeyCode.Q;
    public float growthRate = 0;
    public float damageThreshold = 0;
    [SerializeField] float damageTaken = 0;
    public float roomTimeActiveMax;
    [HideInInspector] public float roomTimeActiveCurr;
    bool roomLocked = false;
    bool playOnce = false;
    Player player;
    HypeMan hypeMan;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        hypeMan = FindObjectOfType<HypeMan>();
        room.SetActive(false);
        roomTimeActiveCurr = roomTimeActiveMax;
    }

    // Update is called once per frame
    void Update() { InputCheck(); RoomCountDown(); }

    //has the inputs for turning on and off the room
    void InputCheck()
    {
        if(Input.GetKeyDown(RoomButton))
        {
            if (room.activeSelf == false)
            {
                room.SetActive(true);
                hypeMan.PHT("Room");
                player.animator.SetBool("roomExpanding", true);
                player.UseStamina();
                return;
            }
            else
            {
                CancelRoom();
                room.SetActive(true);
                player.UseStamina();
            }
        }
        if (Input.GetKey(RoomButton))
            ExpandRoom();
        if (Input.GetKeyUp(RoomButton))
            SetLockedRoom();
    }
    //called to put a time limit on how long a room is active
    void RoomCountDown()
    {
        if (roomLocked == true)
        {
            roomTimeActiveCurr -= Time.deltaTime;
            if (roomTimeActiveCurr <= 5 && playOnce == false)
            {
                room.GetComponent<Animator>().Play("RoomFade");
                playOnce = true;
            }
            if (roomTimeActiveCurr <= 0)
                CancelRoom();
        }
    }
    //grows the room
    void ExpandRoom()
    {
        if(roomLocked == false)
        {
            room.transform.localScale += new Vector3(growthRate, growthRate, growthRate);
            roomHolder.transform.position = gameObject.transform.position;
            if (room.transform.localScale.x > damageThreshold * .8f && playOnce == false)
            {
                playOnce = true;
                room.GetComponent<Animator>().Play("ChangeColor");
            }
            if (room.transform.localScale.x > damageThreshold)
                player.RecieveDamage(damageTaken);
        }
    }
    //called to stop the room from growing
    public void SetLockedRoom()
    {
        if(roomLocked == false)
            room.GetComponent<Animator>().Play("Stable");
        roomLocked = true;
        playOnce = false;
        player.animator.SetBool("roomExpanding", false);
    }
    //turns the room off
    public void CancelRoom()
    {
        player.animator.SetBool("roomExpanding", false);
        player.animator.SetBool("isHolding", false);
        room.transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Player>().inRoom = false;
        GetComponent<RoomBeam>().LetGo();
        foreach (GameObject item in FindObjectOfType<RoomHitBox>().objsInRoom)
        {
            item.GetComponent<RoomChecker>().inRoom = false;
        }
        FindObjectOfType<RoomHitBox>().objsInRoom.Clear();
        roomTimeActiveCurr = roomTimeActiveMax;
        roomLocked = false;
        playOnce = false;
        room.SetActive(false);
    }
}
