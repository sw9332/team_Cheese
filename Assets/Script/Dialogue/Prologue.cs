using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    public Dialogue d_prologue;
    private FadeManager fadeManager;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        fadeManager = FindObjectOfType<FadeManager>();
        dialogueManager.ShowDialogue(d_prologue);
    }

    private void Update()
    {
        if(dialogueManager.count == 3)
        {
            StartCoroutine(fadeManager.FadeIn());
        }
        if(dialogueManager.count > 3 ) 
        {
            StartCoroutine(fadeManager.FadeIn());
        }

        if (dialogueManager.dialogue_continue == false)
        {
            this.gameObject.SetActive(false);
        }
    }
}
