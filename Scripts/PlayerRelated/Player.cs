using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider healthBar;
    AudioMan audioMan;
    //variables
    public float maxHealth;
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
    [HideInInspector] public float counterDamage = 0;
    public GameObject swordHitBox;
    [SerializeField] KeyCode attackButton = KeyCode.Q;
    public KeyCode holdButton = KeyCode.Mouse1;
    public KeyCode taktButton = KeyCode.R;
    public KeyCode shamblesButton = KeyCode.E;
    public KeyCode shockButton = KeyCode.Z;
    public KeyCode injectionButton = KeyCode.F;
    public KeyCode radioButton = KeyCode.C;
    public KeyCode gammaButton = KeyCode.X;
    public GameObject radioBackground;
    public GameObject gammaBackground;
    public GameObject injectionBackground;
    public GameObject counterBackground;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        audioMan = FindObjectOfType<AudioMan>();
        swordHitBox.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(attackButton)) { StartCoroutine(SwordSwing()); }
    }
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
    IEnumerator SwordSwing()
    {
        if (swordHitBox.activeSelf == true)
            yield break;
        yield return new WaitForSeconds(attackStartUp);
        swordHitBox.SetActive(true);
        yield return new WaitForSeconds(hitBoxActiveTime);
        swordHitBox.SetActive(false);
    }
}
