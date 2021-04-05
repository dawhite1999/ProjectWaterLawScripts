using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMan : MonoBehaviour, ILanguage
{
    public GameObject pauseMenu;
    public GameObject optionsScreen;
    public GameObject confirmLeave;
    public Slider mouseSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    bool willPause = false;
    bool opDisplay = false;
    bool confirmDisplayOn = false;
    LanguageMan languageMan;
    public Text mouseSensText;
    public GameObject[] buttons;
    private void Start()
    {
        if (FindObjectOfType<TitleMan>() == true)
            return;
        GetAudioStuff();
        optionsScreen.SetActive(false);
        languageMan = FindObjectOfType<LanguageMan>();
        if(FindObjectOfType<CameraMovement>() != null)
            FindObjectOfType<CameraMovement>().InitializeMouse();
        //setting texts
        foreach (GameObject item in buttons)
        {
            switch(item.name)
            {
                case "Resume":
                    item.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.ContinueGame, EngOrJap());
                    break;
                case "Options":
                    item.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Options, EngOrJap());
                    break;
                case "Quit":
                    item.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Exit, EngOrJap());
                    break;
                case "Yes":
                    item.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Yes, EngOrJap());
                    break;
                case "No":
                    item.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.No, EngOrJap());
                    break;
            }
        }
        confirmLeave.transform.GetChild(0).GetComponent<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.QuitConfirm, EngOrJap());
        if (FindObjectOfType<TitleMan>() == null)
            pauseMenu.SetActive(false);
        else
            optionsScreen.SetActive(false);
    }
    //This is here because there is a weird error IDK how to fix. It's called by title man at start.
    public void GetAudioStuff()
    {
        GetComponentInChildren<AudioMan>().InitializeAudio();
        GetComponentInChildren<AudioMan>().InitiateVolume(PlayerPrefs.GetFloat("MusicVolume", 1), "BGMVolume", musicSlider);
        GetComponentInChildren<AudioMan>().InitiateVolume(PlayerPrefs.GetFloat("EffectsVolume", 1), "SFXVolume", sfxSlider);
    }
    public void Pause()
    {
        willPause = !willPause;
        FindObjectOfType<Player>().disableInput = willPause;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (willPause == true)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            GetComponentInChildren<AudioMan>().PauseAllSFX();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            if (opDisplay == true)
                DisplayOptions();
            if (confirmDisplayOn == true)
                ConfirmLeave();
            pauseMenu.SetActive(false);
            GetComponentInChildren<AudioMan>().PauseAllSFX();
        }
    }
    public void DisplayOptions()
    {
        opDisplay = !opDisplay;
        if (opDisplay == true)
        {
            optionsScreen.SetActive(true);
            mouseSensText.text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.MouseSensitivity, EngOrJap());
        }
        else
            optionsScreen.SetActive(false);
    }
    public void ConfirmLeave()
    {
        confirmDisplayOn = !confirmDisplayOn;
        if (confirmDisplayOn == true)
            confirmLeave.SetActive(true);
        else
            confirmLeave.SetActive(false);
    }
    public void LoadMain()
    {
        Pause();
        FindObjectOfType<SceneMan>().LoadMainMenu();
    }
    public void AdjustMouse(float mouseValue)
    {
        StaticMan.mouseSens = mouseValue;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseValue);
    }
    public bool EngOrJap()
    {
        if (StaticMan.isEnglish == true)
            return true;
        else
            return false;
    }
}
