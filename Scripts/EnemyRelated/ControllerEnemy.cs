using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ControllerEnemy : Enemy
{
    public GameObject projectilePoint;
    public GameObject projectileSpawn;
    public GameObject projectile;
    GameObject attacker;
    bool canShoot = false;
    [SerializeField] float projectileSpeed = 0;
    public GameObject secondExplosion;

    protected override void Start()
    {
        base.Start();
        enemyNav = GetComponentInChildren<EnemyNav>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        attacker = GetComponentInChildren<NavMeshAgent>().gameObject;
        enemyNav.AdjustSpeed(walkSpeed);
    }
    protected override void MakeDecision()
    {
        if (currentState == EnemyStates.Attacking)
        {
            //shoot a ray to make sure you can hit the player
            Ray trackingRay = new Ray(projectileSpawn.transform.position, projectileSpawn.transform.forward);
            RaycastHit trackingRayHit;
            //Debug.DrawRay(trackingRay.origin, projectileSpawn.transform.forward * 100, Color.red);
            if (Physics.Raycast(trackingRay, out trackingRayHit))
            {
                if (trackingRayHit.collider.GetComponent<Player>() != null)
                    canShoot = true;
                else
                    canShoot = false;
            }
            //countdown attack time
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter <= 0)
            {
                attackTimeCounter = attackRate;
                ShootBullet();
            }
            projectilePoint.transform.LookAt(GetComponentInChildren<EnemyNav>().playerLoc);
        }
    }
    void ShootBullet()
    {
        if (canShoot == true)
        {
            var projectileObj = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            projectileObj.GetComponent<Rigidbody>().velocity = (projectilePoint.transform.forward).normalized * projectileSpeed;
            projectileObj.GetComponent<Projectile>().damage = strength;
        }
    }
    protected override IEnumerator Defeat()
    {
        enemyNav.AdjustSpeed(0);
        animator.SetBool("isExploded", true);
        yield return new WaitForSeconds(koAnimTime);
        explodeParticles.SetActive(true);
        secondExplosion.SetActive(true);
        attacker.GetComponent<Renderer>().enabled = false;
        attacker.GetComponentInChildren<Renderer>().enabled = false;
        GetComponent<RoomChecker>().inRoom = false;
        if (FindObjectOfType<RoomHitBox>() != null)
            FindObjectOfType<RoomHitBox>().objsInRoom.Remove(gameObject);
        yield return new WaitForSeconds(.4f);
        if (FindObjectOfType<EnemySpawner>() != null)
            FindObjectOfType<EnemySpawner>().RespawnEnemy(gameObject);
        if (enemyCounter != null)
            enemyCounter.RemoveEnemy(GetComponent<Enemy>());
        Destroy(gameObject);
    }
}
