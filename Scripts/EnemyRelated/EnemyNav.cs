using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    //unity
    [HideInInspector] public Transform playerLoc;
    //references
    RaycastHit rayHit;
    NavMeshAgent navMeshAgent;
    Player player;
    Enemy enemy;
    //variables
    [SerializeField] float detBeamDis;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        playerLoc = FindObjectOfType<Player>().gameObject.transform;
    }
    public void AdjustSpeed(float newSpeed) { navMeshAgent.speed = newSpeed; }
    // Update is called once per frame
    void Update() { PursuePlayer(); }

    void PursuePlayer()
    {
        if(enemy.currentState == Enemy.EnemyStates.Pursuit)
        {
            navMeshAgent.SetDestination(playerLoc.position);
        }
    }
}

