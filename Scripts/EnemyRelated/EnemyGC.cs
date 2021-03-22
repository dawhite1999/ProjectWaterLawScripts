using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGC : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //ground check true
            GetComponentInParent<Enemy>().isGrounded = true;
            if (GetComponentInParent<Enemy>().undoStun == true)
                GetComponentInParent<Enemy>().UnStun();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
            GetComponentInParent<Enemy>().isGrounded = false;
    }
}
