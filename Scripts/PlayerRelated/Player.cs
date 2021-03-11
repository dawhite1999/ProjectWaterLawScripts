using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //variables
    [Header("Stats")]
    public float maxHealth;
    public float maxStamina;
    [HideInInspector] public float currentStamina;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public bool disableInput = false;
    [HideInInspector] public bool inRoom = false;
    public float strength = 0;
    public float hitBoxActiveTime = 0.3f;
    public float counterBoxActiveTime = 0.3f;
    public float injectionStartUp;
    public float counterStartUp;
    public float attackStartUp;
    public float injectionShotBonus = 0;
    public float radioBonus = 0;
    public float gammaBonus = 0;
    [SerializeField] float staminaRecoveryRate = 0;
    [HideInInspector] public float counterDamage = 0;
    [Header("References")]
    public Slider healthBar;
    public Slider staminaBar;
    AudioMan audioMan;
    [HideInInspector] public Animator animator;
    public GameObject swordHitBox;
    public GameObject sword;
    public GameObject swordTip;
    public GameObject radioBackground;
    public GameObject gammaBackground;
    public GameObject injectionBackground;
    public GameObject counterBackground;
    [Header("KeyCodes")]
    [SerializeField] KeyCode attackButton = KeyCode.Q;
    public KeyCode holdButton = KeyCode.Mouse1;
    public KeyCode taktButton = KeyCode.R;
    public KeyCode shamblesButton = KeyCode.E;
    public KeyCode shockButton = KeyCode.Z;
    public KeyCode injectionButton = KeyCode.F;
    public KeyCode radioButton = KeyCode.C;
    public KeyCode gammaButton = KeyCode.X;
    //other variables
    bool attackRight = true;
    Color staminaColor;
    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        staminaBar.value = currentStamina;
        audioMan = FindObjectOfType<AudioMan>();
        animator = GetComponent<Animator>();
        swordHitBox.SetActive(false);
        staminaColor = staminaBar.fillRect.gameObject.GetComponent<Image>().color;
        foreach (Outline item in sword.GetComponentsInChildren<Outline>())
        {
            item.enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(attackButton)) { StartCoroutine(SwordSwing()); }
    }
    //called where ever the player takes dameage
    public void RecieveDamage(float damageTaken)
    {
        currentHealth = currentHealth - damageTaken;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.value = currentHealth;
        }
    }
    //called when activating room, this will drain stamina, and if there is no more stamina, drain 10% of maxhealth
    public void UseStamina()
    {
        if(currentStamina - 10 > 0)
        {
            currentStamina -= 10;
            staminaBar.value = currentStamina;
            if (currentStamina <= 10)
                staminaBar.fillRect.gameObject.GetComponent<Image>().color = Color.red; 
        }
        else
        {
            currentStamina = 0;
            staminaBar.value = 0;
            RecieveDamage(maxHealth * .1f);
        }
        if(IsInvoking("RecoverStamina") == false)
            InvokeRepeating("RecoverStamina", 1, staminaRecoveryRate);
    }
    //called after Stamina is used, this will recover stamina every (recoveryrate seconds) if the player is not in a room.
    public void RecoverStamina()
    {
        if(inRoom == false)
        {
            currentStamina += 5;
            staminaBar.value = currentStamina;
            if (currentStamina > 10)
                staminaBar.fillRect.gameObject.GetComponent<Image>().color = staminaColor;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
                staminaBar.value = currentStamina;
                CancelInvoke("RecoverStamina");
            }
        }
    }
    //called when the player hits the attack button, this will turn the attack hit box on while playing an animation
    IEnumerator SwordSwing()
    {
        if (swordHitBox.activeSelf == true)
            yield break;
        if(attackRight == true) { animator.SetInteger("attackType", 1); }
        else { animator.SetInteger("attackType", 2); }
        attackRight = !attackRight;
        sword.GetComponentInChildren<TrailRenderer>().emitting = true;
        yield return new WaitForSeconds(attackStartUp);
        animator.SetInteger("attackType", 0);
        swordHitBox.SetActive(true);
        yield return new WaitForSeconds(hitBoxActiveTime);
        swordHitBox.SetActive(false);
        sword.GetComponentInChildren<TrailRenderer>().emitting = false;
        if (swordHitBox.transform.GetChild(0).gameObject.activeSelf == true)
            swordHitBox.transform.GetChild(0).gameObject.SetActive(false);
    }
}
