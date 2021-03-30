using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSwitch : DefaultSwitch
{
    [SerializeField] float activeTime = 0;

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
            if (lerpOnce == false)
            {
                Invoke("TurnOff", activeTime);
                door.LerpInit(true);
                lerpOnce = true;
            }
        }
    }
    IEnumerator TurnOffCoroutine()
    {
        switchValue = false;
        GetComponent<Renderer>().material = switchOff;
        door.LerpInit(false);
        yield return new WaitForSeconds(door.GetWaitTime());
        lerpOnce = false;
    }
    void TurnOff()
    {
        StartCoroutine(TurnOffCoroutine());
    }
}
