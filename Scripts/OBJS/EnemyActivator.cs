using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject[] enemies;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            foreach (GameObject robot in enemies)
            {
                robot.SetActive(true);
            }
        }
    }
}
