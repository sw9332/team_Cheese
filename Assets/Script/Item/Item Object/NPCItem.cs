using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItem : MonoBehaviour
{
    public static NPCItem Instance;

    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;

    private Collider2D objectCollider;

    bool canPickUp = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
        }

        if (other.CompareTag("Room D floor")) UIManager.is_NPC = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
        }

        if (other.CompareTag("Room D floor")) UIManager.is_NPC = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPickUp && !dialogueManager.dialogue_continue && !tutorialManager.TutorialUI.activeSelf)
        {
            inventoryManager.PickUpItem(objectCollider);
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        objectCollider = GetComponent<Collider2D>();
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}