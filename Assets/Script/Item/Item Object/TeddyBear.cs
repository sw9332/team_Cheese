using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;
    private TutorialManager tutorialManager;

    private Collider2D objectCollider;

    public bool canPickUp = false;
    public bool isInstalled = false;

    public Vector2 position;

    Vector3 Scale;

    void ItemPosition()
    {
        isInstalled = true;
        position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
        }

        if(other.CompareTag("Chair"))
        {
            switch(other.transform.localScale.x)
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
            inventoryManager.PickUpItem(objectCollider);
            isInstalled = false;
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        objectCollider = GetComponent<Collider2D>();

        ItemPosition();
    }
}