using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float defense = 0.9f;
    [HideInInspector] public float currentHealth;
    AudioMan audioMan;
    [SerializeField] GameObject roomChecker;
    private void Start()
    {
        currentHealth = maxHealth;
    }
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
