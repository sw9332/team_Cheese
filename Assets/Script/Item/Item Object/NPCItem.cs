using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItem : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;
    private SaveManager saveManager;
    private Event2 event2;

    private Collider2D objectCollider;

    bool canPickUp = false;

    ItemData GetitemData(string tag, Vector2 position)
    {
        return new ItemData(tag, position);
    }

    public void PickUp()
    {
        inventoryManager.PickUpItem(objectCollider);
        ItemData itemToRemove = GetitemData(gameObject.tag, transform.position);
        saveManager.itemDataCurrent.RemoveAll(item => item.tag == itemToRemove.tag && item.position == itemToRemove.position);
        gameObject.SetActive(false);
    }

    public void Drop()
    {
        ItemData itemToAdd = GetitemData(gameObject.tag, transform.position);
        if (!saveManager.itemDataCurrent.Exists(item => item.tag == itemToAdd.tag && item.position == itemToAdd.position))
        {
            saveManager.itemDataCurrent.Add(itemToAdd);
        }
        gameObject.SetActive(true);
    }

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
            if (event2 != null) event2.gameObject.SetActive(false);
            PickUp();
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        saveManager = FindFirstObjectByType<SaveManager>();
        event2 = FindFirstObjectByType<Event2>();
        objectCollider = GetComponent<Collider2D>();

        Drop();
    }
}