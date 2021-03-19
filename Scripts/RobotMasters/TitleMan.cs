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
    public Text language;
    public Text confirm;
    [SerializeField] Button[] uiButtons = new Button[0];
    SceneMan sceneMan;
    LanguageMan languageMan;
    bool opDisplay = false;
    bool confirmDisplayOn = false;
    bool tempLanguage = false;
    private void Awake()
    {
        GetComponent<StatLvlHolder>().LoadSave();
    }
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioMan>().InitializeAudio();
        sceneMan = FindObjectOfType<SceneMan>();
        //FindObjectOfType<AudioMan>().PlayBGM(AudioMan.BGMClipNames.Title);
        languageMan = FindObjectOfType<LanguageMan>();
        tempLanguage = StaticMan.isEnglish;
        SetUiText();
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
    }
    public void ConfirmLeave()
    {
        confirmDisplayOn = !confirmDisplayOn;
        if (confirmDisplayOn == true)
            confirmLeave.SetActive(true);
        else
            confirmLeave.SetActive(false);
    }
    public void Leave()
    {
        sceneMan.ExitGame();
    }
    void SetUiText()
    {
        language.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Language, EngOrJap());
        confirm.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.QuitConfirm, EngOrJap());

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
                case "MoveList":
                    //button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.move, EngOrJap());
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
    
    public bool EngOrJap()
    {
        if (StaticMan.isEnglish == true)
            return true;
        else
            return false;
    }
}
