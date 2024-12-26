using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private TutorialManager tutorialManager;

    private Collider2D objectCollider;

    public bool canPickUp = false;
    public bool isInstalled = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPickUp && !dialogueManager.dialogue_continue && !tutorialManager.TutorialUI.activeSelf && inventoryManager.Camera)
        {
            inventoryManager.PickUpItem(objectCollider);
        }

        else if(Input.GetKeyDown(KeyCode.Space) && canPickUp && !inventoryManager.Camera)
        {
            dialogueManager.ShowDialogue(dialogueContentManager.d_not_camera);
            canPickUp = false;
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        objectCollider = GetComponent<Collider2D>();
    }
}
