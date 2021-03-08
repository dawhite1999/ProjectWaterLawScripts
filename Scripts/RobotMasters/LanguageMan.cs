using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMan : MonoBehaviour
{
    public Dictionary<MenuUi, string> englishMenuDict = new Dictionary<MenuUi, string>();
    public Dictionary<MenuUi, string> japaneseMenuDict = new Dictionary<MenuUi, string>();
    public Dictionary<InGameUI, string> englishUIDict = new Dictionary<InGameUI, string>();
    public Dictionary<InGameUI, string> japaneseUIDict = new Dictionary<InGameUI, string>();
    private void Awake()
    {
        AddEnglish();
        AddJapanese();
    }
    /*public void SelectLanguage(bool english)
    {
        if (english == true)
        {
            StaticMan.isEnglish = true;
            FindObjectOfType<TitleMan>().englishCheck.SetActive(true);
            FindObjectOfType<TitleMan>().japaneseCheck.SetActive(false);
        }
        else
        {
            StaticMan.isEnglish = false;
            FindObjectOfType<TitleMan>().englishCheck.SetActive(false);
            FindObjectOfType<TitleMan>().japaneseCheck.SetActive(true);
        }
    }*/
    public enum MenuUi
    {
        NewGame,
        ContinueGame,
        Options,
        Exit,
        Yes,
        No,
        QuitConfirm,
        Language,
    }
    public enum InGameUI
    {
        YouDied
    }
    /*public string SetMenuLanguage(MenuUi textToSet)
    {
        if (StaticMan.isEnglish == true)
            return englishMenuDict[textToSet];
        else
            return japaneseMenuDict[textToSet];
    }*/
    void AddEnglish()
    {
        //menu
        englishMenuDict.Add(MenuUi.NewGame, "New Game");
        englishMenuDict.Add(MenuUi.ContinueGame, "Resume");
        englishMenuDict.Add(MenuUi.Exit, "Exit");
        englishMenuDict.Add(MenuUi.Language, "Language");
        englishMenuDict.Add(MenuUi.No, "No");
        englishMenuDict.Add(MenuUi.Yes, "Yes");
        englishMenuDict.Add(MenuUi.Options, "Options");
        englishMenuDict.Add(MenuUi.QuitConfirm, "Are you sure you want to quit?");
        //in game
        englishUIDict.Add(InGameUI.YouDied, "You Died");
    }
    void AddJapanese()
    {
        japaneseMenuDict.Add(MenuUi.NewGame, "新しいゲーム");
        japaneseMenuDict.Add(MenuUi.ContinueGame, "続ける");
        japaneseMenuDict.Add(MenuUi.Exit, "出る");
        japaneseMenuDict.Add(MenuUi.Language, "言語");
        japaneseMenuDict.Add(MenuUi.No, "いいや");
        japaneseMenuDict.Add(MenuUi.Yes, "はい");
        japaneseMenuDict.Add(MenuUi.Options, "オプション");
        japaneseMenuDict.Add(MenuUi.QuitConfirm, "ゲームを出る?");
        //in game
        japaneseUIDict.Add(InGameUI.YouDied, "死んだ");
    }
}

