using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMan : MonoBehaviour
{
    //references
    public AudioMixer audioMixer;
    [SerializeField] AudioSource musicManager = new AudioSource();
    [SerializeField] List<AudioSource> playerSFXSources = new List<AudioSource>(4);
    [SerializeField] List<AudioSource> otherSFXSources = new List<AudioSource>(4);

    //clips
    [SerializeField] AudioClip[] bgmClips = new AudioClip[0];
    [SerializeField] AudioClip[] playerSFXClips = new AudioClip[0];
    [SerializeField] AudioClip[] enemySFXClips = new AudioClip[0];
    [SerializeField] AudioClip[] otherSFXClips = new AudioClip[0];
    //dictionaries
    Dictionary<PlayerClipNames, AudioClip> PlayerAudioDict = new Dictionary<PlayerClipNames, AudioClip>();
    Dictionary<EnemyClipNames, AudioClip> EnemyAudioDict = new Dictionary<EnemyClipNames, AudioClip>();
    Dictionary<BGMClipNames, AudioClip> BGMAudioDict = new Dictionary<BGMClipNames, AudioClip>();
    Dictionary<OtherClipNames, AudioClip> OtherAudioDict = new Dictionary<OtherClipNames, AudioClip>();
    //clip names
    public enum PlayerClipNames { RoomStart, RoomLoop, Takt, InjectionShot, CounterShock, GammaKnife, Shambles, RadioKnife, Walk, Run, BigHit1, BigHit2, MediumHit1, MediumHit2, SmallHit1, SmallHit2, Swoosh }
    public enum EnemyClipNames {Shoot,  KO, Stun, SmallClash1, SmallClash2, SmallClash3, SmallClash4, MediumClash1, MediumClash2, BigClash, Explosion2, }
    public enum BGMClipNames { Title, Menu, World, Boss}
    public enum OtherClipNames {Explosion, Blip }

    //variables
    int indexNum;
    float exposedVolume;

    public float GetEXVolume() { return exposedVolume; }
    //gets called in pauseman before the sliders get turned off
    public void InitializeAudio()
    {
        //add player clips to dictionary
        PlayerAudioDict.Add(PlayerClipNames.SmallHit1, playerSFXClips[0]);
        PlayerAudioDict.Add(PlayerClipNames.SmallHit2, playerSFXClips[1]);
        PlayerAudioDict.Add(PlayerClipNames.MediumHit1, playerSFXClips[2]);
        PlayerAudioDict.Add(PlayerClipNames.MediumHit2, playerSFXClips[3]);
        PlayerAudioDict.Add(PlayerClipNames.BigHit1, playerSFXClips[4]);
        PlayerAudioDict.Add(PlayerClipNames.BigHit2, playerSFXClips[5]);
        PlayerAudioDict.Add(PlayerClipNames.Swoosh, playerSFXClips[6]);
        PlayerAudioDict.Add(PlayerClipNames.Takt, playerSFXClips[7]);
        PlayerAudioDict.Add(PlayerClipNames.CounterShock, playerSFXClips[8]);
        PlayerAudioDict.Add(PlayerClipNames.Run, playerSFXClips[9]);
        PlayerAudioDict.Add(PlayerClipNames.Walk, playerSFXClips[10]);
        PlayerAudioDict.Add(PlayerClipNames.RoomStart, playerSFXClips[11]);
        PlayerAudioDict.Add(PlayerClipNames.RoomLoop, playerSFXClips[12]);
        PlayerAudioDict.Add(PlayerClipNames.Shambles, playerSFXClips[13]);
        PlayerAudioDict.Add(PlayerClipNames.RadioKnife, playerSFXClips[14]);
        PlayerAudioDict.Add(PlayerClipNames.GammaKnife, playerSFXClips[15]);
        PlayerAudioDict.Add(PlayerClipNames.InjectionShot, playerSFXClips[16]);
        //add enemy clips to dictionary
        EnemyAudioDict.Add(EnemyClipNames.SmallClash1, enemySFXClips[0]);
        EnemyAudioDict.Add(EnemyClipNames.SmallClash2, enemySFXClips[1]);
        EnemyAudioDict.Add(EnemyClipNames.SmallClash3, enemySFXClips[2]);
        EnemyAudioDict.Add(EnemyClipNames.SmallClash4, enemySFXClips[3]);
        EnemyAudioDict.Add(EnemyClipNames.MediumClash1, enemySFXClips[4]);
        EnemyAudioDict.Add(EnemyClipNames.MediumClash2, enemySFXClips[5]);
        EnemyAudioDict.Add(EnemyClipNames.BigClash, enemySFXClips[6]);
        EnemyAudioDict.Add(EnemyClipNames.Shoot, enemySFXClips[7]);
        EnemyAudioDict.Add(EnemyClipNames.Explosion2, enemySFXClips[8]);
        //EnemyAudioDict.Add(EnemyClipNames.KO, enemySFXClips[1]);
        //EnemyAudioDict.Add(EnemyClipNames.Shoot, enemySFXClips[2]);
        //EnemyAudioDict.Add(EnemyClipNames.Stun, enemySFXClips[3]);
        //add bgm clips to the dictionary
        //BGMAudioDict.Add(BGMClipNames.Boss, bgmClips[0]);
        //BGMAudioDict.Add(BGMClipNames.Menu, bgmClips[1]);
        //BGMAudioDict.Add(BGMClipNames.Title, bgmClips[2]);
        //BGMAudioDict.Add(BGMClipNames.World, bgmClips[3]);
        //add other sounds to the dictionary
        OtherAudioDict.Add(OtherClipNames.Blip, otherSFXClips[0]);
        OtherAudioDict.Add(OtherClipNames.Explosion, otherSFXClips[1]);
    }
    public void InitiateVolume(float musicValue, string volumeName, Slider slider)
    {
        audioMixer.SetFloat(volumeName, Mathf.Log10(musicValue) * 20);
        slider.value = musicValue;
    }
    //Functions that adjust the volume
    public void AdjustMusic(float musicValue)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(musicValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicValue);
        musicManager.GetComponent<AudioSource>().volume = musicValue;
    }
    public void AdjustSFX(float sFXValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sFXValue) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", sFXValue);
        exposedVolume = sFXValue;
        foreach (AudioSource source in playerSFXSources)
        {
            source.volume = sFXValue;
        }
        foreach (AudioSource source in otherSFXSources)
        {
            source.volume = sFXValue;
        }
        if(FindObjectOfType<Enemy>() != null)
        {
            foreach (Enemy item in FindObjectsOfType<Enemy>())
            {
                item.ChangeVolume();
            }
        }
        if(FindObjectOfType<Projectile>() != null)
        {
            foreach (Projectile item in FindObjectsOfType<Projectile>())
            {
                item.ChangeVolume();
            }
        }
    }
    //Stopping and starting clips
    public void TestSFX()
    {
        PlayOtherClip(OtherClipNames.Blip);
    }
    public void StopMusic()
    {
        musicManager.Stop();
    }
    public void Stop1RePlayerSFX(int index)
    {
        playerSFXSources[index].Stop();
        playerSFXSources[index].loop = false;
    }
    public void PauseAllSFX()
    {
        foreach (AudioSource source in playerSFXSources)
        {
            source.Pause();
        }
        foreach (AudioSource source in otherSFXSources)
        {
            source.Pause();
        }
        if (FindObjectOfType<Enemy>() != null)
        {
            foreach (Enemy item in FindObjectsOfType<Enemy>())
            {
                item.PauseAllSFX();
            }
        }
    }
    //playing clips
    public void PlayPlayerClip(PlayerClipNames clipName)
    {
        foreach (AudioSource source in playerSFXSources)
        {
            if (source.isPlaying == false)
            {
                source.pitch = 1;
                if (clipName == PlayerClipNames.Swoosh)
                {
                    source.pitch = Random.Range(0.9f, 1.2f);
                }
                source.clip = PlayerAudioDict[clipName];
                source.Play();
                return;
            }
        }
    }
    public void PlayBGM(BGMClipNames clipName)
    {
        musicManager.clip = BGMAudioDict[clipName];
        musicManager.Play();
    }
    //called when a player sfx needs to loop. When being called for walking or running, don't loop, (since its being called on getkey) go to the last audio source, change the pitch, and return.
    public void PlayPlayerRepeatSound(PlayerClipNames clipName)
    {
        if (clipName == PlayerClipNames.Walk || clipName == PlayerClipNames.Run)
        {
            if(playerSFXSources[playerSFXSources.Count - 1].isPlaying == false)
            {
                playerSFXSources[playerSFXSources.Count - 1].clip = PlayerAudioDict[clipName];
                playerSFXSources[playerSFXSources.Count - 1].pitch = Random.Range(0.9f, 1.1f);
            }
            return;
        }
        foreach (AudioSource source in playerSFXSources)
        {
            if (source.isPlaying == false)
            {
                source.clip = PlayerAudioDict[clipName];
                source.loop = true;
                source.Play();
                //this is saved so that when the sfx needs to be stopped, whatever is stopping it can find the correct index
                indexNum = playerSFXSources.IndexOf(source);
                return;
            }
        }
    }
    public void PlayEnemyClip(EnemyClipNames clipName, AudioSource[] audioSources)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying == false)
            {
                source.clip = EnemyAudioDict[clipName];
                source.Play();
                return;
            }
        }
    }
    public void PlayOtherClip(OtherClipNames clipName)
    {
        foreach (AudioSource source in otherSFXSources)
        {
            if (source.isPlaying == false)
            {
                source.clip = OtherAudioDict[clipName];
                source.Play();
                return;
            }
        }
    }
    public int GetIndexNum() { return indexNum; }
}
