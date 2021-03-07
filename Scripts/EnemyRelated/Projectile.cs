using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage = 0;
    [SerializeField] float explosionTime = 0;
    public GameObject explosionEffect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "EnemyGun" && other.GetComponent<AttackRange>() == null && other.GetComponent<RoomHitBox>() == null)
            StartCoroutine(Explosion());
        if (other.GetComponent<Player>() != null)
            other.GetComponent<Player>().RecieveDamage(damage);
    }
    IEnumerator Explosion()
    {
        explosionEffect.SetActive(true);
        GetComponent<Renderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(explosionTime);
        GetComponent<RoomChecker>().inRoom = false;
        if(FindObjectOfType<RoomHitBox>() != null)
            FindObjectOfType<RoomHitBox>().objsInRoom.Remove(gameObject);
        Destroy(gameObject);
    }
}
