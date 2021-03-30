using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEarner : MonoBehaviour
{
    [SerializeField] int skillPoints = 0;
    bool earned;
    [Header("This is to mark which bool in static man gets turned true. level 1 would be 1, etc...")]
    [SerializeField] int earnerNum = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            other.GetComponent<StatLvlHolder>().skillPoints += skillPoints;
            StaticMan.spEarnCheck[earnerNum] = true;
        }
    }
}
