using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public string model;
    public float maxHealth;
    public float defense = 0.9f;
    public float walkSpeed;
    public float strength;
    public float stunDuration;
    public float attackRate;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool undoStun = false;
    [SerializeField] protected float attackStartup;
    public GameObject hitParticles;
    public GameObject explodeParticles;
    public GameObject sparkParticles;
    public Transform damagePopupPrefab;
    [SerializeField] float koAnimTime = 0;
    [HideInInspector] public float currentHealth;
    AudioMan audioMan;
    public Animator animator;
    Player player;
    EnemyNav enemyNav;
    protected float attackTimeCounter;
    [HideInInspector] public List<EnemyStates> EnemyStateList = new List<EnemyStates>();
    public EnemyStates currentState = EnemyStates.Pursuit;

    public enum EnemyStates
    {
        Attacking,
        Pursuit,
        Stunned,
        Exploding
    }
    //called to set a new state
    public void SetState(EnemyStates newState)
    {
        currentState = newState;
        switch(currentState)
        {
            case EnemyStates.Attacking:
                enemyNav.AdjustSpeed(0);
                break;
            case EnemyStates.Exploding:
                StartCoroutine(Defeat());
                break;
            case EnemyStates.Pursuit:
                enemyNav.AdjustSpeed(walkSpeed);
                attackTimeCounter = attackRate;
                break;
            case EnemyStates.Stunned:
                StartCoroutine(Stun());
                break;
        }
    }
    public GameObject GetEnemy()
    {
        return gameObject;
    }
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        attackTimeCounter = attackRate;
        audioMan = FindObjectOfType<AudioMan>();
        player = FindObjectOfType<Player>();
        enemyNav = GetComponent<EnemyNav>();
        animator = GetComponent<Animator>();
        EnemyStateList.Add(EnemyStates.Pursuit);
        EnemyStateList.Add(EnemyStates.Attacking);
        EnemyStateList.Add(EnemyStates.Stunned);
        EnemyStateList.Add(EnemyStates.Exploding);
        GetComponent<Outline>().OutlineColor = new Color(0, 1, 0.15f);
        GetComponent<Outline>().OutlineWidth = 10;
        GetComponent<Outline>().enabled = false;
    }
    //make decision is overwritten in derivitive classes
    protected virtual void Update(){ MakeDecision(); }
    protected virtual void MakeDecision() { }

    public void RecieveDamage(float damageTaken)
    {
        if(currentState != EnemyStates.Exploding)
        {
            damageTaken *= defense;
            currentHealth = Mathf.RoundToInt(currentHealth - damageTaken);
            if(damageTaken > 9999)
            {
                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    StartCoroutine(Defeat());
                    return;
                }
            }
            Transform damagePopupTransform = Instantiate(damagePopupPrefab, gameObject.transform.position, Quaternion.identity);
            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.SetDamage(damageTaken);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                SetState(EnemyStates.Exploding);
            }
        }
    }
    //called when enemy is hit with a projectile sent by the player, this will disable movement and make enemy interactable
    IEnumerator Stun()
    {
        animator.SetBool("isStunned", true);
        sparkParticles.SetActive(true);
        GetComponent<EnemyNav>().AdjustSpeed(0);
        gameObject.AddComponent<InteractableObject>();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(stunDuration);
        undoStun = true;

    }
    //Called when enemy is ready to be unstunned, and it touches the ground
    public void UnStun()
    {
        undoStun = false;
        sparkParticles.SetActive(false);
        animator.SetBool("isStunned", false);
        if (FindObjectOfType<RoomBeam>().heldObject == gameObject)
            FindObjectOfType<RoomBeam>().LetGo();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<NavMeshAgent>().enabled = true;
        Destroy(GetComponent<InteractableObject>());
        GetComponent<EnemyNav>().AdjustSpeed(walkSpeed);
        SetState(EnemyStates.Pursuit);
    }
    //this is called to stun the enemy, or take damage when enemy is thrown
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<InteractableObject>() != null && currentState != EnemyStates.Exploding)
        {
            if(collision.gameObject.GetComponent<InteractableObject>().beingLaunched == true)
            {
                RecieveDamage(TaktDamageCalc(collision.gameObject.GetComponent<Rigidbody>()));
                if (currentHealth > 0)
                    SetState(EnemyStates.Stunned);
            }
        }
        if(currentState == EnemyStates.Stunned)
        {
            if(GetComponent<InteractableObject>().beingLaunched == true)
                RecieveDamage(TaktDamageCalc(gameObject.GetComponent<Rigidbody>()));
        }
    }
    //calculates the damage dealt when takt is used
    float TaktDamageCalc(Rigidbody incomingObj)
    {
        float netDamage = 5;
        netDamage *= incomingObj.mass;
        return netDamage;
    }
    IEnumerator Defeat()
    {
        GetComponent<EnemyNav>().AdjustSpeed(0);
        animator.SetBool("isExploded", true);
        yield return new WaitForSeconds(koAnimTime);
        explodeParticles.SetActive(true);
        GetComponent<RoomChecker>().inRoom = false;
        if (FindObjectOfType<RoomHitBox>() != null)
            FindObjectOfType<RoomHitBox>().objsInRoom.Remove(gameObject);
        yield return new WaitForSeconds(.4f);
        if (FindObjectOfType<EnemySpawner>() != null)
            FindObjectOfType<EnemySpawner>().RespawnEnemy(gameObject);
            Destroy(gameObject);
    }
}
