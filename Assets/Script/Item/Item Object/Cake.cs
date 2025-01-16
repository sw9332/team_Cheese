using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private TutorialManager tutorialManager;
    private SaveManager saveManager;

    private Collider2D objectCollider;

    public bool canPickUp = false;
    public bool isInstalled = false;

    ItemData GetitemData(string tag, Vector2 position)
    {
        return new ItemData(tag, position);
    }

    void PickUp()
    {
        inventoryManager.PickUpItem(objectCollider);
        ItemData itemToRemove = GetitemData(gameObject.tag, transform.position);
        saveManager.itemDataCurrent.RemoveAll(item => item.tag == itemToRemove.tag && item.position == itemToRemove.position);
    }

    void Drop()
    {
        saveManager.itemDataCurrent.Add(GetitemData(gameObject.tag, transform.position));
    }

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
            PickUp();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canPickUp && !inventoryManager.Camera)
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
        saveManager = FindFirstObjectByType<SaveManager>();
        objectCollider = GetComponent<Collider2D>();

        Drop();
    }
}
