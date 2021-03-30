using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitch : DefaultSwitch
{
    [SerializeField] protected float timeOn = 0;
    public GameObject[] timedSwitches = new GameObject[0];

    protected override void Start()
    {
        return;
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null || collision.gameObject.GetComponent<InteractableObject>() != null && switchValue == false)
        {
            switchValue = true;
            GetComponent<Renderer>().material = switchOn;
            if (SwitchCheck() == false)
            {
                Invoke("TurnOff", timeOn);
            }
            else
            {
                foreach (GameObject timedSwitch in timedSwitches)
                {
                    timedSwitch.GetComponentInChildren<TimedSwitch>().CancelInvoke("TurnOff");
                }
                if (lerpOnce == false)
                {
                    door.LerpInit(true);
                    lerpOnce = true;
                }
            }
        }
    }

    protected bool SwitchCheck()
    {
        foreach (GameObject timedSwitch in timedSwitches)
        {
            if (timedSwitch.GetComponentInChildren<TimedSwitch>().switchValue == false)
                return false;
        }
        return true;
    }
    protected void TurnOff()
    {
        switchValue = false;
        GetComponent<Renderer>().material = switchOff;
    }
}
