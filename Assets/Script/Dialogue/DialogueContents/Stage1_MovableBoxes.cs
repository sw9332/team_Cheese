using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_MovableBoxes : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;

    public Dialogue movableBoxes;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(tutorialManager.ShowTutorialUI(false, movableBoxes));
            this.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
}
