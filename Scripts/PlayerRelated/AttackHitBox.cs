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
                player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.BigClash);
                return;
            }
            if(counterOn == true)
            {
                soundPicker = Random.Range(0, 2);
                switch(soundPicker)
                {
                    case 0:
                        player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.MediumClash1);
                        break;
                    case 1:
                        player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.MediumClash2);
                        break;
                }
                other.GetComponent<Enemy>().RecieveDamage(player.strength + player.counterDamage);
                return;
            }
            soundPicker = Random.Range(0, 5);
            switch(soundPicker)
            {
                case 0:
                    player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.SmallClash1);
                    break;
                case 1:
                    player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.SmallClash2);
                    break;
                case 2:
                    player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.SmallClash3);
                    break;
                case 3:
                    player.audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.SmallClash4);
                    break;
            }
            other.GetComponent<Enemy>().RecieveDamage(player.strength);
        }
    }
}
