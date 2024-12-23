using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;

    private Collider2D objectCollider;

    public bool canPickUp = false;
    public bool isInstalled = false;

    Vector3 Scale;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueManager.dialogue_continue)
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
        if (Input.GetKeyDown(KeyCode.Space) && canPickUp)
        {
            inventoryManager.PickUpItem(objectCollider);
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        objectCollider = GetComponent<Collider2D>();
    }
}