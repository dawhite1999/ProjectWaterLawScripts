using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SPEarner : MonoBehaviour, ILanguage
{
    public Text openText;
    public UpgradeMan upgradeMan;
    [SerializeField] int skillPoints = 0;
    LanguageMan languageMan;
    [Header("This is to mark which bool in static man gets turned true. level 1 would be 1, etc...")]
    [SerializeField] int earnerNum = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null && StaticMan.spEarnCheck[earnerNum] == false)
        {
            languageMan = FindObjectOfType<LanguageMan>();
            upgradeMan.AddSP(skillPoints);
            StaticMan.spEarnCheck[earnerNum] = true;
            StartCoroutine(SPEarn());
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
