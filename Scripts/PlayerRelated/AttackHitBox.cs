using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public Player player;
    public bool gammaOn = false;
    public bool counterOn = false;
    public float oriEnemyDefense;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if(gammaOn == true)
            {
                oriEnemyDefense = other.GetComponent<Enemy>().defense;
                other.GetComponent<Enemy>().defense = 1;
                other.GetComponent<Enemy>().RecieveDamage(player.strength + player.gammaBonus);
                other.GetComponent<Enemy>().defense = oriEnemyDefense;
                return;
            }
            if(counterOn == true)
            {
                other.GetComponent<Enemy>().RecieveDamage(player.strength + player.counterDamage);
                return;
            }
            other.GetComponent<Enemy>().RecieveDamage(player.strength);
        }
    }
}
