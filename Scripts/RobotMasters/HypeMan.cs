using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HypeMan : MonoBehaviour
{
    [SerializeField] List<GameObject> hypeTexts = new List<GameObject>();
    public void PHT(string incomingText)
    {
        StartCoroutine(PlayHypeText(incomingText));
    }
    IEnumerator PlayHypeText(string incomingText)
    {
        //go throught the list of hypetexts
        foreach (GameObject text in hypeTexts)
        {
            //if one of them is inactive
            if (text.activeSelf == false)
            {
                //turn it on, change the text, and feed it to the turn off function
                text.SetActive(true);
                text.GetComponent<TextMeshProUGUI>().text = incomingText;
                //play the animation with the corresponding index
                switch (hypeTexts.IndexOf(text))
                {
                    case 0:
                        //play anim 1
                        text.GetComponent<Animator>().Play("TextSlide1");
                        break;
                    case 1:
                        //play anim 2
                        text.GetComponent<Animator>().Play("TextSlide2");
                        break;
                    case 2:
                        //play anim 3
                        text.GetComponent<Animator>().Play("TextSlide3");
                        break;
                }
                //wait for the animation to finish, turn text off
                yield return new WaitForSeconds(1.4f);
                text.SetActive(false);
                //leave the foreach loop
                break;
            }
        }
    }
}
