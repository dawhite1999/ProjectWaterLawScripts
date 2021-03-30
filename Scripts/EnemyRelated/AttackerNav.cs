using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackerNav : EnemyNav
{
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerLoc = FindObjectOfType<Player>().gameObject.transform;
        enemy = GetComponentInParent<Enemy>();
    }
}
