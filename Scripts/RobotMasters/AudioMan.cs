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
    [SerializeField] AudioSource[] playerSFXSources = new AudioSource[4];
    [SerializeField] AudioSource[] enemySFXSources = new AudioSource[4];
    [SerializeField] AudioSource[] otherSFXSources = new AudioSource[4];
    Slider musicSlider;
    Slider sfxSlider;
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
    public enum PlayerClipNames { Room, Takt, InjectionShot, CounterShock, GammaKnife, Shambles, RadioKnife, Walk, Run, Jump, Slice}
    public enum EnemyClipNames {Shoot, HitPlayer, KO, Stun }
    public enum BGMClipNames { Title, Menu, World, Boss}
    public enum OtherClipNames {Explosion, Ding }

    //gets called in pauseman before the sliders get turned off
    public void InitializeAudio()
    {
        if(GameObject.Find("MusicSlider") != null)
        {
            musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
            sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            sfxSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1);
        }
        /* we dont have any audio right now so its commented out 
        //add player clips to dictionary
        PlayerAudioDict.Add(PlayerClipNames.CounterShock, playerSFXClips[0]);
        PlayerAudioDict.Add(PlayerClipNames.GammaKnife, playerSFXClips[1]);
        PlayerAudioDict.Add(PlayerClipNames.InjectionShot, playerSFXClips[2]);
        PlayerAudioDict.Add(PlayerClipNames.Jump, playerSFXClips[3]);
        PlayerAudioDict.Add(PlayerClipNames.RadioKnife, playerSFXClips[4]);
        PlayerAudioDict.Add(PlayerClipNames.Room, playerSFXClips[5]);
        PlayerAudioDict.Add(PlayerClipNames.Run, playerSFXClips[6]);
        PlayerAudioDict.Add(PlayerClipNames.Shambles, playerSFXClips[7]);
        PlayerAudioDict.Add(PlayerClipNames.Slice, playerSFXClips[8]);
        PlayerAudioDict.Add(PlayerClipNames.Takt, playerSFXClips[9]);
        PlayerAudioDict.Add(PlayerClipNames.Walk, playerSFXClips[10]);
        //add enemy clips to dictionary
        EnemyAudioDict.Add(EnemyClipNames.HitPlayer, enemySFXClips[0]);
        EnemyAudioDict.Add(EnemyClipNames.KO, enemySFXClips[1]);
        EnemyAudioDict.Add(EnemyClipNames.Shoot, enemySFXClips[2]);
        EnemyAudioDict.Add(EnemyClipNames.Stun, enemySFXClips[3]);
        //add bgm clips to the dictionary
        BGMAudioDict.Add(BGMClipNames.Boss, bgmClips[0]);
        BGMAudioDict.Add(BGMClipNames.Menu, bgmClips[1]);
        BGMAudioDict.Add(BGMClipNames.Title, bgmClips[2]);
        BGMAudioDict.Add(BGMClipNames.World, bgmClips[3]);
        //add other sounds to the dictionary
        OtherAudioDict.Add(OtherClipNames.Ding, otherSFXClips[0]);
        OtherAudioDict.Add(OtherClipNames.Explosion, otherSFXClips[1]);*/
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
        foreach (AudioSource source in playerSFXSources)
        {
            source.volume = sFXValue;
        }
        foreach (AudioSource source in enemySFXSources)
        {
            source.volume = sFXValue;
        }
        foreach (AudioSource source in otherSFXSources)
        {
            source.volume = sFXValue;
        }
    }
    //Stopping and starting clips
    public void TestSFX()
    {
        PlayOtherClip(OtherClipNames.Ding);
    }
    public void StopMusic()
    {
        musicManager.Stop();
    }
    public void StopAllSFX()
    {
        foreach (AudioSource source in playerSFXSources)
        {
            source.Stop();
        }
        foreach (AudioSource source in enemySFXSources)
        {
            source.Stop();
        }
        foreach (AudioSource source in otherSFXSources)
        {
            source.Stop();
        }
    }
    public void PauseAllSFX()
    {
        foreach (AudioSource source in playerSFXSources)
        {
            source.Pause();
        }
        foreach (AudioSource source in enemySFXSources)
        {
            source.Pause();
        }
        foreach (AudioSource source in otherSFXSources)
        {
            source.Pause();
        }
    }
    //playing clips
    public void PlayPlayerClip(PlayerClipNames clipName)
    {
        foreach (AudioSource source in playerSFXSources)
        {
            if (source.isPlaying == false)
            {
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
            if(playerSFXSources[playerSFXSources.Length - 1].isPlaying == false)
            {
                playerSFXSources[playerSFXSources.Length - 1].clip = PlayerAudioDict[clipName];
                playerSFXSources[playerSFXSources.Length - 1].pitch = Random.Range(0.9f, 1.1f);
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
                return;
            }
        }
    }
    public void PlayEnemyClip(EnemyClipNames clipName)
    {
        foreach (AudioSource source in enemySFXSources)
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
}
