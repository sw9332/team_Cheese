using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
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
        gameObject.SetActive(false);
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
            PickUp();
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        saveManager = FindFirstObjectByType<SaveManager>();
        objectCollider = GetComponent<Collider2D>();

        Drop();
    }
}
