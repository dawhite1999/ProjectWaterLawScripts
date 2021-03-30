using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTriggerSwitch : TimedSwitch
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null || other.GetComponent<InteractableObject>() != null && switchValue == false)
        {
            GetComponent<Renderer>().material = switchOn;
            switchValue = true;
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
}
