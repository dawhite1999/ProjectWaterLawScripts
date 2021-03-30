using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    //unity
    [HideInInspector] public Transform playerLoc;
    //references
    protected RaycastHit rayHit;
    protected NavMeshAgent navMeshAgent;
    protected Player player;
    protected Enemy enemy;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        playerLoc = FindObjectOfType<Player>().gameObject.transform;
    }
    public void AdjustSpeed(float newSpeed) { GetComponent<NavMeshAgent>().speed = newSpeed; }
    // Update is called once per frame
    protected void Update() { PursuePlayer(); }

    protected void PursuePlayer()
    {
        if(enemy.currentState == Enemy.EnemyStates.Pursuit)
        {
            navMeshAgent.SetDestination(playerLoc.position);
        }
    }
}

