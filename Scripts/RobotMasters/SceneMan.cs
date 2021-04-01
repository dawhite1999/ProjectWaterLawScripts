using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneMan : MonoBehaviour, ILanguage
{
    GameObject fadeScreen;

    private void Start()
    {
        fadeScreen = GameObject.Find("FadeScreen");
        StartCoroutine(SceneFade(false, ""));
    }
    public void LoadMainMenu()
    {
        StartCoroutine(SceneFade(true, "Title"));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator SceneFade(bool fadeOut, string scene)
    {
        if (fadeOut == true)
        {
            fadeScreen.SetActive(true);
            fadeScreen.GetComponent<Animator>().Play("FadeOut");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            yield return new WaitForSeconds(.4f);
            if (scene != "")
                SceneManager.LoadScene(scene);
        }
        else
        {
            fadeScreen.GetComponent<Animator>().Play("FadeIn");
            yield return new WaitForSeconds(.4f);
            fadeScreen.SetActive(false);
        }
    }
    public void PlayDeathFade()
    {
        fadeScreen.SetActive(true); 
        fadeScreen.GetComponentInChildren<Text>().text = FindObjectOfType<LanguageMan>().SetInGameSenetence(LanguageMan.InGameUI.YouDied, EngOrJap());
        fadeScreen.GetComponent<Animator>().Play("DeathFade");
    }
    public void NewGame()
    {
        StaticMan.gammaLvl = 0;
        StaticMan.counterLvl = 0;
        StaticMan.hpLvl = 0;
        StaticMan.injectionLvl = 0;
        StaticMan.radioLvl = 0;
        StaticMan.roomLvl = 0;
        StaticMan.skillPoints = 0;
        StaticMan.stagesComplete = 0;
        StaticMan.staminaLvl = 0;
        StaticMan.strengthLvl = 0;
        LoadStage();
    }

    public void LoadStage()
    {
        switch(StaticMan.stagesComplete)
        {
            case 0:
                StartCoroutine(SceneFade(true, "Tutorial"));
                break;
            case 1:
                StartCoroutine(SceneFade(true, "Level1"));
                break;
            case 2:
                StartCoroutine(SceneFade(true, "Level2"));
                break;
            case 3:
                StartCoroutine(SceneFade(true, "Level3"));
                break;
            case 4:
                StartCoroutine(SceneFade(true, "Level4"));
                break;
            case 5:
                StartCoroutine(SceneFade(true, "Level5"));
                break;
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
