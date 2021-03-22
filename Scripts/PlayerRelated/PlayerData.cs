using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is what temporarily holds the data that is going to be saved
[System.Serializable]
public class PlayerData
{
    public bool isEng;
    public int stagesCleared;
    public int gammaLvl;
    public int radioLvl;
    public int counterLvl;
    public int injectionLvl;
    public int roomLvl;
    public int hpLvl;
    public int staminaLvl;
    public int strengthLvl;
    public int skillPoints;
    public bool[] spEarnCheck;
    public PlayerData(StatLvlHolder statLvlHolder)
    {
        isEng = statLvlHolder.isEng;
        stagesCleared = statLvlHolder.stagesComplete;
        gammaLvl = statLvlHolder.gammaLevel;
        radioLvl = statLvlHolder.radioLevel;
        counterLvl = statLvlHolder.counterLevel;
        injectionLvl = statLvlHolder.injectionLevel;
        roomLvl = statLvlHolder.roomLevel;
        hpLvl = statLvlHolder.roomLevel;
        staminaLvl = statLvlHolder.staminaLevel;
        strengthLvl = statLvlHolder.strengthLevel;
        skillPoints = statLvlHolder.skillPoints;
        for (int i = 0; i < spEarnCheck.Length; i++)
        {
            spEarnCheck[i] = statLvlHolder.spEarnCheck[i];
        }
    }
}
