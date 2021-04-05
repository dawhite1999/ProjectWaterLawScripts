using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    int soundPicker;
    int min;
    int max;
    AudioMan audioMan;
    private void Start()
    {
        audioMan = FindObjectOfType<AudioMan>();
        switch(GetComponentInParent<Enemy>().modelName)
        {
            case Enemy.Model.Big:
                min = 0; max = 2;
                break;
            case Enemy.Model.Normal:
                min = 2; max = 4;
                break;
            case Enemy.Model.Small:
                min = 4; max = 6;
                break;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().RecieveDamage(GetComponentInParent<Enemy>().strength);
            soundPicker = Random.Range(min, max);
            switch(soundPicker)
            {
                case 0:
                    audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.BigHit1);
                    break;
                case 1:
                    audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.BigHit2);
                    break;
                case 2:
                    audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.MediumHit1);
                    break;
                case 3:
                    audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.MediumHit2);
                    break;
                case 4:
                    audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.SmallHit1);
                    break;
                case 5:
                    audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.SmallHit2);
                    break;
            }
        }
    }
}
