using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePoint;
    public GameObject projectileSpawn;
    public GameObject projectile;
    bool canShoot = false;
    [SerializeField] float projectileSpeed = 0;
    protected override void MakeDecision()
    {
        if (currentState == EnemyStates.Attacking)
        {
            //shoot a ray to make sure you can hit the player
            Ray trackingRay = new Ray(projectileSpawn.transform.position, projectileSpawn.transform.forward);
            RaycastHit trackingRayHit;
            Debug.DrawRay(trackingRay.origin, projectileSpawn.transform.forward * 100, Color.red);
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
            projectilePoint.transform.LookAt(GetComponent<EnemyNav>().playerLoc);
        }
    }
    
    void ShootBullet()
    {
        if(canShoot == true)
        {
            var projectileObj = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            projectileObj.GetComponent<Rigidbody>().velocity = (projectilePoint.transform.forward).normalized * projectileSpeed;
            projectileObj.GetComponent<Projectile>().damage = strength;
        }
    }
}
