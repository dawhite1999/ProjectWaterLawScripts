using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleMan : MonoBehaviour, ILanguage
{
    public GameObject englishCheck;
    public GameObject japaneseCheck;
    public GameObject optionsScreen;
    public GameObject confirmLeave;
    public GameObject confirmNGLeave;
    public Slider mouseSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Text language;
    public Text confirm;
    public Text confirmNG;
    public Text errorText;
    [SerializeField] Button[] uiButtons = new Button[0];
    SceneMan sceneMan;
    LanguageMan languageMan;
    AudioMan audioMan;
    bool opDisplay = false;
    bool confirmDisplayOn = false;
    bool confirmNGDisplayOn = false;
    bool tempLanguage = false;
    private void Awake()
    {
        GetComponent<StatLvlHolder>().LoadSave();
    }
    // Start is called before the first frame update
    void Start()
    {
        //find stuff
        audioMan = FindObjectOfType<AudioMan>();
        audioMan.InitializeAudio();
        sceneMan = FindObjectOfType<SceneMan>();
        //FindObjectOfType<AudioMan>().PlayBGM(AudioMan.BGMClipNames.Title);
        languageMan = FindObjectOfType<LanguageMan>();
        tempLanguage = StaticMan.isEnglish;
        SetUiText();
        audioMan.InitiateVolume(PlayerPrefs.GetFloat("MusicVolume", 1), "BGMVolume", musicSlider);
        audioMan.InitiateVolume(PlayerPrefs.GetFloat("EffectsVolume", 1), "SFXVolume", sfxSlider);
        StaticMan.mouseSens = PlayerPrefs.GetFloat("MouseSensitivity", 100);
        mouseSlider.value = StaticMan.mouseSens;
        //turn stuff off
        optionsScreen.SetActive(false);
        confirmLeave.SetActive(false);
        englishCheck.SetActive(false);
        japaneseCheck.SetActive(false);
        if (tempLanguage == true)
            englishCheck.SetActive(true);
        else
            japaneseCheck.SetActive(true);
    }
    public void DisplayOptions()
    {
        opDisplay = !opDisplay;
        if (opDisplay == true)
            optionsScreen.SetActive(true);
        else
            optionsScreen.SetActive(false);
        if (tempLanguage != StaticMan.isEnglish)
        {
            sceneMan.LoadMainMenu();
        }
        audioMan.PlayOtherClip(AudioMan.OtherClipNames.Blip);
    }
    public void ConfirmLeave()
    {
        confirmDisplayOn = !confirmDisplayOn;
        if (confirmDisplayOn == true)
            confirmLeave.SetActive(true);
        else
            confirmLeave.SetActive(false);
        audioMan.PlayOtherClip(AudioMan.OtherClipNames.Blip);
    }
    public void Leave()
    {
        sceneMan.ExitGame();
    }
    void SetUiText()
    {
        language.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Language, EngOrJap());
        confirm.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.QuitConfirm, EngOrJap());
        confirmNG.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.NewGameConfirm, EngOrJap());
        foreach (Button button in uiButtons)
        {
            switch(button.name)
            {
                case "Resume":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.ContinueGame, EngOrJap());
                    break;
                case "Options":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Options, EngOrJap());
                    break;
                case "NewGame":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.NewGame, EngOrJap());
                    break;
                case "Quit":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Exit, EngOrJap());
                    break;
                case "Yes":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Yes, EngOrJap());
                    break;
                case "No":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.No, EngOrJap());
                    break;
            }
        }
    }
    public void ResumeCheck()
    {
        if (StaticMan.stagesComplete == 0)
            StartCoroutine(DisplayError());
        else sceneMan.LoadStage();
        audioMan.PlayOtherClip(AudioMan.OtherClipNames.Blip);
    }
    public void ConfirmNewGame()
    {
        confirmNGDisplayOn = !confirmNGDisplayOn;
        if (confirmNGDisplayOn == true)
            confirmNGLeave.SetActive(true);
        else
            confirmNGLeave.SetActive(false);
        audioMan.PlayOtherClip(AudioMan.OtherClipNames.Blip);
    }
    public void NewGame()
    {
        SaveMan.DeleteSave();
        GetComponent<StatLvlHolder>().ResetProgress();
        sceneMan.LoadStage();
        audioMan.PlayOtherClip(AudioMan.OtherClipNames.Blip);
    }
    public void AdjustMouse(float mouseValue)
    {
        StaticMan.mouseSens = mouseValue;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseValue);
    }
    IEnumerator DisplayError()
    {
        errorText.gameObject.SetActive(true);
        errorText.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.NoSave, EngOrJap());
        yield return new WaitForSeconds(3);
        errorText.gameObject.SetActive(false);
    }
    public void SwitchLanguage(bool isEng)
    {
        if(isEng == true && EngOrJap() == false)
        {
            StaticMan.isEnglish = true;
            sceneMan.LoadMainMenu();
        }
        else if(isEng == false && EngOrJap() == true)
        {
            StaticMan.isEnglish = false;
            sceneMan.LoadMainMenu();
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
