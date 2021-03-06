using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomBeam : MonoBehaviour
{
    //references
    [SerializeField] Transform heldObjectLoc;
    [SerializeField] Transform switchedObjLoc;
    [SerializeField] GameObject roomBeamOrigin;

    Vector3 tempShamblesLoc;
    GameObject heldObject;
    GameObject potSwitchPartner;
    Quaternion heldObjRotation;
    Player player;

    //variables
    bool isUpdating = false;
    [SerializeField] float launchForce;
    public enum GUN_STATE { INACTIVE, ACTIVE, TURNINGOFF }
    public GUN_STATE currentState = GUN_STATE.INACTIVE;
    /////////////////////////////////////////////////////////////////ability variables
    bool radioOn = false;
    bool canRadio = true;
    public float radioTimeMax;
    float radioTimeCurr;
    public float radioCDMax;
    float radioCDCurr;
    public float gammaCDMax;
    float gammaCDCurr;
    public float injectionCDMax;
    float injectionCDCurr;
    public float counterCDMax;
    float counterCDCurr;
    bool gammaUsed = false;
    bool injectionUsed = false;
    bool counterUsed = false;
    /////////////////////////////////////////////////////////////////ability variables

    private void Start()
    {
        player = GetComponent<Player>();
        radioTimeCurr = radioTimeMax;
        radioCDCurr = radioCDMax;
        counterCDCurr = counterCDMax;
        gammaCDCurr = gammaCDMax;
        injectionCDCurr = injectionCDMax;
    }
    private void Update()
    {
        if(player.inRoom == true)
        {
            UpdateInput();
            //for Takt
            CheckGunState();
            Hold();
            //for shambles
            ContinuosRay();
            //for ability cooldowns
            RadioCheck();
            GammaCheck();
            CounterCheck();
            InjectionCheck();
        }
    }

    //Inputs
    private void UpdateInput()
    {
        if (GetComponent<Player>().disableInput == false)
        {
            if (Input.GetKeyDown(player.holdButton)) { Activate(); }
            else if (Input.GetKeyUp(player.holdButton)) { Deactivate(); }
            if (Input.GetKeyDown(player.taktButton) && currentState == GUN_STATE.ACTIVE) { Takt(); }
            if(Input.GetKeyDown(player.shamblesButton)) { Shambles(potSwitchPartner); }
            if (Input.GetKeyDown(player.injectionButton)) { StartCoroutine(InjectionShot()); }
            if (Input.GetKeyDown(player.radioButton)) { RadioKnife(true); }
            if (Input.GetKeyDown(player.gammaButton)) { StartCoroutine(GammaKnife()); }
            if (Input.GetKeyDown(player.shockButton)) { StartCoroutine(CounterShock()); }
        }
    }

    void ContinuosRay()
    {
        Ray roomRay = new Ray(roomBeamOrigin.transform.position, roomBeamOrigin.transform.forward);
        RaycastHit roomRayHit;
        Debug.DrawRay(roomBeamOrigin.transform.position, roomBeamOrigin.transform.forward, Color.blue);

        if (Physics.Raycast(roomRay, out roomRayHit))
        {
            if (roomRayHit.collider.GetComponent<InteractableObject>() != null)
            {
                if(roomRayHit.collider.GetComponent<RoomChecker>().inRoom == true)
                potSwitchPartner = roomRayHit.collider.gameObject;
            }
            else
                potSwitchPartner = null;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////Moving things in Room////////////////////////////////////////////////////////////////////////////////////////
    private void CheckGunState()
    {
        if (currentState == GUN_STATE.ACTIVE)
            ShootRay(true);
        else if (currentState == GUN_STATE.TURNINGOFF)
            ShootRay(false);
    }
    private void Activate() { currentState = GUN_STATE.ACTIVE; }
    private void Deactivate() { currentState = GUN_STATE.TURNINGOFF; }
    void Hold()
    {
        if (heldObject != null)
        {
            if (isUpdating == true && heldObject.GetComponent<InteractableObject>().beingLaunched == false)
            {
                heldObject.transform.position = heldObjectLoc.transform.position;
                heldObject.GetComponent<Rigidbody>().useGravity = false;
                if (heldObject.transform.rotation != heldObjRotation)
                    LetGo();
            }
        }
    }
    public void LetGo()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            isUpdating = false;
            heldObject = null;
        }
    }
    void ShootRay(bool isActive)
    {
        Ray roomRay = new Ray(roomBeamOrigin.transform.position, roomBeamOrigin.transform.forward);
        RaycastHit roomRayHit;
        Debug.DrawRay(roomBeamOrigin.transform.position, roomBeamOrigin.transform.forward, Color.red);

        if (Physics.Raycast(roomRay, out roomRayHit) && isActive == true)
        {
            if (roomRayHit.collider.GetComponent<InteractableObject>() != null)
            {
                if (roomRayHit.collider.GetComponent<InteractableObject>().beingLaunched == false && roomRayHit.collider.GetComponent<RoomChecker>().inRoom == true)
                {
                    isUpdating = true;
                    heldObject = roomRayHit.collider.gameObject;
                    heldObjRotation = heldObject.transform.rotation;
                }
                else if(roomRayHit.collider.GetComponent<RoomChecker>().inRoom == false)
                {
                    if (roomRayHit.collider.GetComponent<Rigidbody>() != null)
                        LetGo();
                    currentState = GUN_STATE.INACTIVE;
                }
            }
        }
        else if (Physics.Raycast(roomRay, out roomRayHit) && isActive == false)
        {
            if (roomRayHit.collider.GetComponent<Rigidbody>() != null)
                LetGo();
            currentState = GUN_STATE.INACTIVE;
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////Moving things in Room////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////////////Room Abilities//////////////////////////////////////////////////////////////////////////////////////////////////
    void Shambles(GameObject itemToSwitch)
    {
        if(itemToSwitch != null)
        {
            tempShamblesLoc = new Vector3(itemToSwitch.transform.position.x, itemToSwitch.transform.position.y, itemToSwitch.transform.position.z);
            itemToSwitch.transform.position = switchedObjLoc.position;
            if(currentState == GUN_STATE.ACTIVE)
            {
                LetGo();
                currentState = GUN_STATE.INACTIVE;
            }
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = new Vector3(tempShamblesLoc.x, tempShamblesLoc.y + 3, tempShamblesLoc.z);
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
        }
    }
    IEnumerator CounterShock()
    {
        if(counterUsed == false)
        {
            player.counterDamage = ((player.maxHealth - player.currentHealth) / 100) + 1;
            player.counterBackground.GetComponent<Image>().color = Color.yellow;
            yield return new WaitForSeconds(player.counterStartUp);
            player.swordHitBox.GetComponent<AttackHitBox>().counterOn = true;
            player.swordHitBox.SetActive(true);
            yield return new WaitForSeconds(player.counterBoxActiveTime);
            player.swordHitBox.GetComponent<AttackHitBox>().counterOn = false;
            player.swordHitBox.SetActive(false);
            player.counterBackground.GetComponent<Image>().color = Color.white;
            counterUsed = true;
        }

    }
    void Takt()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<InteractableObject>().beingLaunched = true;
            heldObject.GetComponent<Rigidbody>().velocity = transform.forward * launchForce;
            heldObject.GetComponent<InteractableObject>().InvokeRePickup();
            LetGo();
        }
    }
    IEnumerator InjectionShot()
    {
        if(injectionUsed == false)
        {
            Ray roomRay = new Ray(roomBeamOrigin.transform.position, roomBeamOrigin.transform.forward);
            RaycastHit roomRayHit;
            Debug.DrawRay(roomBeamOrigin.transform.position, roomBeamOrigin.transform.forward, Color.green);

            player.injectionBackground.GetComponent<Image>().color = Color.yellow;
            yield return new WaitForSeconds(player.injectionStartUp);
            if (Physics.Raycast(roomRay, out roomRayHit))
            {
                if (roomRayHit.collider.GetComponent<Enemy>() != null)
                {
                    if (roomRayHit.collider.GetComponent<RoomChecker>().inRoom == true)
                        roomRayHit.collider.GetComponent<Enemy>().RecieveDamage(player.strength + player.injectionShotBonus);
                }
            }
            injectionUsed = true;
            player.injectionBackground.GetComponent<Image>().color = Color.white;
        }

    }
    void RadioKnife(bool turnOn)
    {
        if(turnOn == true)
        {
            radioOn = true;
            player.radioBackground.GetComponent<Image>().color = Color.yellow;
            player.strength = player.strength + player.radioBonus;
        }
        else
        {
            radioOn = false;
            canRadio = false;
            player.radioBackground.GetComponent<Image>().color = Color.white;
            player.strength = player.strength - player.radioBonus;
        }

    }
    IEnumerator GammaKnife()
    {
        if(gammaUsed == false)
        {
            if (player.swordHitBox.activeSelf == true)
                yield break;
            player.gammaBackground.GetComponent<Image>().color = Color.yellow;
            yield return new WaitForSeconds(player.attackStartUp);
            player.swordHitBox.GetComponent<AttackHitBox>().gammaOn = true;
            player.swordHitBox.SetActive(true);
            yield return new WaitForSeconds(player.hitBoxActiveTime);
            player.swordHitBox.GetComponent<AttackHitBox>().gammaOn = false;
            player.swordHitBox.SetActive(false);
            player.gammaBackground.GetComponent<Image>().color = Color.white;
            gammaUsed = true;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////Room Abilities//////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////////////Ability Checks//////////////////////////////////////////////////////////////////////////////////////////////////
    void RadioCheck()
    {
        if (radioOn == true)
        {
            radioTimeCurr -= Time.deltaTime;
            if (radioTimeCurr <= 0)
                RadioKnife(false);
        }
        else if (canRadio == false)
        {
            radioCDCurr -= Time.deltaTime;
            if (radioCDCurr <= 0)
            {
                radioCDCurr = radioCDMax;
                canRadio = true;
                player.radioBackground.GetComponentInChildren<Text>().text = "Radio Knife";
            }
            player.radioBackground.GetComponentInChildren<Text>().text = Mathf.Round(radioCDCurr).ToString();
        }
    }
    void GammaCheck()
    {
        if(gammaUsed == true)
        {
            gammaCDCurr -= Time.deltaTime;
            player.gammaBackground.GetComponentInChildren<Text>().text = Mathf.Round(gammaCDCurr).ToString();
            if (gammaCDCurr <= 0)
            {
                gammaCDCurr = gammaCDMax;
                gammaUsed = false;
                player.gammaBackground.GetComponentInChildren<Text>().text = "Gamma Knife";
            }

        }
    }
    void InjectionCheck()
    {
        if (injectionUsed == true)
        {
            injectionCDCurr -= Time.deltaTime;
            player.injectionBackground.GetComponentInChildren<Text>().text = Mathf.Round(injectionCDCurr).ToString();
            if (injectionCDCurr <= 0)
            {
                injectionCDCurr = injectionCDMax;
                injectionUsed = false;
                player.injectionBackground.GetComponentInChildren<Text>().text = "Injection Shot";
            }
        }
    }
    void CounterCheck()
    {
        if (counterUsed == true)
        {
            counterCDCurr -= Time.deltaTime;
            player.counterBackground.GetComponentInChildren<Text>().text = Mathf.Round(counterCDCurr).ToString();
            if (counterCDCurr <= 0)
            {
                counterCDCurr = counterCDMax;
                counterUsed = false;
                player.counterBackground.GetComponentInChildren<Text>().text = "Counter Shock";
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////Ability Checks//////////////////////////////////////////////////////////////////////////////////////////////////
}
