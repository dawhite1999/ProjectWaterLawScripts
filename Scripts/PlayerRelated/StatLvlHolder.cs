using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is called to save and load by buttons, and constructs the playerdata class
public class StatLvlHolder : MonoBehaviour
{
    public bool isEng;
    public int stagesComplete;
    public int gammaLevel;
    public int radioLevel;
    public int counterLevel;
    public int injectionLevel;
    public int roomLevel;
    public int hPLevel;
    public int staminaLevel;
    public int strengthLevel;
    public int skillPoints;
    public bool[] spEarnCheck;
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
            for (int i = 0; i < StaticMan.spEarnCheck.Length; i++)
            {
                StaticMan.spEarnCheck[i] = spEarnCheck[i];
            }
        }
    }
}
