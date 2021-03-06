﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public GameObject roomHolder;
    [HideInInspector] public GameObject room;
    [SerializeField] KeyCode RoomButton = KeyCode.Q;
    public float growthRate = 0;
    public float damageThreshold = 0;
    [SerializeField] float damageTaken = 0;
    public float roomTimeActiveMax;
    [HideInInspector] public float roomTimeActiveCurr;
    bool roomLocked = false;
    bool playOnce = false;
    int roomSFXIndex;
    Player player;
    HypeMan hypeMan;
    RoomBeam roomBeam;
    AudioMan audioMan;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        hypeMan = FindObjectOfType<HypeMan>();
        roomBeam = GetComponent<RoomBeam>();
        audioMan = FindObjectOfType<AudioMan>();
        roomHolder = GameObject.Find("RoomHolder");
        room = GameObject.Find("Room");
        room.SetActive(false);
        roomTimeActiveCurr = roomTimeActiveMax;
    }

    // Update is called once per frame
    void Update() { InputCheck(); RoomCountDown(); }

    //has the inputs for turning on and off the room
    void InputCheck()
    {
        if(player.disableInput == false)
        {
            if (Input.GetKeyDown(RoomButton))
            {
                if (room.activeSelf == false)
                {
                    StartRoom();
                    return;
                }
                else
                {
                    CancelRoom();
                    StartRoom();
                }
            }
            if (Input.GetKey(RoomButton))
                ExpandRoom();
            if (Input.GetKeyUp(RoomButton))
                SetLockedRoom();
        }
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
    //creates room
    void StartRoom()
    {
        room.SetActive(true);
        hypeMan.PHT("Room");
        StartCoroutine(PlayRoomSounds());
        player.animator.SetBool("roomExpanding", true);
        player.UseStamina();
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
        if(roomSFXIndex != 0)
            audioMan.Stop1RePlayerSFX(roomSFXIndex);
    }
    //turns the room off
    public void CancelRoom()
    {
        player.animator.SetBool("roomExpanding", false);
        player.animator.SetBool("isHolding", false);
        room.transform.localScale = new Vector3(1, 1, 1);
        player.inRoom = false;
        roomBeam.RefreshShamblesVars();
        roomBeam.LetGo();
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
    IEnumerator PlayRoomSounds()
    {
        audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.RoomStart);
        yield return new WaitForSeconds(.6f);
        if (roomLocked == true)
            yield break;
        audioMan.PlayPlayerRepeatSound(AudioMan.PlayerClipNames.RoomLoop);
        //this saved so that we can cancel the correct sfx when we stop growing the room.
        roomSFXIndex = audioMan.GetIndexNum();
    }
}
