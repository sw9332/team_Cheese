using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerControl player;
    private ItemManager itemManager;
    private InventoryManager inventoryManager;

    [Header("�÷��̾� ���� ������")]
    public PlayerData playerData;

    [Header("���� ���� ���� ������")]
    public GameData gameData;

    [Header("�κ��丮 ���� ������")]
    public InventoryData inventoryData;

    [Header("������ ���� ������")]
    public List<ItemData> itemDataCurrent = new List<ItemData>();
    public List<ItemData> itemDataSave = new List<ItemData>();

    public void Save()
    {
        playerData.position.x = player.transform.position.x;
        playerData.position.y = player.transform.position.y;
        playerData.move = player.isMove;
        PlayerPrefs.SetFloat("Player Position X", playerData.position.x);
        PlayerPrefs.SetFloat("Player Position Y", playerData.position.y);
        PlayerPrefs.SetInt("Move", System.Convert.ToInt16(playerData.move));

        gameData.state = GameManager.GameState;
        gameData.end = GameManager.GameEnd;
        PlayerPrefs.SetString("Game State", gameData.state);
        PlayerPrefs.SetInt("Game End", System.Convert.ToInt16(gameData.end));

        for (int i = 0; i < inventoryData.slot.Length; i++)
        {
            if (inventoryManager.SlotDB[i] != null)
            {
                inventoryData.slot[i] = inventoryManager.SlotDB[i];
                PlayerPrefs.SetString("Inventory Slot " + i, inventoryData.slot[i]);
            }

            else PlayerPrefs.SetString("Inventory Slot " + i, "");
        }

        itemDataSave = new List<ItemData>(itemDataCurrent);

        string itemDataJson = JsonUtility.ToJson(new ItemDataSave(itemDataSave));
        PlayerPrefs.SetString("Item Data", itemDataJson);

        GameManager.Save = true;
        PlayerPrefs.SetInt("Save", System.Convert.ToInt16(GameManager.Save));
    }

    public void Load()
    {
        if (GameManager.Save || GameManager.Load)
        {
            player = FindFirstObjectByType<PlayerControl>();
            itemManager = FindFirstObjectByType<ItemManager>();
            inventoryManager = FindFirstObjectByType<InventoryManager>();

            playerData.position.x = PlayerPrefs.GetFloat("Player Position X");
            playerData.position.y = PlayerPrefs.GetFloat("Player Position Y");
            player.transform.position = playerData.position;

            playerData.move = System.Convert.ToBoolean(PlayerPrefs.GetInt("Move"));
            player.isMove = playerData.move;

            gameData.state = PlayerPrefs.GetString("Game State");
            gameData.end = System.Convert.ToBoolean(PlayerPrefs.GetInt("Game End"));
            GameManager.GameState = gameData.state;
            GameManager.GameEnd = gameData.end;

            for (int i = 0; i < inventoryData.slot.Length; i++)
            {
                inventoryManager.SlotDB[i] = null;
                inventoryManager.SlotImageDB[i].sprite = null;

                string loadedSlotData = PlayerPrefs.GetString("Inventory Slot " + i);

                if (!string.IsNullOrEmpty(loadedSlotData))
                {
                    inventoryManager.SlotDB[i] = loadedSlotData;
                    inventoryManager.SlotImageDB[i].sprite = itemManager.GetItemSprite(loadedSlotData);
                }
            }

            string itemDataJson = PlayerPrefs.GetString("Item Data");
            ItemDataSave loadedItemData = JsonUtility.FromJson<ItemDataSave>(itemDataJson);

            foreach (var itemData in itemDataCurrent)
            {
                DestroyItem(itemData);
            }

            foreach (var itemData in loadedItemData.item)
            {
                GameObject itemPrefab = itemManager.GetItem(itemData.tag).prefab;
                if (itemPrefab != null)
                {
                    GameObject itemInstance = Instantiate(itemPrefab, itemData.position, Quaternion.identity);
                    itemInstance.SetActive(true);
                }
            }

            itemDataCurrent = new List<ItemData>(loadedItemData.item);
        }
    }

    void DestroyItem(ItemData itemData)
    {
        var item = GameObject.FindGameObjectsWithTag(itemData.tag);
        foreach (var obj in item)
        {
            if (obj.transform.position == (Vector3)itemData.position)
            {
                Destroy(obj);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Load();
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        itemManager = FindFirstObjectByType<ItemManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector2 position;
    public bool move;
}

[System.Serializable]
public class GameData
{
    public string state;
    public bool end;
}

[System.Serializable]
public class InventoryData
{
    public string[] slot;
}

[System.Serializable]
public class ItemData
{
    public string tag;
    public Vector2 position;

    public ItemData(string tag, Vector2 position)
    {
        this.tag = tag;
        this.position = position;
    }
}

[System.Serializable]
public class ItemDataSave
{
    public List<ItemData> item;

    public ItemDataSave(List<ItemData> item)
    {
        this.item = item;
    }
}