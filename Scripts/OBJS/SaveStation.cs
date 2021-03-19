using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveStation : MonoBehaviour, ILanguage
{
    public Text openText;
    LanguageMan languageMan;
    Player player;
    public bool EngOrJap()
    {
        if (StaticMan.isEnglish == true)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            languageMan = FindObjectOfType<LanguageMan>();
            player = other.GetComponent<Player>();
            openText.gameObject.SetActive(true);
            openText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.OpenSave, EngOrJap());
            player.canSave = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        openText.gameObject.SetActive(false);
        player.canSave = false;
    }
}
