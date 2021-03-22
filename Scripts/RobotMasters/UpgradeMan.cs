using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMan : MonoBehaviour, ILanguage
{
    [SerializeField] GameObject[] buttons = new GameObject[0];
    [SerializeField] Text skillPointText;
    [SerializeField] Text eventText;
    [SerializeField] Text descriptionText;
    [SerializeField] Text spCostText;
    [SerializeField] Text nextLvlDescText;
    Player player;
    RoomBeam roomBeam;
    Room room;
    LanguageMan languageMan;
    StatLvlHolder statLvlHolder;
    enum NextLevelStats { CoolDown, DamageBonus }
    enum NLRoomStats { ExpandRate, DamageThreshold, RoomActive}
    [SerializeField] int gammaCap;
    [SerializeField] int radioCap;
    [SerializeField] int counterCap;
    [SerializeField] int injectionCap;
    [SerializeField] int roomCap;
    [SerializeField] int strengthCap;
    [SerializeField] int hPCap;
    [SerializeField] int staminaCap;
    int spCost;
    string tempAbilityName;
    public bool EngOrJap()
    {
        if (StaticMan.isEnglish == true)
            return true;
        else
            return false;
    }
    //Called by level up to save the local stat level to static man
    void SetStaticStat(int localStat, int staticStat)
    {
        staticStat = localStat;
        StaticMan.skillPoints = statLvlHolder.skillPoints;
    }
    //called in player at start
    public void InitializeUpgrades()
    {
        player = FindObjectOfType<Player>();
        room = FindObjectOfType<Room>();
        roomBeam = FindObjectOfType<RoomBeam>();
        languageMan = FindObjectOfType<LanguageMan>();
        statLvlHolder = FindObjectOfType<StatLvlHolder>();
        SetLanguage();
    }
    //Sets the text of the ui 
    void SetLanguage()
    {
        foreach (GameObject button in buttons)
        {
            switch(button.name)
            {
                case "GammaKnifeUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Gamma, EngOrJap());
                    break;
                case "RadioKnifeUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Radio, EngOrJap());
                    break;
                case "CounterShockUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Counter, EngOrJap());
                    break;
                case "InjectionShotUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Injection, EngOrJap());
                    break;
                case "HitPointsUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.HitPoints, EngOrJap());
                    break;
                case "StaminaUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Stamina, EngOrJap());
                    break;
                case "RoomUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Room, EngOrJap());
                    break;
                case "SaveButton":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SaveButton, EngOrJap());
                    break;
                case "CloseButton":
                    button.GetComponentInChildren<Text>().text = languageMan.SetMenuLanguage(LanguageMan.MenuUi.Exit, EngOrJap());
                    break;
                case "StrengthUp":
                    button.GetComponentInChildren<Text>().text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.Strength, EngOrJap());
                    break;
            }
        }
        skillPointText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SkillPoints, EngOrJap()) + " " + statLvlHolder.skillPoints;
        eventText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SaveStatus, EngOrJap());
        eventText.gameObject.SetActive(false);
        descriptionText.text = "";
        nextLvlDescText.text = "";
        spCostText.text = "";
    }

    //Called when cursor hovers over ability Displays the current stats, and next stats of a certain ability
    public void SetDescription(GameObject skillButton)
    {
        switch(skillButton.name)
        {
            case "GammaKnifeUp":
                DisplayAbilStats(roomBeam.gammaCDMax, player.gammaBonus, statLvlHolder.gammaLevel);
                DisplayNxtAbilSts(GammaNextLevel(statLvlHolder.gammaLevel, NextLevelStats.CoolDown), GammaNextLevel(statLvlHolder.gammaLevel, NextLevelStats.DamageBonus), statLvlHolder.gammaLevel, gammaCap);
                break;
            case "RadioKnifeUp":
                DisplayAbilStats(roomBeam.radioCDMax, player.radioBonus, statLvlHolder.radioLevel);
                DisplayNxtAbilSts(RadioNextLevel(statLvlHolder.radioLevel, NextLevelStats.CoolDown), RadioNextLevel(statLvlHolder.radioLevel, NextLevelStats.DamageBonus), statLvlHolder.radioLevel, radioCap);
                break;
            case "CounterShockUp":
                DisplayAbilStats(roomBeam.counterCDMax, player.counterMultiplier, statLvlHolder.counterLevel);
                DisplayNxtAbilSts(CounterNextLevel(statLvlHolder.counterLevel, NextLevelStats.CoolDown), CounterNextLevel(statLvlHolder.counterLevel, NextLevelStats.DamageBonus), statLvlHolder.counterLevel, counterCap);
                break;
            case "InjectionShotUp":
                DisplayAbilStats(roomBeam.injectionCDMax, player.injectionShotBonus, statLvlHolder.injectionLevel);
                DisplayNxtAbilSts(InjectionNextLevel(statLvlHolder.injectionLevel + 1, NextLevelStats.CoolDown), InjectionNextLevel(statLvlHolder.injectionLevel + 1, NextLevelStats.DamageBonus), statLvlHolder.injectionLevel, injectionCap);
                break;
            case "HitPointsUp":
                DisplayStats(statLvlHolder.hPLevel, player.maxHealth, "Hit Points ");
                DisplayNxtStats(statLvlHolder.hPLevel, HitPointsNL(statLvlHolder.hPLevel), "Hit Points ", hPCap);
                spCostText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPCost, EngOrJap()) + SetSkillPoints(statLvlHolder.hPLevel).ToString();
                break;
            case "StaminaUp":
                DisplayStats(statLvlHolder.staminaLevel, player.maxStamina, "Stamina ");
                DisplayNxtStats(statLvlHolder.staminaLevel, StaminaNL(statLvlHolder.staminaLevel), "Stamina ", staminaCap);
                spCostText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPCost, EngOrJap()) + SetSkillPoints(statLvlHolder.staminaLevel).ToString();
                break;
            case "RoomUp":
                spCostText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPCost, EngOrJap()) + SetSkillPoints(statLvlHolder.roomLevel).ToString();
                descriptionText.text = "Current Level Stats" + "\n" + "Lvl " + statLvlHolder.roomLevel + "\n" + "Damage Threshold " + room.damageThreshold + "m" + "\n" + "Expand Rate " + room.growthRate + "\n" + "Room Active Time " + room.roomTimeActiveMax + "s";
                if(EngOrJap() == true)
                {
                    if(statLvlHolder.roomLevel + 1 > roomCap)
                        nextLvlDescText.text = "Max level reached";
                    else
                        nextLvlDescText.text = "Next Level Stats" + "\n" + "Lvl " + statLvlHolder.roomLevel + "\n" + "Damage Threshold " + RoomNL(statLvlHolder.roomLevel + 1, NLRoomStats.DamageThreshold) + "m" + "\n" + "Expand Rate " + RoomNL(statLvlHolder.roomLevel + 1, NLRoomStats.ExpandRate) + "\n" + "Room Active Time " + RoomNL(statLvlHolder.roomLevel + 1, NLRoomStats.RoomActive) + "s";
                }
                else
                {
                    if (statLvlHolder.roomLevel + 1 > roomCap)
                        nextLvlDescText.text = "Max level reached";
                    else
                        nextLvlDescText.text = "Next Level Stats" + "\n" + "Lvl " + statLvlHolder.roomLevel + "\n" + "Damage Threshold " + RoomNL(statLvlHolder.roomLevel + 1, NLRoomStats.DamageThreshold) + "m" + "\n" + "Expand Rate " + RoomNL(statLvlHolder.roomLevel + 1, NLRoomStats.ExpandRate) + "\n" + "Room Active Time " + RoomNL(statLvlHolder.roomLevel + 1, NLRoomStats.RoomActive) + "s";
                }  
                break;
            case "StrengthUp":
                spCostText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPCost, EngOrJap()) + SetSkillPoints(statLvlHolder.strengthLevel).ToString();
                DisplayStats(statLvlHolder.strengthLevel, player.strength, "Strength ");
                DisplayNxtStats(statLvlHolder.strengthLevel, StrengthNL(statLvlHolder.strengthLevel), "Strength ", strengthCap);
                break;
        }
        tempAbilityName = skillButton.name;
    }
    //Called when cursor leaves ability
    public void ClearDescription()
    {
        spCostText.text = "";
        descriptionText.text = "";
        nextLvlDescText.text = "";
        tempAbilityName = "";
    }
    //Called by SetDescription. sets current ability stats 
    void DisplayAbilStats(float coolDown, float damageBonus, int level)
    {
        if(EngOrJap() == true)
            descriptionText.text = "Current Level Stats" + "\n" + "Lvl " + level + "\n" + "Cooldown " + coolDown + "\n" + "Damage Bonus " + damageBonus;
        else
            descriptionText.text = "Current Level Stats" + "\n" + "Cooldown " + coolDown + "\n" + "Damage Bonus " + damageBonus;
    }
    //Called by SetDescription. sets next ability stats 
    void DisplayNxtAbilSts(float coolDown, float damageBonus, int level, int levelCap)
    {
        if (EngOrJap() == true)
        {
            if(level + 1 > levelCap)
                nextLvlDescText.text = "Max level reached";
            else
                nextLvlDescText.text = "Next Level Stats" + "\n" + "Lvl " + level + 1 + "\n" + "Cooldown " + coolDown + "\n" + "Damage Bonus " + damageBonus;
        }
        else
        {
            if (level + 1 > levelCap)
                nextLvlDescText.text = "Max level reached";
            else
                nextLvlDescText.text = "Next Level Stats" + "\n" + "Cooldown " + coolDown + "\n" + "Damage Bonus " + damageBonus;
        }
        if(level + 1 > levelCap)
            spCostText.text = "Max";
        else
            spCostText.text = languageMan.SetInGameSenetence(LanguageMan.InGameUI.SPCost, EngOrJap()) + SetSkillPoints(level + 1).ToString();
    }
    //Called by SetDescription. sets current ability stats 
    void DisplayStats(int level, float stat, string statName)
    {
        if(EngOrJap() == true)
            descriptionText.text = "Current Level Stats" + "\n" + "Lvl " + level + "\n" + statName + stat;
        else
            descriptionText.text = "Current Level Stats" + "\n" + "Lvl " + level + "\n" + statName + stat;
    }
    //Called by SetDescription. sets next ability stats 
    void DisplayNxtStats(int level, float stat, string statName, int levelCap)
    {
        if (EngOrJap() == true)
        {
            if (level + 1 > levelCap)
                nextLvlDescText.text = "Max level reached";
            else
                descriptionText.text = "Next Level Stats" + "\n" + "Lvl " + level + 1 + "\n" + statName + stat;
        }
        else
        {
            if (level + 1 > levelCap)
                nextLvlDescText.text = "Max level reached";
            else
                descriptionText.text = "Current Level Stats" + "\n" + "Lvl " + level + 1 + "\n" + statName + stat;
        }
    }
    //Called by SetDescription the skill point cost for each level
    int SetSkillPoints(int level)
    {
        switch(level)
        {
            case 0:
                spCost = 1;
                break;
            case 1:
                spCost = 3;
                break;
            case 2:
                spCost = 5;
                break;
            case 3:
                spCost = 7;
                break;
            case 4:
                spCost = 9;
                break;
            case 5:
                spCost = 11;
                break;
            case 6:
                spCost = 13;
                break;
            case 7:
                spCost = 15;
                break;
        }
        return spCost;
    }
    
    //Called by SetDescription and level up sets next level stat for gamma
    float GammaNextLevel(int level, NextLevelStats stat)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                if (stat == NextLevelStats.CoolDown) { tempValue = 55; } else { tempValue = 60; }
                break;
            case 2:
                if (stat == NextLevelStats.CoolDown) { tempValue = 50; } else { tempValue = 75; }
                break;
            case 3:
                if (stat == NextLevelStats.CoolDown) { tempValue = 45; } else { tempValue = 95; }
                break;
            case 4:
                if (stat == NextLevelStats.CoolDown) { tempValue = 40; } else { tempValue = 120; }
                break;
            case 5:
                if (stat == NextLevelStats.CoolDown) { tempValue = 35; } else { tempValue = 200; }
                break;
            case 6:
                if (stat == NextLevelStats.CoolDown) { tempValue = 30; } else { tempValue = 300; }
                break;
        }
        return tempValue;
    }
    //Called by SetDescription and level up sets next level stat for Radio
    float RadioNextLevel(int level, NextLevelStats stat)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                if (stat == NextLevelStats.CoolDown) { tempValue = 8; } else { tempValue = 5; }
                break;
            case 2:
                if (stat == NextLevelStats.CoolDown) { tempValue = 7; } else { tempValue = 7; }
                break;
            case 3:
                if (stat == NextLevelStats.CoolDown) { tempValue = 6; } else { tempValue = 9; }
                break;
            case 4:
                if (stat == NextLevelStats.CoolDown) { tempValue = 5; } else { tempValue = 11; }
                break;
            case 5:
                if (stat == NextLevelStats.CoolDown) { tempValue = 4; } else { tempValue = 15; }
                break;
            case 6:
                if (stat == NextLevelStats.CoolDown) { tempValue = 3; } else { tempValue = 20; }
                break;
        }
        return tempValue;
    }
    //Called by SetDescription and level up sets next level stat for counter
    float CounterNextLevel(int level, NextLevelStats stat)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                if (stat == NextLevelStats.CoolDown) { tempValue = 28; } else { tempValue = 1.6f; }
                break;
            case 2:
                if (stat == NextLevelStats.CoolDown) { tempValue = 25; } else { tempValue = 1.7f; }
                break;
            case 3:
                if (stat == NextLevelStats.CoolDown) { tempValue = 22; } else { tempValue = 1.8f; }
                break;
            case 4:
                if (stat == NextLevelStats.CoolDown) { tempValue = 19; } else { tempValue = 1.9f; }
                break;
            case 5:
                if (stat == NextLevelStats.CoolDown) { tempValue = 15; } else { tempValue = 2.0f; }
                break;
            case 6:
                if (stat == NextLevelStats.CoolDown) { tempValue = 10; } else { tempValue = 3; }
                break;
        }
        return tempValue;
    }
    //Called by SetDescription and level up sets next level stat for injection
    float InjectionNextLevel(int level, NextLevelStats stat)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                if (stat == NextLevelStats.CoolDown) { tempValue = 9; } else { tempValue = 7; }
                break;
            case 2:
                if (stat == NextLevelStats.CoolDown) { tempValue = 8; } else { tempValue = 8; }
                break;
            case 3:
                if (stat == NextLevelStats.CoolDown) { tempValue = 7; } else { tempValue = 9; }
                break;
            case 4:
                if (stat == NextLevelStats.CoolDown) { tempValue = 6; } else { tempValue = 15; }
                break;
            case 5:
                if (stat == NextLevelStats.CoolDown) { tempValue = 5; } else { tempValue = 20; }
                break;
            case 6:
                if (stat == NextLevelStats.CoolDown) { tempValue = 5; } else { tempValue = 30; }
                break;
        }
        return tempValue;
    }

    //Called by SetDescription and level up sets next level stat for hit points
    float HitPointsNL(int level)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                tempValue = 115;
                break;
            case 2:
                tempValue = 140;
                break;
            case 3:
                tempValue = 180;
                break;
            case 4:
                tempValue = 230;
                break;
            case 5:
                tempValue = 280;
                break;
            case 6:
                tempValue = 350;
                break;
        }
        return tempValue;
    }
    //Called by SetDescription and level up sets next level stat for hit points
    float StrengthNL(int level)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                tempValue = 7;
                break;
            case 2:
                tempValue = 10;
                break;
            case 3:
                tempValue = 15;
                break;
            case 4:
                tempValue = 25;
                break;
            case 5:
                tempValue = 35;
                break;
            case 6:
                tempValue = 50;
                break;
        }
        return tempValue;
    }
    //Called by SetDescription and level up sets next level stat for hit points
    float StaminaNL(int level)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                tempValue = 50;
                break;
            case 2:
                tempValue = 60;
                break;
            case 3:
                tempValue = 70;
                break;
            case 4:
                tempValue = 80;
                break;
            case 5:
                tempValue = 100;
                break;
            case 6:
                tempValue = 150;
                break;
        }
        return tempValue;
    }
    //Called by SetDescription and level up sets next level stat for hit points
    float RoomNL(int level, NLRoomStats stat)
    {
        float tempValue = 0;
        switch (level)
        {
            case 1:
                if (stat == NLRoomStats.DamageThreshold) { tempValue = 40; } else if(stat == NLRoomStats.ExpandRate) { tempValue = .2f; } else { tempValue = 35; }
                break;
            case 2:
                if (stat == NLRoomStats.DamageThreshold) { tempValue = 55; } else if (stat == NLRoomStats.ExpandRate) { tempValue = .3f; } else { tempValue = 40; }
                break;
            case 3:
                if (stat == NLRoomStats.DamageThreshold) { tempValue = 70; } else if (stat == NLRoomStats.ExpandRate) { tempValue = .4f; } else { tempValue = 50; }
                break;
            case 4:
                if (stat == NLRoomStats.DamageThreshold) { tempValue = 100; } else if (stat == NLRoomStats.ExpandRate) { tempValue = .5f; } else { tempValue = 60; }
                break;
            case 5:
                if (stat == NLRoomStats.DamageThreshold) { tempValue = 200; } else if (stat == NLRoomStats.ExpandRate) { tempValue = .7f; } else { tempValue = 80; }
                break;
            case 6:
                if (stat == NLRoomStats.DamageThreshold) { tempValue = 500; } else if (stat == NLRoomStats.ExpandRate) { tempValue = 1; } else { tempValue = 100; }
                break;
        }
        return tempValue;
    }
    
    bool LevelUpCheck(int level, int levelCap, int skillCost, int staticStat, bool isAdd)
    {
        if(isAdd == true)
        {
            if (level < levelCap)
            {
                if (statLvlHolder.skillPoints > 0)
                {
                    //success
                    level++;
                    statLvlHolder.skillPoints -= skillCost;
                    SetStaticStat(level, staticStat);
                    return true;
                }
                //not enough skill points
                else
                {
                    StartCoroutine(DisplayEvent(languageMan.SetInGameSenetence(LanguageMan.InGameUI.NoSkillPoints, EngOrJap())));
                    return false;
                }
            }
            //max level reached
            else
            {
                StartCoroutine(DisplayEvent(languageMan.SetInGameSenetence(LanguageMan.InGameUI.LevelCap, EngOrJap())));
                return false;
            }
        }
        else
        {
            level--;
            statLvlHolder.skillPoints += skillCost;
            return true;
        }

    }
    //called by the plus an minus buttons,  consumes skill points to level up stats
    public void LevelUp(bool isAdding)
    {
        switch(tempAbilityName)
        {
            case "GammaKnifeUp":
                if (LevelUpCheck(statLvlHolder.gammaLevel, gammaCap, SetSkillPoints(statLvlHolder.gammaLevel), StaticMan.gammaLvl, isAdding) == true)
                    ChangeGMAStats();
                break;
            case "RadioKnifeUp":
                if (LevelUpCheck(statLvlHolder.radioLevel, radioCap, SetSkillPoints(statLvlHolder.radioLevel), StaticMan.radioLvl, isAdding) == true)
                    ChangeRDOStats();
                break;
            case "InjectionShotUp":
                if (LevelUpCheck(statLvlHolder.injectionLevel, injectionCap, SetSkillPoints(statLvlHolder.injectionLevel), StaticMan.injectionLvl, isAdding) == true)
                    ChangeINJStats();
                break;
            case "CounterShockUp":
                if (LevelUpCheck(statLvlHolder.counterLevel, counterCap, SetSkillPoints(statLvlHolder.counterLevel), StaticMan.counterLvl, isAdding) == true)
                    ChangeCTRStats();
                break;
            case "RoomUp":
                if (LevelUpCheck(statLvlHolder.roomLevel, roomCap, SetSkillPoints(statLvlHolder.roomLevel), StaticMan.roomLvl, isAdding) == true)
                    ChangeRMStats();
                break;
            case "HitPointsUp":
                if (LevelUpCheck(statLvlHolder.hPLevel, hPCap, SetSkillPoints(statLvlHolder.hPLevel), StaticMan.hpLvl, isAdding) == true)
                    ChangeHPStats();
                break;
            case "StaminaUp":
                if (LevelUpCheck(statLvlHolder.staminaLevel, staminaCap, SetSkillPoints(statLvlHolder.staminaLevel), StaticMan.staminaLvl, isAdding) == true)
                    ChangeSTMStats();
                break;
            case "StrengthUp":
                if (LevelUpCheck(statLvlHolder.strengthLevel, strengthCap, SetSkillPoints(statLvlHolder.strengthLevel), StaticMan.strengthLvl, isAdding) == true)
                    ChangeSTRStats();
                break;
        }
    }
    //called when you display a sentence on the bottom of the screen
    IEnumerator DisplayEvent(string sentence)
    {
        if (eventText.gameObject.activeSelf == true)
            yield break;
        eventText.gameObject.SetActive(true);
        eventText.text = sentence;
        yield return new WaitForSeconds(2);
        eventText.gameObject.SetActive(false);
    }
    //the following "change" functions are called by player at start to change the player stats depending on the stat level saved by static man
    public void ChangeGMAStats()
    {
        if (statLvlHolder.gammaLevel == 0)
            return;
        roomBeam.gammaCDMax = GammaNextLevel(statLvlHolder.gammaLevel, NextLevelStats.CoolDown);
        player.gammaBonus = GammaNextLevel(statLvlHolder.gammaLevel, NextLevelStats.DamageBonus);
    }
    public void ChangeRDOStats()
    {
        if (statLvlHolder.radioLevel == 0)
            return;
        roomBeam.radioCDMax = RadioNextLevel(statLvlHolder.radioLevel, NextLevelStats.CoolDown);
        player.radioBonus = RadioNextLevel(statLvlHolder.radioLevel, NextLevelStats.DamageBonus);
    }
    public void ChangeINJStats()
    {
        if (statLvlHolder.injectionLevel == 0)
            return;
        roomBeam.injectionCDMax = InjectionNextLevel(statLvlHolder.injectionLevel, NextLevelStats.CoolDown);
        player.injectionShotBonus = InjectionNextLevel(statLvlHolder.injectionLevel, NextLevelStats.DamageBonus);
    }
    public void ChangeCTRStats()
    {
        if (statLvlHolder.counterLevel == 0)
            return;
        roomBeam.counterCDMax = CounterNextLevel(statLvlHolder.counterLevel, NextLevelStats.CoolDown);
        player.counterMultiplier = CounterNextLevel(statLvlHolder.counterLevel, NextLevelStats.DamageBonus);
    }
    public void ChangeRMStats()
    {
        if (statLvlHolder.roomLevel == 0)
            return;
        room.roomTimeActiveMax = RoomNL(statLvlHolder.roomLevel, NLRoomStats.RoomActive);
        room.growthRate = RoomNL(statLvlHolder.roomLevel, NLRoomStats.ExpandRate);
        room.damageThreshold = RoomNL(statLvlHolder.roomLevel, NLRoomStats.DamageThreshold);
    }
    public void ChangeHPStats()
    {
        if (statLvlHolder.hPLevel == 0)
            return;
        player.maxHealth = HitPointsNL(statLvlHolder.hPLevel);
    }
    public void ChangeSTMStats()
    {
        if (statLvlHolder.staminaLevel == 0)
            return;
        player.maxStamina = StaminaNL(statLvlHolder.staminaLevel);
    }
    public void ChangeSTRStats()
    {
        if (statLvlHolder.strengthLevel == 0)
            return;
        player.strength = StrengthNL(statLvlHolder.strengthLevel);
    }
    public void ChangeSP()
    {
        if (statLvlHolder.skillPoints == 0)
            return;
        statLvlHolder.skillPoints = StaticMan.skillPoints;
    }
}
