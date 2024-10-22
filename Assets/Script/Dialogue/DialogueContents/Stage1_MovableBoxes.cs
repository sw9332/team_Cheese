using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_MovableBoxes : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;

    public Dialogue movableBoxes;

    IEnumerator CheckDialogueEnd()
    {
        yield return StartCoroutine(tutorialManager.ShowTutorialUI());
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueManager.ShowDialogue(movableBoxes);
            StartCoroutine(CheckDialogueEnd());
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
}
