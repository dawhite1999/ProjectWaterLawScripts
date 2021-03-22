using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSwitch : MonoBehaviour
{
    [SerializeField] protected bool switchValue;
    public Animator thingToAnimate;
    [SerializeField] protected string anim1Name = "";
    [SerializeField] protected string anim2Name = "";
    public Material switchOff;
    public Material switchOn;
    protected void Start()
    {
        if (switchValue == false)
            GetComponent<Renderer>().material = switchOff;
        else
            GetComponent<Renderer>().material = switchOn;
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null || collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            switchValue = !switchValue;
            if (switchValue == false)
            {
                GetComponent<Renderer>().material = switchOff;
                thingToAnimate.Play(anim1Name);
            }
            else
            {
                GetComponent<Renderer>().material = switchOn;
                thingToAnimate.Play(anim1Name);
            }
        }
    }
}
