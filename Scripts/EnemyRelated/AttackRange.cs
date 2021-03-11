using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    Enemy enemy;
    EnemyNav enemyNav;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyNav = GetComponentInParent<EnemyNav>();
    }
    //this is to trigger the attack state when the player enters the range
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && enemy.currentState != Enemy.EnemyStates.Stunned)
        {
            enemy.SetState(Enemy.EnemyStates.Attacking);
            enemyNav.AdjustSpeed(0);
        }
    }
    //stop attacking if player exits range
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null && enemy.currentState != Enemy.EnemyStates.Stunned)
        {
            enemy.SetState(Enemy.EnemyStates.Pursuit);
            enemyNav.AdjustSpeed(enemy.walkSpeed);
            enemy.attackTimeCounter = enemy.attackRate;
        }
    }
}
