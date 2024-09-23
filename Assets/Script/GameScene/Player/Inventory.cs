using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public string[] itemDB = new string[4];
    public Image[] itemImageDB = new Image[4];

    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;
    public GameObject NPC_Object;

    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;
    public Sprite NPC_Sprite;

    private DialogueManager dialogueManager;
    private UIManager uiManager;

    // 대화내용
    [SerializeField]
    public Dialogue d_camera;

    // 아이템 배치
    public void PlaceItem(int slotIndex)
    {
        if (itemDB[slotIndex] == null) return;

        GameObject itemObject = GetItemObject(itemDB[slotIndex]);
        Vector3 position = Player.object_collision == "땅" ? Player.pos : Object.pos;

        Instantiate(itemObject, position, Quaternion.identity);
        itemDB[slotIndex] = null;
        itemImageDB[slotIndex].sprite = null;
    }

    // 아이템 줍기 
    GameObject GetItemObject(string item_name)
    {
        if (item_name == "BrownTeddyBear")
            return BrownTeddyBear_Object;
        else if (item_name == "PinkTeddyBear")
            return PinkTeddyBear_Object;
        else if (item_name == "YellowTeddyBear")
            return YellowTeddyBear_Object;
        else if (item_name == "Cake")
            return Cake_Object;
        else if (item_name == "NPC")
            return NPC_Object;

        return null;
    }

    Sprite GetItemSprite(string item_name)
    {
        if (item_name == "BrownTeddyBear")
            return BrownTeddyBear_Sprite;
        else if (item_name == "PinkTeddyBear")
            return PinkTeddyBear_Sprite;
        else if (item_name == "YellowTeddyBear")
            return YellowTeddyBear_Sprite;
        else if (item_name == "Cake")
            return Cake_Sprite;
        else if (item_name == "NPC")
            return NPC_Sprite;

        return null;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "DroppedBrownTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < itemDB.Length; i++)
            {
                if (itemDB[i] == "" || itemDB[i] == null)
                {
                    itemDB[i] = "BrownTeddyBear";
                    itemImageDB[i].sprite = GetItemSprite(itemDB[i]);
                    Destroy(other.gameObject);

                    dialogueManager.ShowDialogue(d_camera);
                    uiManager.CameraUI.SetActive(true);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "BrownTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < itemDB.Length; i++)
            {
                if (itemDB[i] == "" || itemDB[i] == null)
                {
                    itemDB[i] = "BrownTeddyBear";
                    itemImageDB[i].sprite = GetItemSprite(itemDB[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "PinkTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < itemDB.Length; i++)
            {
                if (itemDB[i] == "" || itemDB[i] == null)
                {
                    itemDB[i] = "PinkTeddyBear";
                    itemImageDB[i].sprite = GetItemSprite(itemDB[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "YellowTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < itemDB.Length; i++)
            {
                if (itemDB[i] == "" || itemDB[i] == null)
                {
                    itemDB[i] = "YellowTeddyBear";
                    itemImageDB[i].sprite = GetItemSprite(itemDB[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "Cake" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < itemDB.Length; i++)
            {
                if (itemDB[i] == "" || itemDB[i] == null)
                {
                    itemDB[i] = "Cake";
                    itemImageDB[i].sprite = GetItemSprite(itemDB[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "NPC" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < itemDB.Length; i++)
            {
                if (itemDB[i] == "" || itemDB[i] == null)
                {
                    itemDB[i] = "NPC";
                    itemImageDB[i].sprite = GetItemSprite(itemDB[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }
    }

     void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) PlaceItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlaceItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlaceItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlaceItem(3);
    }
}