using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    public Animator thingToAnimate;
    [SerializeField] string anim1Name = "";
    [SerializeField] string anim2Name = "";
    [SerializeField] Material switchOff;
    [SerializeField] Material switchOn;
    [SerializeField] GameObject switchGFX;
    [SerializeField] enum SwitchType { Sphere, Box}
    [SerializeField] SwitchType switchType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null || other.GetComponent<InteractableObject>() != null)
        {
            if(switchType == SwitchType.Box && other.GetComponent<BoxCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOn;
                thingToAnimate.Play(anim1Name);
                return;
            }
            if (switchType == SwitchType.Sphere && other.GetComponent<SphereCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOn;
                thingToAnimate.Play(anim1Name);
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
                thingToAnimate.Play(anim2Name);
            }
            if (switchType == SwitchType.Sphere && other.GetComponent<SphereCollider>() == true)
            {
                switchGFX.GetComponent<Renderer>().material = switchOff;
                thingToAnimate.Play(anim2Name);
            }
        }
    }
}
