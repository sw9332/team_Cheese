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

    private Player player;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private UIManager uiManager;

    private bool canPickup = false;
    private Collider2D ItemCollider;

    GameObject GetItemObject(string itemName)
    {
        switch (itemName)
        {
            case "BrownTeddyBear": return ItemDB[0];
            case "PinkTeddyBear": return ItemDB[1];
            case "YellowTeddyBear": return ItemDB[2];
            case "Cake": return ItemDB[3];
            case "NPC": return ItemDB[4];
            default: return null;
        }
    }

    Sprite GetItemSprite(string itemName)
    {
        switch (itemName)
        {
            case "BrownTeddyBear": return ItemSpriteDB[0];
            case "PinkTeddyBear": return ItemSpriteDB[1];
            case "YellowTeddyBear": return ItemSpriteDB[2];
            case "Cake": return ItemSpriteDB[3];
            case "NPC": return ItemSpriteDB[4];
            default: return null;
        }
    }

    void PlaceItem(int slotIndex)
    {
        if (SlotDB[slotIndex] == null) return;

        GameObject itemObject = GetItemObject(SlotDB[slotIndex]);
        Vector3 position = !Player.objectCollision ? player.transform.position : Object.pos;

        Instantiate(itemObject, position, Quaternion.identity);
        SlotDB[slotIndex] = null;
        SlotImageDB[slotIndex].sprite = null;
    }

    void PickupItem(string itemName, Collider2D other)
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

    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "DroppedBrownTeddyBear":
            case "BrownTeddyBear":
            case "PinkTeddyBear":
            case "YellowTeddyBear":
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
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) PlaceItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlaceItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlaceItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlaceItem(3);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
}