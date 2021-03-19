using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CutsceneMan : MonoBehaviour
{
    /*[SerializeField] GameObject openingScene;
    [SerializeField] GameObject bossScene;
    [SerializeField] GameObject endingScene;
    [SerializeField] GameObject creditsScene;
    [SerializeField] int popopoopo;

    [SerializeField] float readTime;
    LanguageMan languageMan;
    // Start is called before the first frame update
    void Start()
    {
        languageMan = FindObjectOfType<LanguageMan>();
        openingScene.SetActive(false);
        bossScene.SetActive(false);
        endingScene.SetActive(false);
        creditsScene.SetActive(false);
        switch (StaticMan.cutscenePicker)
        {
            case 0:
                openingScene.SetActive(true);
                StartCoroutine(OpeningScene());
                break;
            case 1:
                bossScene.SetActive(true);
                StartCoroutine(BossScene());
                break;
            case 2:
                endingScene.SetActive(true);
                StartCoroutine(EndingScene());
                break;
            case 3:
                creditsScene.SetActive(true);
                StartCoroutine(CreditsScene());
                break;
        }
    }

    IEnumerator OpeningScene()
    {
        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishOPDict[LanguageMan.OpeningCutscene.Line1], openingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseOPDict[LanguageMan.OpeningCutscene.Line1], openingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishOPDict[LanguageMan.OpeningCutscene.Line2], openingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseOPDict[LanguageMan.OpeningCutscene.Line2], openingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime + 4);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishOPDict[LanguageMan.OpeningCutscene.Line3], openingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseOPDict[LanguageMan.OpeningCutscene.Line3], openingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);

        openingScene.GetComponent<Animator>().Play("SleepOn");

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishOPDict[LanguageMan.OpeningCutscene.Line4], openingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseOPDict[LanguageMan.OpeningCutscene.Line4], openingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishOPDict[LanguageMan.OpeningCutscene.Line5], openingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseOPDict[LanguageMan.OpeningCutscene.Line5], openingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);
        FindObjectOfType<SceneMan>().LoadNewGame();
    }
    IEnumerator BossScene()
    {
        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishBossDict[LanguageMan.BossCutscene.Line1], bossScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseBossDict[LanguageMan.BossCutscene.Line1], bossScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishBossDict[LanguageMan.BossCutscene.Line2], bossScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseBossDict[LanguageMan.BossCutscene.Line2], bossScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishBossDict[LanguageMan.BossCutscene.Line3], bossScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseBossDict[LanguageMan.BossCutscene.Line3], bossScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);
        FindObjectOfType<SceneMan>().LoadBoss();
    }
    IEnumerator EndingScene()
    {
        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishEndDict[LanguageMan.EndingCutscene.Line1], endingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseEndDict[LanguageMan.EndingCutscene.Line1], endingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime + 4);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishEndDict[LanguageMan.EndingCutscene.Line2], endingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseEndDict[LanguageMan.EndingCutscene.Line2], endingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime + 4);

        if (StaticMan.isEnglish == true) { StartCoroutine(TypeTheSentence(languageMan.englishEndDict[LanguageMan.EndingCutscene.Line3], endingScene.GetComponentInChildren<Text>())); }
        else { StartCoroutine(TypeTheSentence(languageMan.japaneseEndDict[LanguageMan.EndingCutscene.Line3], endingScene.GetComponentInChildren<Text>())); }
        yield return new WaitForSeconds(readTime);
        FindObjectOfType<SceneMan>().LoadCredits();
    }
    IEnumerator CreditsScene()
    {
        FindObjectOfType<AudioMan>().InitializeAudio();
        FindObjectOfType<AudioMan>().PlayClip(AudioMan.ClipNames.credits, AudioMan.ClipType.Music);
        if (StaticMan.isEnglish == true)
            creditsScene.GetComponentInChildren<Text>().text = languageMan.englishEndDict[LanguageMan.EndingCutscene.CreditsLine];
        else
            creditsScene.GetComponentInChildren<Text>().text = languageMan.japaneseEndDict[LanguageMan.EndingCutscene.CreditsLine];
        yield return new WaitForSeconds(10);
        FindObjectOfType<SceneMan>().LoadMainMenu();
    }

    IEnumerator TypeTheSentence(string sentenceToType, Text visualText)
    {
        visualText.text = "";
        //type sentence
        foreach (char letter in sentenceToType.ToCharArray())
        {
            visualText.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }*/
}

