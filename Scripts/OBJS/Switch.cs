using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] protected bool switchValue;
    public Animator thingToAnimate;
    [SerializeField] string anim1Name = "";
    [SerializeField] string anim2Name = "";
    public Material switchOff;
    public Material switchOn;
    private void Start()
    {
        if (switchValue == false)
            GetComponent<Renderer>().material = switchOff;
        else
            GetComponent<Renderer>().material = switchOn;
    }
    protected void OnCollisionEnter(Collision collision)
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
