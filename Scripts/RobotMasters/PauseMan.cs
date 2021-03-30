using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMan : MonoBehaviour, ILanguage
{
    public GameObject pauseMenu;
    public GameObject optionsScreen;
    public GameObject confirmLeave;
    public GameObject moveList;
    bool willPause = false;
    bool opDisplay = false;
    bool confirmDisplayOn = false;
    bool movesOn = false;
    CameraMovement cameraMovement;
    LanguageMan languageMan;
    public Text mouseSensText;
    private void Start()
    {
        GetComponentInChildren<AudioMan>().InitializeAudio();
        optionsScreen.SetActive(false);
        cameraMovement = FindObjectOfType<CameraMovement>();
        languageMan = FindObjectOfType<LanguageMan>();
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
    public void DisplayMoveList()
    {
        movesOn = !movesOn;
        if (movesOn == true)
            moveList.SetActive(true);
        else
            moveList.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }
    public void LoadMain()
    {
        FindObjectOfType<SceneMan>().LoadMainMenu();
    }
    public void AdjustMouse(float mouseValue)
    {
        cameraMovement.mouseSensitity = mouseValue;
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
