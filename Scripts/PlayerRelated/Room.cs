using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject roomHolder;
    [SerializeField] GameObject room;
    [SerializeField] KeyCode RoomButton = KeyCode.Q;
    [SerializeField] float growthRate;
    [SerializeField] float damageThreshold;
    [SerializeField] float damageTaken;
    public float roomTimeActiveMax;
    public float roomTimeActiveCurr;
    bool roomLocked = false;
    bool playOnce = false;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        room.SetActive(false);
        roomTimeActiveCurr = roomTimeActiveMax;
    }

    // Update is called once per frame
    void Update() { InputCheck(); RoomCountDown(); }

    void InputCheck()
    {
        if(Input.GetKeyDown(RoomButton))
        {
            if (room.activeSelf == false)
            {
                room.SetActive(true);
                return;
            }
            else
            {
                CancelRoom();
                room.SetActive(true);
            }
        }
        if (Input.GetKey(RoomButton))
            ExpandRoom();
        if (Input.GetKeyUp(RoomButton))
            SetLockedRoom();
    }
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
    void ExpandRoom()
    {
        room.transform.localScale += new Vector3(growthRate, growthRate, growthRate);
        roomHolder.transform.position = gameObject.transform.position;
        if(room.transform.localScale.x > damageThreshold * .8f && playOnce == false)
        {
            playOnce = true;
            room.GetComponent<Animator>().Play("ChangeColor");
        }
        if (room.transform.localScale.x > damageThreshold)
            player.RecieveDamage(damageTaken);
    }
    void SetLockedRoom()
    {
        roomLocked = true;
        playOnce = false;
        room.GetComponent<Animator>().Play("Stable");
    }
    public void CancelRoom()
    {
        room.transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Player>().inRoom = false;
        GetComponent<RoomBeam>().LetGo();
        GetComponent<RoomBeam>().currentState = RoomBeam.GUN_STATE.INACTIVE;
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
