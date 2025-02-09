using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Door1 : MonoBehaviour
{
    public static bool unlocked = false;
    public static bool door_Unlocked = false;

    private InventoryManager inventoryManager;

    private DialogueManager dialogueManager;
    private DialogueChoiceManager dialogueChoiceManager;

    public Dialogue locked_door1;
    public Dialogue unlocked1;
    public Dialogue unlocked2;

    IEnumerator Unlocked()
    {
        dialogueManager.is_ChoiceExpected = true;
        dialogueManager.ShowDialogue(unlocked1);
        while (dialogueManager.dialogue_continue) yield return null;

        dialogueManager.ShowDialogue(unlocked2);
        dialogueManager.ShowChoiceDialogue(true, "연다", "그만둔다");
        while (dialogueManager.dialogue_continue) yield return null;

        if (door_Unlocked)
        {
            this.gameObject.SetActive(false);

            for (int i = 0; i < inventoryManager.SlotDB.Length; i++)
            {
                if (inventoryManager.SlotDB[i] == "Key")
                {
                    inventoryManager.SlotDB[i] = null;
                    inventoryManager.SlotImageDB[i].sprite = null;
                    break;
                }
            }
        }

        else this.gameObject.SetActive(true);

        unlocked = false;
        door_Unlocked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i<inventoryManager.SlotDB.Length; i++)
            {
                if (inventoryManager.SlotDB[i] != "Key")
                {
                    dialogueManager.ShowDialogue(locked_door1);
                    break;
                }
                    

                else if (inventoryManager.SlotDB[i] == "Key")
                {
                    StartCoroutine(Unlocked());
                    unlocked = true;
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