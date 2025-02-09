using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Calender : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue calender;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) dialogueManager.ShowDialogue(calender);
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
}