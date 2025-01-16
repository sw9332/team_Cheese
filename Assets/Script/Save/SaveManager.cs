using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerControl player;
    private ItemManager itemManager;
    private InventoryManager inventoryManager;

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
        playerData.position = player.transform.position;
        playerData.move = player.isMove;

        gameData.state = GameManager.GameState;
        gameData.end = GameManager.GameEnd;

        for (int i = 0; i < inventoryData.slot.Length; i++)
        {
            inventoryData.slot[i] = inventoryManager.SlotDB[i];
        }

        itemDataSave = new List<ItemData>(itemDataCurrent);
    }

    public void Load()
    {
        if ((playerData.position.x != 0 && playerData.position.y != 0) && gameData.state != null)
        {
            player.transform.position = playerData.position;
            player.isMove = playerData.move;

            GameManager.GameState = gameData.state;
            GameManager.GameEnd = gameData.end;

            for (int i = 0; i < inventoryData.slot.Length; i++)
            {
                if (inventoryData.slot[i] != null)
                {
                    inventoryManager.SlotDB[i] = inventoryData.slot[i];
                    inventoryManager.SlotImageDB[i].sprite = itemManager.GetItemSprite(inventoryData.slot[i]);
                }
            }

            itemDataCurrent = new List<ItemData>(itemDataSave);
        }

        else return;
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