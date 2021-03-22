using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitch : MonoBehaviour
{
    [SerializeField] float timeOn;
    [SerializeField] GameObject[] timedSwitches = new GameObject[0];
    public Material switchOff;
    public Material switchOn;
    [SerializeField] protected bool switchValue;
    [SerializeField] Door door;
    bool lerpOnce = false;

    protected void OnCollisionEnter(Collision collision)
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
    bool SwitchCheck()
    {
        foreach (GameObject timedSwitch in timedSwitches)
        {
            if (timedSwitch.GetComponentInChildren<TimedSwitch>().switchValue == false)
                return false;
        }
        return true;
    }
    void TurnOff()
    {
        switchValue = false;
        GetComponent<Renderer>().material = switchOff;
    }
}
