using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMan : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsScreen;
    public GameObject confirmLeave;
    public GameObject moveList;
    bool willPause = false;
    bool opDisplay = false;
    bool confirmDisplayOn = false;
    bool movesOn = false;

    private void Start()
    {
        GetComponentInChildren<AudioMan>().InitializeAudio();
        optionsScreen.SetActive(false);
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
            optionsScreen.SetActive(true);
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
}
