using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialZone : MonoBehaviour, ILanguage
{
    public GameObject tutorLine;
    public LanguageMan languageMan;
    [SerializeField] int helpfulText = 0;
    public EnemyNav enemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>()!= null)
        {
            tutorLine.SetActive(true);
            switch (helpfulText)
            {
                case 0:
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.Controls, EngOrJap());
                    break;
                case 1:
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.Room, EngOrJap());
                    break;
                case 2:
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.RoomHealth, EngOrJap());
                    break;
                case 3:
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.Hold, EngOrJap());
                    break;
                case 4:
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.Shambles, EngOrJap());
                    break;
                case 5:
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.Takt, EngOrJap());
                    break;
                case 6:
                    if(enemy != null)
                        enemy.AdjustSpeed(3.5f);
                    tutorLine.GetComponentInChildren<Text>().text = languageMan.SetTutorialSentence(LanguageMan.Tutorial.Abilities, EngOrJap());
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            tutorLine.SetActive(false);
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
