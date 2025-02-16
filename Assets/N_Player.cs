using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Player : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    private Animator animator;
    private PolygonCollider2D polygon;

    public bool isFollow = false;
    public bool event4 = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player": polygon.isTrigger = true; break;

            case "Event 1 (Chapter 2)":
                dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event1);
                Destroy(other.gameObject);
                break;

            case "Event 2 (Chapter 2)":
                dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event2);
                Destroy(other.gameObject);
                break;

            case "Event 4 (Chapter 2)":
                dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event4);
                event4 = true;
                isFollow = false;
                Destroy(other.gameObject);
                break;
        }

        if (other.CompareTag("Player") && event4)
        {
            dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event5);
            event4 = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) polygon.isTrigger = false;
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();

        polygon = GetComponent<PolygonCollider2D>();
    }
}