using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float defense = 0.9f;
    public float walkSpeed;
    public float strength;
    public float attackRate;
    public float stunDuration;
    [SerializeField] float koAnimTime = 0;
    [HideInInspector] public float currentHealth;
    AudioMan audioMan;
    Animator animator;
    Player player;
    [HideInInspector] public float attackTimeCounter;
    [HideInInspector] public List<EnemyStates> EnemyStateList = new List<EnemyStates>();
    [HideInInspector] public EnemyStates currentState = EnemyStates.Pursuit;

    public enum EnemyStates
    {
        Attacking,
        Pursuit,
        Stunned
    }
    public enum AnimStates
    {
        Attacking,
        Pursuit,
        Stunned
    }
    //called to set a new state
    public void SetState(EnemyStates newState) { currentState = newState; }
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        attackTimeCounter = attackRate;
        audioMan = FindObjectOfType<AudioMan>();
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        EnemyStateList.Add(EnemyStates.Pursuit);
        EnemyStateList.Add(EnemyStates.Attacking);
        EnemyStateList.Add(EnemyStates.Stunned);
        GetComponent<Outline>().OutlineColor = new Color(0, 1, 0.15f);
        GetComponent<Outline>().OutlineWidth = 10;
        GetComponent<Outline>().enabled = false;
    }
    //make decision is overwritten in derivitive classes
    protected virtual void Update(){ MakeDecision(); }
    protected virtual void MakeDecision() { }

    public void RecieveDamage(float damageTaken)
    {
        damageTaken *= defense;
        currentHealth = Mathf.RoundToInt(currentHealth - damageTaken);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(Defeat());
        }
        Debug.Log("Damage taken " + damageTaken * defense);
    }
    //called when enemy is hit with a projectile sent by the player, this will disable movement and make enemy interactable
    IEnumerator Stun()
    {
        SetState(EnemyStates.Stunned);
        GetComponent<EnemyNav>().AdjustSpeed(0);
        gameObject.AddComponent<InteractableObject>();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(stunDuration);
        if(FindObjectOfType<RoomBeam>().heldObject == gameObject)
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
        if(collision.gameObject.GetComponent<InteractableObject>() != null)
        {
            if(collision.gameObject.GetComponent<InteractableObject>().beingLaunched == true)
            {
                RecieveDamage(TaktDamageCalc(collision.gameObject.GetComponent<Rigidbody>()));
                if (currentHealth > 0)
                    StartCoroutine(Stun());
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
        yield return new WaitForSeconds(koAnimTime);
        GetComponent<RoomChecker>().inRoom = false;
        if (FindObjectOfType<RoomHitBox>() != null)
            FindObjectOfType<RoomHitBox>().objsInRoom.Remove(gameObject);
        Destroy(gameObject);
    }
}
