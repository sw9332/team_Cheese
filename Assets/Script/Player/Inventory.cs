using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public string[] itemDB = new string[4];
    public Image[] SlotImageDB = new Image[4];

    public GameObject BrownTeddyBear_Item;
    public GameObject PinkTeddyBear_Item;
    public GameObject YellowTeddyBear_Item;
    public GameObject Cake_Item;
    public GameObject NPC_Item;

    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;
    public Sprite NPC_Sprite;

    private Player player;
    private DialogueManager dialogueManager;
    private DialogueEventManager dialogueEventManager;
    private UIManager uiManager;

    private bool canPickup = false;
    private Collider2D currentItemCollider;

    GameObject GetItemObject(string itemName)
    {
        switch (itemName)
        {
            case "BrownTeddyBear": return BrownTeddyBear_Item;
            case "PinkTeddyBear": return PinkTeddyBear_Item;
            case "YellowTeddyBear": return YellowTeddyBear_Item;
            case "Cake": return Cake_Item;
            case "NPC": return NPC_Item;

            default: return null;
        }
    }

    Sprite GetItemSprite(string itemName)
    {
        switch (itemName)
        {
            case "BrownTeddyBear": return BrownTeddyBear_Sprite;
            case "PinkTeddyBear": return PinkTeddyBear_Sprite;
            case "YellowTeddyBear": return YellowTeddyBear_Sprite;
            case "Cake": return Cake_Sprite;
            case "NPC": return NPC_Sprite;

            default: return null;
        }
    }

    void PlaceItem(int slotIndex)
    {
        if (itemDB[slotIndex] == null) return;

        GameObject itemObject = GetItemObject(itemDB[slotIndex]);
        Vector3 position = !Player.objectCollision ? player.transform.position : Object.pos;

        Instantiate(itemObject, position, Quaternion.identity);
        itemDB[slotIndex] = null;
        SlotImageDB[slotIndex].sprite = null;
    }

    void PickupItem(string itemName, Collider2D other)
    {
        for (int i = 0; i < itemDB.Length; i++)
        {
            if (string.IsNullOrEmpty(itemDB[i]))
            {
                itemDB[i] = itemName;
                SlotImageDB[i].sprite = GetItemSprite(itemName);
                Destroy(other.gameObject);
                break;
            }
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
                currentItemCollider = other;
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
                currentItemCollider = null;
                break;
        }
    }

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.Space))
        {
            if (currentItemCollider != null)
            {
                string tag = currentItemCollider.gameObject.tag;

                switch (tag)
                {
                    case "DroppedBrownTeddyBear": PickupItem("BrownTeddyBear", currentItemCollider);
                        dialogueManager.ShowDialogue(dialogueEventManager.d_cake);
                        uiManager.CameraUI.SetActive(true);
                        break;
                    case "BrownTeddyBear": PickupItem("BrownTeddyBear", currentItemCollider); break;
                    case "PinkTeddyBear": PickupItem("PinkTeddyBear", currentItemCollider); break;
                    case "YellowTeddyBear": PickupItem("YellowTeddyBear", currentItemCollider); break;
                    case "Cake": PickupItem("Cake", currentItemCollider); break;
                    case "NPC": PickupItem("NPC", currentItemCollider); break;
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
        dialogueEventManager = FindObjectOfType<DialogueEventManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
}
