using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePoint;
    public GameObject projectileSpawn;
    public GameObject projectile;
    [SerializeField] float projectileSpeed = 0;
    protected override void MakeDecision()
    {
        if (currentState == EnemyStates.Attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter <= 0)
            {
                attackTimeCounter = attackRate;
                ShootBullet();
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        projectilePoint.transform.LookAt(GetComponent<EnemyNav>().playerLoc);
    }
    void ShootBullet()
    {
        var projectileObj = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        projectileObj.GetComponent<Rigidbody>().velocity = (projectilePoint.transform.forward).normalized * projectileSpeed;
        projectileObj.GetComponent<Projectile>().damage = strength;
    }
}
