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
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            enemy.SetState(Enemy.EnemyStates.Attacking);
            enemyNav.AdjustSpeed(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            enemy.SetState(Enemy.EnemyStates.Pursuit);
            enemyNav.AdjustSpeed(enemy.walkSpeed);
            enemy.attackTimeCounter = enemy.attackRate;
        }
    }
}
