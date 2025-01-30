using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private SaveManager saveManager;
    private DialogueManager dialogueManager;
    private DialogueChoiceManager dialogueChoiceManager;

    public Dialogue save;

    public static bool trigger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
            dialogueManager.ShowDialogue(save);
            dialogueManager.ShowChoiceDialogue(true, "예", "아니오");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //trigger = false;
        }
    }

    void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueChoiceManager = FindFirstObjectByType<DialogueChoiceManager>();
    }
}