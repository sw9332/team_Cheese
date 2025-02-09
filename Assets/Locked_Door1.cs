using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Door1 : MonoBehaviour
{
    private InventoryManager inventoryManager;

    private DialogueManager dialogueManager;
    private DialogueChoiceManager dialogueChoiceManager;

    public Dialogue locked_door1;
    public Dialogue unlocked1;
    public Dialogue unlocked2;

    IEnumerator Unlocked()
    {
        dialogueManager.ShowDialogue(unlocked1);
        while (dialogueManager.dialogue_continue) yield return null;
        dialogueManager.ShowDialogue(unlocked2);
        dialogueManager.ShowChoiceDialogue(true, "연다", "그만둔다");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i<inventoryManager.SlotDB.Length; i++)
            {
                if (inventoryManager.SlotDB[i] != "Key")
                    dialogueManager.ShowDialogue(locked_door1);

                else if (inventoryManager.SlotDB[i] == "Key")
                {
                    StartCoroutine(Unlocked());
                    break;
                }
            }
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueChoiceManager = FindFirstObjectByType<DialogueChoiceManager>();
    }
}