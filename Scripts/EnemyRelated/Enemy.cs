using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float defense = 0.9f;
    public float walkSpeed;
    public float strength;
    public float attackRate;
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
    }
    protected virtual void Update(){ MakeDecision(); }

    protected virtual void MakeDecision() { }
    public void RecieveDamage(float damageTaken)
    {
        damageTaken *= defense;
        currentHealth = Mathf.RoundToInt(currentHealth - damageTaken);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        Debug.Log("Enenmy health " + currentHealth);
    }
}
