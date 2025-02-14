using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance = null;

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        
        else
        {
            Destroy(this.gameObject);
        }
    }

    private PlayerControl player;
    private ItemManager itemManager;
    private InventoryManager inventoryManager;

    public Bullet bullet;

    [Header("플레이어 저장 데이터")]
    public PlayerData playerData;

    [Header("게임 상태 저장 데이터")]
    public GameData gameData;

    [Header("인벤토리 저장 데이터")]
    public InventoryData inventoryData;

    [Header("아이템 저장 데이터")]
    public List<ItemData> itemDataCurrent = new List<ItemData>();
    public List<ItemData> itemDataSave = new List<ItemData>();

    public void Save()
    {
        player = FindFirstObjectByType<PlayerControl>();
        itemManager = FindFirstObjectByType<ItemManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        playerData.position.x = player.transform.position.x;
        playerData.position.y = player.transform.position.y;
        PlayerPrefs.SetFloat("Player Position X", playerData.position.x);
        PlayerPrefs.SetFloat("Player Position Y", playerData.position.y);

        playerData.direction = player.Direction;
        PlayerPrefs.SetString("Player Direction", playerData.direction);

        inventoryData.miniGameCamera = inventoryManager.miniGameCamera;
        PlayerPrefs.SetInt("Camera", System.Convert.ToInt16(inventoryData.miniGameCamera));

        playerData.bullet = bullet.bulletNum;
        PlayerPrefs.SetInt("Bullet", playerData.bullet);

        gameData.state = GameManager.GameState;
        PlayerPrefs.SetString("Game State", gameData.state);

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
            playerData.direction = PlayerPrefs.GetString("Player Direction");

            switch (playerData.direction)
            {
                case "Up": player.transform.position = new Vector2(playerData.position.x, playerData.position.y - 0.5f); break;
                case "Down": player.transform.position = new Vector2(playerData.position.x, playerData.position.y + 0.5f); break;
                case "Left": player.transform.position = new Vector2(playerData.position.x + 0.5f, playerData.position.y); break;
                case "Right": player.transform.position = new Vector2(playerData.position.x - 0.5f, playerData.position.y); break;
            }
            

            inventoryData.miniGameCamera = System.Convert.ToBoolean(PlayerPrefs.GetInt("Camera"));
            inventoryManager.miniGameCamera = inventoryData.miniGameCamera;

            playerData.bullet = PlayerPrefs.GetInt("Bullet");
            bullet.bulletNum = playerData.bullet;

            gameData.state = PlayerPrefs.GetString("Game State");
            GameManager.GameState = gameData.state;

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

    public void DeleteKey()
    {
        PlayerPrefs.DeleteKey("Player Position X");
        PlayerPrefs.DeleteKey("Player Position Y");
        PlayerPrefs.DeleteKey("Player Direction");
        PlayerPrefs.DeleteKey("Camera");
        PlayerPrefs.DeleteKey("Bullet");
        PlayerPrefs.DeleteKey("Game State");
        PlayerPrefs.DeleteKey("Game End");
        PlayerPrefs.DeleteKey("Item Data");
        PlayerPrefs.DeleteKey("Save");

        for (int i = 0; i < inventoryData.slot.Length; i++)
        {
            PlayerPrefs.DeleteKey("Inventory Slot " + i);
        }

        GameManager.Save = false;
        GameManager.Load = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Load();
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector2 position;
    public string direction;
    public int hp;
    public int bullet;
}

[System.Serializable]
public class GameData
{
    public string state;
}

[System.Serializable]
public class InventoryData
{
    public string[] slot;
    public bool miniGameCamera;
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