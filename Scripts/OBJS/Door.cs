using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public bool canMove;
    [SerializeField] float speed = 5.0F;
    [Header("Will stop lerping after this amount of time")]
    [SerializeField] float waitTime = 1.0F;
    private float startTime;
    private float journeyLength;
    bool isOpening = false;

    public float GetWaitTime() { return waitTime; }
    void LerpDoor()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;
        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;
        if (isOpening == true)
        {
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(top.position, bottom.position, fractionOfJourney);
        }
        else
        {
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(bottom.position, top.position, fractionOfJourney);
        }
    }
    private void Update()
    {
        if (canMove == true)
            LerpDoor();
    }
    public void LerpInit(bool open)
    {
        //dont lerp if you are already lerping
        if(canMove == false)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            // Calculate the journey length.
            journeyLength = Vector3.Distance(top.position, bottom.position);
            canMove = true;
            isOpening = open;
            Invoke("StopLerp", waitTime);
        }
    }
    void StopLerp()
    {
        canMove = false;
    }
}
