using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMan : MonoBehaviour
{
    public Dictionary<MenuUi, string> englishMenuDict = new Dictionary<MenuUi, string>();
    public Dictionary<MenuUi, string> japaneseMenuDict = new Dictionary<MenuUi, string>();
    public Dictionary<InGameUI, string> englishUIDict = new Dictionary<InGameUI, string>();
    public Dictionary<InGameUI, string> japaneseUIDict = new Dictionary<InGameUI, string>();
    public Dictionary<Tutorial, string> englishTutDict = new Dictionary<Tutorial, string>();
    public Dictionary<Tutorial, string> japaneseTutDict = new Dictionary<Tutorial, string>();
    private void Awake()
    {
        AddEnglish();
        AddJapanese();
    }
    //called by title man
    public void SelectLanguage(bool english)
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
    }
    public enum MenuUi { NewGame, ContinueGame, Options, Exit, Yes, No, QuitConfirm, Language, MouseSensitivity}
    public enum InGameUI { YouDied, OpenSave, Gamma, Radio, Counter, Injection, HitPoints, Stamina, Room, SkillPoints, SaveButton, SaveStatus, Strength, NoSkillPoints, LevelCap, SPCost, MoveNxtStage }
    public enum Tutorial { Room, Shambles, Takt, RoomHealth, Abilities, Hold, Controls}

    public string SetMenuLanguage(MenuUi textToSet, bool isEng)
    {
        if (isEng == true)
            return englishMenuDict[textToSet];
        else
            return japaneseMenuDict[textToSet];
    }
    public string SetInGameSenetence(InGameUI textToSet, bool isEng)
    {
        if (isEng == true)
            return englishUIDict[textToSet];
        else
            return japaneseUIDict[textToSet];
    }
    public string SetTutorialSentence(Tutorial tutorial, bool isEng)
    {
        if (isEng == true)
            return englishTutDict[tutorial];
        else
            return japaneseTutDict[tutorial];
    }

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
        englishMenuDict.Add(MenuUi.MouseSensitivity, "Mouse Sensitivity");
        //in game
        englishUIDict.Add(InGameUI.YouDied, "You Died");
        englishUIDict.Add(InGameUI.OpenSave, "Right Click to open the save menu.");
        englishUIDict.Add(InGameUI.Gamma, "Gamma Knife");
        englishUIDict.Add(InGameUI.Counter, "Couner Shock");
        englishUIDict.Add(InGameUI.HitPoints, "Hit Points");
        englishUIDict.Add(InGameUI.Injection, "Injection Shot");
        englishUIDict.Add(InGameUI.Radio, "Radio Knife");
        englishUIDict.Add(InGameUI.Room, "Room");
        englishUIDict.Add(InGameUI.SaveButton, "Save");
        englishUIDict.Add(InGameUI.SaveStatus, "Game Saved");
        englishUIDict.Add(InGameUI.SkillPoints, "Skill Points");
        englishUIDict.Add(InGameUI.Stamina, "Stamina");
        englishUIDict.Add(InGameUI.Strength, "Strength");
        englishUIDict.Add(InGameUI.NoSkillPoints, "Not enough skill points");
        englishUIDict.Add(InGameUI.LevelCap, "Cannot upgrade further");
        englishUIDict.Add(InGameUI.SPCost, "Skill Points needed ");
        englishUIDict.Add(InGameUI.MoveNxtStage, "Right Click to move on to the next stage");
        //Tutorial
        englishTutDict.Add(Tutorial.Abilities, "Radio Knife: Increases damage dealt for a short time. Press 1 to activate" + "\n" + "Gamma Knife: A powerful attack that bypasses enemy defense. Press 2 to activate." + "\n" + "Injection Shot: A projectile attack. Press 3 to activate." + "\n" + "Counter Shock: An attack that grows in strength the lower your hitpoints get. Press 4 to activate." + "\n" + "Defeat this enemy to move on");
        englishTutDict.Add(Tutorial.Room, "Press and Hold Q to create a room." + "\n" + "While in a room, your abilities activate." + "\n" + "Your abilities will only activate on objects that are inside the room with you." + "\n" + "The following explanations of your abilities will assume both you and the object are in the room");
        englishTutDict.Add(Tutorial.RoomHealth, "Inside a room, you will not recover stamina." + "\n" + "Without stamina, creating the room will use 10% of your life instead." + "\n" + "A room has no size limit, but big rooms will drain your life.");
        englishTutDict.Add(Tutorial.Shambles, "Press E to swap your position with an object." + "\n" + "To swap the positions of one object with another, hold E, look at one object, look at another, and then release E.");
        englishTutDict.Add(Tutorial.Takt, "Press R to launch a held object." + "\n" + "Launched objects will continue to travel outside of your room" + "\n" + "Normally, enemies do not act as interactable objects. You can not pick up, shambles, or takt them. However, if you launch an object at an enemy, the enemy will enter the stunned state. Stunned enemies act as other interactable objects.");
        englishTutDict.Add(Tutorial.Hold, "To hold an item, look at the item, and hold right click");
        englishTutDict.Add(Tutorial.Controls, "W = move forward." + "\n" + "A = move left." + "\n" + "S = move backwards" + "\n" + "D = move right" + "\n" + "Shift + W =  Run" + "\n" + "Spacebar = jump" + "\n" + "Right click = attack");
    }
    void AddJapanese()
    {
        japaneseMenuDict.Add(MenuUi.NewGame, "ニュゲーム");
        japaneseMenuDict.Add(MenuUi.ContinueGame, "続ける");
        japaneseMenuDict.Add(MenuUi.Exit, "出る");
        japaneseMenuDict.Add(MenuUi.Language, "言語");
        japaneseMenuDict.Add(MenuUi.No, "いいえ");
        japaneseMenuDict.Add(MenuUi.Yes, "はい");
        japaneseMenuDict.Add(MenuUi.Options, "オプション");
        japaneseMenuDict.Add(MenuUi.QuitConfirm, "ゲームを出る?");
        //in game
        japaneseUIDict.Add(InGameUI.YouDied, "死んだ");
        //Tutorial
        japaneseTutDict.Add(Tutorial.Abilities, "");
        japaneseTutDict.Add(Tutorial.Room, "");
        japaneseTutDict.Add(Tutorial.RoomHealth, "");
        japaneseTutDict.Add(Tutorial.Shambles, "");
        japaneseTutDict.Add(Tutorial.Takt, "");
        japaneseTutDict.Add(Tutorial.Hold, "");
    }
}

