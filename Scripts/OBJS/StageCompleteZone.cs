using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageCompleteZone : MonoBehaviour, ILanguage
{
    public Text openText;
    public UpgradeMan upgradeMan;
    [SerializeField] int skillPoints = 0;
    LanguageMan languageMan;
    [Header("This is to keep track of if you have beaten the stage already. level 1 would be 1, etc...")]
    [SerializeField] int earnerNum = 0;
    bool enteredOnce = false;
    private void Start()
    {
        if (FindObjectOfType<StatLvlHolder>().stagesComplete > earnerNum)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null && enteredOnce == false)
        {
            languageMan = FindObjectOfType<LanguageMan>();
            upgradeMan.AddSP(skillPoints);
            StartCoroutine(SPEarn());
            //increase the number of stages completed
            FindObjectOfType<StatLvlHolder>().stagesComplete++;
            enteredOnce = true;
        }
    }
    IEnumerator SPEarn()
    {
        openText.gameObject.SetActive(true);
        openText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPEarned, EngOrJap());
        yield return new WaitForSeconds(3);
        if (openText.text == languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPEarned, EngOrJap()))
            openText.gameObject.SetActive(false);
    }
    public bool EngOrJap()
    {
        if (StaticMan.isEnglish == true)
            return true;
        else
            return false;
    }
}
