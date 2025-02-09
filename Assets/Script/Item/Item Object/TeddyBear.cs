using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;
    private SaveManager saveManager;

    private Collider2D objectCollider;

    public bool canPickUp = false;

    Vector3 Scale;

    ItemData GetitemData(string tag, Vector2 position)
    {
        return new ItemData(tag, position);
    }

    void PickUp()
    {
        for (int i = 0; i < inventoryManager.SlotDB.Length; i++)
        {
            if (inventoryManager.SlotDB[i] == null || inventoryManager.SlotDB[i] == "")
            {
                inventoryManager.PickUpItem(objectCollider);
                ItemData itemToRemove = GetitemData(gameObject.tag, transform.position);
                saveManager.itemDataCurrent.RemoveAll(item => item.tag == itemToRemove.tag && item.position == itemToRemove.position);
                gameObject.SetActive(false);
                break;
            }
        }
    }

    void Drop()
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

        if (other.CompareTag("Chair"))
        {
            switch (other.transform.localScale.x)
            {
                case -1:
                    Scale = transform.localScale;
                    Scale.x = -1;
                    transform.localScale = Scale;
                    break;

                case 1:
                    Scale = transform.localScale;
                    Scale.x = 1;
                    transform.localScale = Scale;
                    break;
            }
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
        if (Input.GetKeyDown(KeyCode.Space) && canPickUp && !dialogueManager.dialogue_continue && !tutorialManager.TutorialUI.activeSelf)
        {
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
        objectCollider = GetComponent<Collider2D>();

        Drop();
    }
}