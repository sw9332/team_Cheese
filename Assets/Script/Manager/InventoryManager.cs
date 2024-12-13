using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public string[] SlotDB;
    public Image[] SlotImageDB;

    private Player player;
    private PlayerAttack playerattack;
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

        // 이 순서를 먼저 해야 사용 type의 item 조건이 발동됨
        if (!Player.objectCollision && item != null && item.type == Type.사용)
        {
            UseItem(item);
            SlotDB[slotIndex] = null;
            SlotImageDB[slotIndex].sprite = null;
            return;
        }
        else if (!Player.objectCollision && item != null) Instantiate(item.prefab, player.transform.position, Quaternion.identity);
        else if (Player.objectCollision && item != null) Instantiate(item.prefab, Object.pos, Quaternion.identity);
        

        SlotDB[slotIndex] = null;
        SlotImageDB[slotIndex].sprite = null;
    }

    public void UseItem(Item item)
    {
        if (item.id=="Ammo")
        {
            playerattack.bullet.bulletNum += 5;
        }

        if(item.id=="ChocoBar")
        {
            // get HP Object on Player HP
            GameObject hpObj = GameObject.Find("Player HP").transform.GetChild(GameObject.Find("Player HP").transform.childCount - 1).gameObject;

            // set hpObj's position on UI system
            RectTransform rectTransform = hpObj.GetComponent<RectTransform>();
            Vector2 newAnchoredPosition = rectTransform.anchoredPosition + new Vector2(100, 0);

            // Locate newly generated Hp 
            GameObject newHpObj = Instantiate(hpObj, hpObj.transform.parent); // 부모 유지
            RectTransform newRectTransform = newHpObj.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = newAnchoredPosition;

            playerattack.hp.Add(hpObj);
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
    }
}
