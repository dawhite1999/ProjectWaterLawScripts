using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : DefaultSwitch
{
    public GameObject switchGFX;
    public enum SwitchType { Sphere, Box}
    public SwitchType switchType;

    protected override void Start()
    {
        return;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null || other.GetComponent<InteractableObject>() != null)
        {
            if(switchType == SwitchType.Box && other.GetComponent<BoxCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOn;
                door.LerpInit(true);
                return;
            }
            if (switchType == SwitchType.Sphere && other.GetComponent<SphereCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOn;
                door.LerpInit(true);
                return;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null || other.GetComponent<InteractableObject>() != null)
        {
            if (switchType == SwitchType.Box && other.GetComponent<BoxCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOff;
                door.LerpInit(false);
            }
            if (switchType == SwitchType.Sphere && other.GetComponent<SphereCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOff;
                door.LerpInit(false);
            }
        }
    }
}
