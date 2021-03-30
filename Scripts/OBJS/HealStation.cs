using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealStation : MonoBehaviour, ILanguage
{
    [SerializeField] int charges = 3;
    public Text openText;
    LanguageMan languageMan;
    Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            languageMan = FindObjectOfType<LanguageMan>();
            player = other.GetComponent<Player>();
            openText.gameObject.SetActive(true);
            openText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.HealCharges, EngOrJap()) + charges;
            if (charges > 0 && player.currentHealth < player.maxHealth)
            {
                other.GetComponent<Player>().AddHealth();
                charges--;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            openText.gameObject.SetActive(false);
        }
    }

    public bool EngOrJap()
    {
        if (StaticMan.isEnglish == true)
            return true;
        else
            return false;
    }
}
