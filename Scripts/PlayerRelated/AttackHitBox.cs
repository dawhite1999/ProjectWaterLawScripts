using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public Player player;
    public bool gammaOn = false;
    public bool counterOn = false;
    public float oriEnemyDefense;
    int soundPicker;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            //ignore enemy defense when using gamma knife
            if(gammaOn == true)
            {
                oriEnemyDefense = other.GetComponent<Enemy>().defense;
                other.GetComponent<Enemy>().defense = 1;
                other.GetComponent<Enemy>().RecieveDamage(player.strength + player.gammaBonus);
                other.GetComponent<Enemy>().defense = oriEnemyDefense;
                player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.BigClash, other.GetComponent<Enemy>().enemySFXSources);
                return;
            }
            if(counterOn == true)
            {
                soundPicker = Random.Range(0, 2);
                switch(soundPicker)
                {
                    case 0:
                        player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.MediumClash1, other.GetComponent<Enemy>().enemySFXSources);
                        break;
                    case 1:
                        player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.MediumClash2, other.GetComponent<Enemy>().enemySFXSources);
                        break;
                }
                other.GetComponent<Enemy>().RecieveDamage(player.strength + player.counterDamage);
                return;
            }
            soundPicker = Random.Range(0, 5);
            switch(soundPicker)
            {
                case 0:
                    player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.SmallClash1, other.GetComponent<Enemy>().enemySFXSources);
                    break;
                case 1:
                    player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.SmallClash2, other.GetComponent<Enemy>().enemySFXSources);
                    break;
                case 2:
                    player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.SmallClash3, other.GetComponent<Enemy>().enemySFXSources);
                    break;
                case 3:
                    player.audioMan.PlayEnemyClip(AudioMan.EnemyClipNames.SmallClash4, other.GetComponent<Enemy>().enemySFXSources);
                    break;
            }
            other.GetComponent<Enemy>().RecieveDamage(player.strength);
        }
    }
}
