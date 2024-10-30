using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;
    private TutorialManager tutorialManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        fadeManager = FindObjectOfType<FadeManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();

        dialogueManager.ShowDialogue(dialogueContentManager.d_prologue);
    }

    void Update()
    {
        if (dialogueManager.count == 3) StartCoroutine(fadeManager.FadeIn());
        if (dialogueManager.count > 3 ) StartCoroutine(fadeManager.FadeIn());

        if (dialogueManager.dialogue_continue == false)
        {
            gameObject.SetActive(false);
            tutorialManager.TutorialType(1);
            tutorialManager.TutorialUI.SetActive(true);
        }
    }
}