using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    public Dialogue d_prologue;
    public GameManager gameManager;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.ShowDialogue(d_prologue);
    }

    private void Update()
    {
        if(dialogueManager.count == 3)
        {
            StartCoroutine(gameManager.FadeIn());
        }
        if(dialogueManager.count > 3 ) 
        {
            StartCoroutine(gameManager.FadeIn());
        }

        if (dialogueManager.dialogue_continue == false)
        {
            this.gameObject.SetActive(false);
        }
    }
}
