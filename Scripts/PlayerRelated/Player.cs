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
    public float strength = 0;
    [HideInInspector] public float hitBoxActiveTime = 0.3f;
    [HideInInspector] public float counterBoxActiveTime = 0.3f;
    [HideInInspector] public float injectionStartUp;
    [HideInInspector] public float counterStartUp;
    [HideInInspector] public float attackStartUp;
    public float injectionShotBonus = 0;
    public float radioBonus = 0;
    public float gammaBonus = 0;
    public float counterMultiplier = 1.5f;
    public bool isAttacking = false;
    [SerializeField] float staminaRecoveryRate = 0;
    [HideInInspector] public float counterDamage = 0;
    [Header("References")]
    public GameObject swordHitBox;
    public GameObject sword;
    public GameObject swordTip;
    [HideInInspector] public Slider healthBar;
    [HideInInspector] public Slider staminaBar;
    [HideInInspector] public AudioMan audioMan;
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObject radioBackground;
    [HideInInspector] public GameObject gammaBackground;
    [HideInInspector] public GameObject injectionBackground;
    [HideInInspector] public GameObject counterBackground;
    [HideInInspector] public GameObject saveScreen;
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
    [HideInInspector] public bool disableInput = false;
    [HideInInspector] public bool inRoom = false;
    bool isDead = false;
    [HideInInspector] public bool canSave = false;
    bool saveOn = false;
    [HideInInspector] public bool canNxtStg = false;

    private void Start()
    {
        radioBackground = GameObject.Find("RadioKnifeBack");
        gammaBackground = GameObject.Find("GammaKnifeBack");
        injectionBackground = GameObject.Find("InjectionShotBack");
        counterBackground = GameObject.Find("CounterShockBack");
        saveScreen = GameObject.Find("SaveScreen");
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
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
        InitiateStats();
    }
    //Gets the levels from static man and changes the stats according to the levels
    void InitiateStats()
    {
        //this is here just so the stats dont get changed when you are in the sample scene
        if (GameObject.Find("TestZone") != null)
        {
            GameObject.Find("SaveScreen").SetActive(false);
            return;
        }
        StatLvlHolder statHolder = GetComponent<StatLvlHolder>();
        UpgradeMan upgradeMan = FindObjectOfType<UpgradeMan>();
        //get the levels
        statHolder.gammaLevel = StaticMan.gammaLvl;
        statHolder.radioLevel = StaticMan.radioLvl;
        statHolder.counterLevel = StaticMan.counterLvl;
        statHolder.injectionLevel = StaticMan.injectionLvl;
        statHolder.roomLevel = StaticMan.roomLvl;
        statHolder.hPLevel = StaticMan.hpLvl;
        statHolder.staminaLevel = StaticMan.staminaLvl;
        statHolder.strengthLevel = StaticMan.strengthLvl;
        statHolder.skillPoints = StaticMan.skillPoints;

        //sets the stats according to the level
        upgradeMan.InitializeUpgrades();
        upgradeMan.ChangeCTRStats();
        upgradeMan.ChangeGMAStats();
        upgradeMan.ChangeHPStats();
        upgradeMan.ChangeINJStats();
        upgradeMan.ChangeRDOStats();
        upgradeMan.ChangeRMStats();
        upgradeMan.ChangeSTMStats();
        upgradeMan.ChangeSTRStats();
        GameObject.Find("SaveScreen").SetActive(false);
    }
    private void Update()
    {
        if (disableInput == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                FindObjectOfType<PauseMan>().Pause();
            if (Input.GetKeyDown(attackButton)) { StartCoroutine(SwordSwing()); }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (canSave == true)
                {
                    TurnOnSave();
                    return;
                }
                if(canNxtStg == true)
                {
                    MoveToNextStage();
                }
            }
        }
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
            if(isDead == false)
                StartCoroutine(Death());
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
        if(isAttacking == false)
        {
            isAttacking = true;
            attackRight = !attackRight;
            if (attackRight == true) { animator.SetInteger("attackType", 1); }
            else { animator.SetInteger("attackType", 2); }
            sword.GetComponentInChildren<TrailRenderer>().emitting = true;
            yield return new WaitForSeconds(attackStartUp);
            audioMan.PlayPlayerClip(AudioMan.PlayerClipNames.Swoosh);
            animator.SetInteger("attackType", 0);
            swordHitBox.SetActive(true);
            yield return new WaitForSeconds(hitBoxActiveTime);
            swordHitBox.SetActive(false);
            sword.GetComponentInChildren<TrailRenderer>().emitting = false;
            if (swordHitBox.transform.GetChild(0).gameObject.activeSelf == true)
                swordHitBox.transform.GetChild(0).gameObject.SetActive(false);
            isAttacking = false;
        }
    }
    //called when hp hits 0
    IEnumerator Death()
    {
        disableInput = true;
        isDead = true;
        SceneMan sceneMan = FindObjectOfType<SceneMan>();
        sceneMan.PlayDeathFade();
        yield return new WaitForSeconds(5);
        sceneMan.LoadMainMenu();
    }
    //called when you right click inside a save station
    void TurnOnSave()
    {
        saveOn = !saveOn;
        if (saveOn == true)
        {
            saveScreen.SetActive(true);
            FindObjectOfType<SaveStation>().openText.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            saveScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    //called when you right click inside a next stage station
    void MoveToNextStage()
    {
        //disable input
        disableInput = true;
        FindObjectOfType<SceneMan>().LoadStage();
    }

    public void AddHealth()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }
}
