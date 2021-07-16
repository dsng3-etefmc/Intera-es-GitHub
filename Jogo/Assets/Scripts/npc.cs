using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{

    public Sprite profile;
    public string speech;
    public string actorName = "Roberto";

    public LayerMask layer;

    private DialogueController dialogue;

    public float radiousInteraction;

    private bool playerOnRadius;

    public GameObject indicator;

    public string[] sentences = {
        "Hello, Stranger!",
        "...",
        "Goodbye, Stranger!"
    };

    void Start() {
        this.dialogue = FindObjectOfType<DialogueController>();
    }

    void Update() {

        if (this.playerOnRadius) {
            this.indicator.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                dialogue.SpeechMultiple(sentences, name, profile);
            }
        } else {
            this.indicator.SetActive(false);
        }

    }

    void FixedUpdate () {
        this.awaitSpeech();
    }

    void awaitSpeech() {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, this.radiousInteraction, layer);

        this.playerOnRadius = hit != null;
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, this.radiousInteraction);
    }
}
