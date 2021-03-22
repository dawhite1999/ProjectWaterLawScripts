using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextStageStation : MonoBehaviour, ILanguage
{
    public Text moveText;
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
        if (other.GetComponent<Player>() != null)
        {
            languageMan = FindObjectOfType<LanguageMan>();
            player = other.GetComponent<Player>();
            moveText.gameObject.SetActive(true);
            moveText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.MoveNxtStage, EngOrJap());
            player.canNxtStg = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            moveText.gameObject.SetActive(false);
            player.canNxtStg = false;
        }
    }
}
