using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] float hitBoxActiveTime = 0;
    public GameObject attackHitBox;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackHitBox.SetActive(false);
    }
    public IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackStartup);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(hitBoxActiveTime);
        attackHitBox.SetActive(false);
        animator.SetBool("isAttacking", false);
    }
    protected override void MakeDecision()
    {
        if (currentState == EnemyStates.Attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter <= 0)
            {
                attackTimeCounter = attackRate;
                StartCoroutine(Attack());
            }
        }
    }
}
