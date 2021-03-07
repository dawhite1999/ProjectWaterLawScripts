using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().RecieveDamage(GetComponentInParent<Enemy>().strength);
        }
    }
}
