using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    //this is to trigger the attack state when the player enters the range
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && enemy.currentState != Enemy.EnemyStates.Stunned && enemy.currentState != Enemy.EnemyStates.Exploding)
        {
            enemy.SetState(Enemy.EnemyStates.Attacking);
        }
    }
    //stop attacking if player exits range
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null && enemy.currentState != Enemy.EnemyStates.Stunned && enemy.currentState != Enemy.EnemyStates.Exploding)
        {
            enemy.SetState(Enemy.EnemyStates.Pursuit);
        }
    }
}
