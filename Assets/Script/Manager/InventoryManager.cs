using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public string[] SlotDB;
    public Image[] SlotImageDB;
    public bool miniGameCamera = false;

    bool flower = false;
    bool chicken = false;
    bool Flower_and_Chicken = false;

    private Player player;
    private PlayerAttack playerattack;
    private ItemManager itemManager;
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

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

        if (!Flower_and_Chicken)
        {
            for (int i = 0; i < SlotDB.Length; i++)
            {
                if (SlotDB[i] == "Flower") flower = true;
                if (SlotDB[i] == "Chicken") chicken = true;
            }

            if (flower && chicken)
            {
                Flower_and_Chicken = true;
                dialogueManager.ShowDialogue(dialogue);
            }
        }
    }

    public void DropItem(int slotIndex)
    {
        if (Table.trigger)
        {
            if (SlotDB[slotIndex] != "Cake")
            {
                dialogueManager.ShowDialogue(dialogueContentManager.d_not_a_cake);
                return;
            }
        }

        if (string.IsNullOrEmpty(SlotDB[slotIndex])) return;

        Item item = itemManager.GetItem(SlotDB[slotIndex]);

        // �� ������ ���� �ؾ� ��� type�� item ������ �ߵ���

        switch (item.type)
        {
            case Type.일반: Drop(item); break;
            case Type.사용: Use(item); break;
            case Type.회복: HpRecovery(item); break;
        }

        SlotDB[slotIndex] = null;
        SlotImageDB[slotIndex].sprite = null;
    }

    public void Drop(Item item)
    {
        Vector2 position = !Player.objectCollision ? player.transform.position : Object.pos;
        Instantiate(item.prefab, position, Quaternion.identity);
    }

    public void Use(Item item)
    {
        switch (item.id)
        {
            case "Ammo": playerattack.bullet.bulletNum += 5; break;
        }
    }

    public void HpRecovery(Item item)
    {
        switch (item.id)
        {
            case "ChocoBar": Hp.Instance.HpPlus(2.0f); break;
        }
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
        playerattack = FindFirstObjectByType<PlayerAttack>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
    }
}