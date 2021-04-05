using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage = 0;
    [SerializeField] float explosionTime = 0;
    public GameObject explosionEffect;
    [SerializeField] AudioSource[] audioSources = new AudioSource[1];
    private void Start()
    {
        ChangeVolume();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "EnemyGun" && other.GetComponent<AttackRange>() == null && other.GetComponent<RoomHitBox>() == null && other.GetComponent<EnemySpawner>() == null)
            StartCoroutine(Explosion());
        if (other.GetComponent<Player>() != null)
            other.GetComponent<Player>().RecieveDamage(damage);
        if (other.GetComponent<Enemy>() != null)
            other.GetComponent<Enemy>().RecieveDamage(damage);
    }
    IEnumerator Explosion()
    {
        explosionEffect.SetActive(true);
        FindObjectOfType<AudioMan>().PlayEnemyClip(AudioMan.EnemyClipNames.Explosion2, audioSources);
        GetComponent<Renderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(explosionTime);
        GetComponent<RoomChecker>().inRoom = false;
        if(FindObjectOfType<RoomHitBox>() != null)
            FindObjectOfType<RoomHitBox>().objsInRoom.Remove(gameObject);
        Destroy(gameObject);
    }
    public void ChangeVolume()
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = FindObjectOfType<AudioMan>().GetEXVolume();
        }
    }
}
