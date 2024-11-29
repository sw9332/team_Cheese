using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public string[] SlotDB;
    public Image[] SlotImageDB;

    public GameObject[] ItemDB;
    public Sprite[] ItemSpriteDB;

    public Bullet bullet;
    public Loot[] lootList;

    private Player player;
    private PlayerAttack playerAttack;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private UIManager uiManager;

    private bool canPickup = false;
    private Collider2D ItemCollider;

    

    public GameObject GetItemObject(string itemName)
    {
        switch (itemName)
        {
            case "BrownTeddyBear": return ItemDB[0];
            case "PinkTeddyBear": return ItemDB[1];
            case "YellowTeddyBear": return ItemDB[2];
            case "Cake": return ItemDB[3];
            case "NPC": return ItemDB[4];
            // Ammo(Clone), ChocoBar(Clone) -> when object is duplicated
            case "Ammo": return ItemDB[5];
            case "Ammo(Clone)":return ItemDB[5];
            case "ChocoBar": return ItemDB[6];
            case "ChocoBar(Clone)":return ItemDB[6];
            default: return null;
        }
    }

    public Sprite GetItemSprite(string itemName)
    {
        switch (itemName)
        {
            case "BrownTeddyBear": return ItemSpriteDB[0];
            case "PinkTeddyBear": return ItemSpriteDB[1];
            case "YellowTeddyBear": return ItemSpriteDB[2];
            case "Cake": return ItemSpriteDB[3];
            case "NPC": return ItemSpriteDB[4];
            // Ammo(Clone), ChocoBar(Clone) -> when object is duplicated
            case "Ammo": return ItemSpriteDB[5];
            case "Ammo(Clone)": return ItemSpriteDB[5];
            case "ChocoBar": return ItemSpriteDB[6];
            case "ChocoBar(Clone)": return ItemSpriteDB[6];

            default: return null;
        }
    }

    public void PlaceItem(int slotIndex)
    {
        if (SlotDB[slotIndex] == null) return;

        GameObject itemObject = GetItemObject(SlotDB[slotIndex]);
        Vector3 position = !Player.objectCollision ? player.transform.position : Object.pos;
        if (itemObject.name == "Ammo" || itemObject.name == "Ammo(Clone)")
        {
            bullet.bulletNum += 5;

            // Destroy 안쓰는 이유: 원본 삭제의 우려 /  false변수 -> 현재 실행되는 객체만 삭제

            DestroyImmediate(itemObject,false); 
            SlotDB[slotIndex] = null;
            SlotImageDB[slotIndex].sprite = null;
        }
        else if (itemObject.name == "ChocoBar" || itemObject.name == "ChocoBar(Clone)")
        {
            // get last hp and set for new position for new HP
            GameObject hpObj = GameObject.Find("Player HP").transform.GetChild(GameObject.Find("Player HP").transform.childCount - 1).gameObject;
            RectTransform rectTransform = hpObj.GetComponent<RectTransform>();
            Vector2 newAnchoredPosition = rectTransform.anchoredPosition + new Vector2(100, 0);

            // Locate newly generated Hp 
            GameObject newHpObj = Instantiate(hpObj, hpObj.transform.parent); // 부모 유지
            RectTransform newRectTransform = newHpObj.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = newAnchoredPosition;

            playerAttack.hp.Add(hpObj);
            // Why DestroyImmediate -> in case original asset can be deleted / false변수 -> 현재 실행되는 객체만 삭제
            DestroyImmediate(itemObject, false);
            SlotDB[slotIndex] = null;
            SlotImageDB[slotIndex].sprite = null;
        }
        else
        { 
            Instantiate(itemObject, position, Quaternion.identity);
            SlotDB[slotIndex] = null;
            SlotImageDB[slotIndex].sprite = null;
        }

        
    }

    public void PickupItem(string itemName, Collider2D other)
    {
        for (int i = 0; i < SlotDB.Length; i++)
            if (string.IsNullOrEmpty(SlotDB[i]))
            {
                SlotDB[i] = itemName;
                SlotImageDB[i].sprite = GetItemSprite(itemName);
                Destroy(other.gameObject);
                break;
            }
    }

    public void Clean()
    {
        for(int i=0; i < SlotDB.Length; i++)
        {
            SlotDB[i] = null;
            SlotImageDB[i].sprite = null;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "DroppedBrownTeddyBear":
            case "BrownTeddyBear":
            case "PinkTeddyBear":
            case "YellowTeddyBear":
            case "LootItem":
            case "Cake":
            case "NPC":
                canPickup = true;
                ItemCollider = other;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "DroppedBrownTeddyBear":
            case "BrownTeddyBear":
            case "PinkTeddyBear":
            case "YellowTeddyBear":
            case "Cake":
            case "LootItem":
            case "NPC":
                canPickup = false;
                ItemCollider = null;
                break;
        }
    }

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.Space))
        {
            if (ItemCollider != null)
            {
                string tag = ItemCollider.gameObject.tag;

                switch (tag)
                {
                    case "DroppedBrownTeddyBear": PickupItem("BrownTeddyBear", ItemCollider);
                        dialogueManager.ShowDialogue(dialogueContentManager.d_camera);
                        uiManager.CameraUI.SetActive(true);
                        break;
                    case "BrownTeddyBear": PickupItem("BrownTeddyBear", ItemCollider); break;
                    case "PinkTeddyBear": PickupItem("PinkTeddyBear", ItemCollider); break;
                    case "YellowTeddyBear": PickupItem("YellowTeddyBear", ItemCollider); break;
                    case "Cake": PickupItem("Cake", ItemCollider); break;
                    case "NPC": PickupItem("NPC", ItemCollider); break;
                    case "LootItem":
                        Debug.Log($"Loot Item Name: {ItemCollider.name}");
                        PickupItem(ItemCollider.name, ItemCollider); break;
                }
            }
        }

        if (!dialogueManager.dialogue_continue)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) PlaceItem(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlaceItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlaceItem(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) PlaceItem(3);
        }
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerAttack = FindFirstObjectByType<PlayerAttack>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        uiManager = FindFirstObjectByType<UIManager>();

    }
}