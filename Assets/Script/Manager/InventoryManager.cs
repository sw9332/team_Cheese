using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public string[] SlotDB;
    public Image[] SlotImageDB;

    private Player player;
    private ItemManager itemManager;

    public void PickUpItem(Collider2D item)
    {
        for (int i = 0; i < SlotDB.Length; i++)
        {
            if (string.IsNullOrEmpty(SlotDB[i]))
            {
                SlotDB[i] = item.tag;
                SlotImageDB[i].sprite = itemManager.GetItemSprite(item.tag);
                Destroy(item.gameObject);
                break;
            }
        }
    }

    public void DropItem(int slotIndex)
    {
        if (string.IsNullOrEmpty(SlotDB[slotIndex])) return;

        Item item = itemManager.GetItem(SlotDB[slotIndex]);

        if (!Player.objectCollision && item != null) Instantiate(item.prefab, player.transform.position, Quaternion.identity);
        else if (Player.objectCollision && item != null) Instantiate(item.prefab, Object.pos, Quaternion.identity);

        SlotDB[slotIndex] = null;
        SlotImageDB[slotIndex].sprite = null;
    }

    public void Clean()
    {
        for (int i = 0; i < SlotDB.Length; i++)
        {
            SlotDB[i] = null;
            SlotImageDB[i].sprite = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) DropItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) DropItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) DropItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) DropItem(3);
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        itemManager = FindFirstObjectByType<ItemManager>();
    }
}
