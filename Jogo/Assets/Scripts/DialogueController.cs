using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speechText;
    public Text actorName;

    [Header("Settings")] 
    public float typingSpeed;

    private string[] sentences;
    private int sentenceIndex = 0;

    private bool onUse = false;

    void Start() {
        speechText.text = "";
    }

    public void Speech(string speech, string name, Sprite profile) {
        this.SpeechMultiple(new string[] { speech }, name, profile);
    }

    public void SpeechMultiple(string[] speech, string name, Sprite profile) {
        if (this.onUse) return;
        this.onUse = true;

        this.dialogueObj.SetActive(true);
        this.profile.sprite = profile;
        this.actorName.text = name;

        this.sentenceIndex = 0;
        this.sentences = speech;

        StartCoroutine(this.typeSentence());
    }

    IEnumerator typeSentence() {

        foreach (char letter in this.sentences[sentenceIndex].ToCharArray()) {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void NextSentence () {

        if (speechText.text == sentences[sentenceIndex]) {

            speechText.text = "";

            if (this.sentenceIndex < sentences.Length - 1) {
                this.sentenceIndex++;
                StartCoroutine(this.typeSentence());
            } else {
                this.sentenceIndex = 0;
                dialogueObj.SetActive(false);
                this.onUse = false;
            }

        }

    }
}
