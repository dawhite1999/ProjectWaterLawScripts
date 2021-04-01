using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is called to save and load by buttons, and constructs the playerdata class
public class StatLvlHolder : MonoBehaviour
{
    public bool isEng;
    public int stagesComplete;
    public int gammaLevel = 1;
    public int radioLevel = 1;
    public int counterLevel = 1;
    public int injectionLevel = 1;
    public int roomLevel = 1;
    public int hPLevel = 1;
    public int staminaLevel = 1;
    public int strengthLevel = 1;
    public int skillPoints;
    public void SaveScore()
    {
        SaveMan.SaveScore(this);
    }
    //call this before you initialize stats in player
    public void LoadSave()
    {
        if (SaveMan.LoadPlayer() != null)
        {
            PlayerData playerData = SaveMan.LoadPlayer();
            StaticMan.stagesComplete = playerData.stagesCleared;
            StaticMan.gammaLvl = playerData.gammaLvl;
            StaticMan.radioLvl = playerData.radioLvl;
            StaticMan.counterLvl = playerData.counterLvl;
            StaticMan.injectionLvl = playerData.injectionLvl;
            StaticMan.roomLvl = playerData.roomLvl;
            StaticMan.hpLvl = playerData.hpLvl;
            StaticMan.staminaLvl = playerData.staminaLvl;
            StaticMan.strengthLvl = playerData.strengthLvl;
            StaticMan.skillPoints = playerData.skillPoints;
        }
        else
        {
            StaticMan.gammaLvl = 1;
            StaticMan.radioLvl = 1;
            StaticMan.counterLvl = 1;
            StaticMan.injectionLvl = 1;
            StaticMan.roomLvl = 1;
            StaticMan.hpLvl = 1;
            StaticMan.staminaLvl = 1;
            StaticMan.strengthLvl = 1;
        }
    }
    public void ResetProgress()
    {
        StaticMan.gammaLvl = 1;
        StaticMan.radioLvl = 1;
        StaticMan.counterLvl = 1;
        StaticMan.injectionLvl = 1;
        StaticMan.roomLvl = 1;
        StaticMan.hpLvl = 1;
        StaticMan.staminaLvl = 1;
        StaticMan.strengthLvl = 1;
        StaticMan.skillPoints = 0;
        StaticMan.stagesComplete = 0;
    }
}
