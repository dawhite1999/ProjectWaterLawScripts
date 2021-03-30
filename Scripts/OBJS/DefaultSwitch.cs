using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSwitch : MonoBehaviour
{
    public bool switchValue;
    public Material switchOff;
    public Material switchOn;
    protected bool lerpOnce = false;
    public Door door;
    protected virtual void Start()
    {
        if (switchValue == false)
            GetComponent<Renderer>().material = switchOff;
        else
            GetComponent<Renderer>().material = switchOn;
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            switchValue = !switchValue;
            if (switchValue == false)
            {
                GetComponent<Renderer>().material = switchOff;
                if (lerpOnce == false)
                {
                    door.LerpInit(true);
                    lerpOnce = true;
                    StartCoroutine(AvailableWait());
                }
            }
            else if(lerpOnce == false)
            {
                GetComponent<Renderer>().material = switchOn;
                door.LerpInit(true);
                lerpOnce = true;
            }
        }
    }
    IEnumerator AvailableWait()
    {
        yield return new WaitForSeconds(door.GetWaitTime());
        lerpOnce = false;
    }
}
