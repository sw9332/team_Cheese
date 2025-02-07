using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Door1 : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue locked_door1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) dialogueManager.ShowDialogue(locked_door1);
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
}