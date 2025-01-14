using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerControl player;
    private ItemManager itemManager;
    private InventoryManager inventoryManager;

    [Header("플레이어 저장 데이터")]
    public Vector2 PlayerPosition;
    public bool Move;

    [Header("게임 상태 저장 데이터")]
    public string gameSate;
    public bool gameEnd;

    [Header("인벤토리 저장 데이터")]
    public string[] Inventory;

    public void Save()
    {
        PlayerPosition = player.transform.position;
        Move = player.isMove;

        gameSate = GameManager.GameState;
        gameEnd = GameManager.GameEnd;

        for (int i = 0; i < Inventory.Length; i++)
        {
            Inventory[i] = inventoryManager.SlotDB[i];
        }
    }

    public void Load()
    {
        if ((PlayerPosition.x != 0 && PlayerPosition.y != 0) && gameSate != null)
        {
            player.transform.position = PlayerPosition;
            player.isMove = Move;

            GameManager.GameState = gameSate;
            GameManager.GameEnd = gameEnd;

            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] != null)
                {
                    inventoryManager.SlotDB[i] = Inventory[i];
                    inventoryManager.SlotImageDB[i].sprite = itemManager.GetItemSprite(Inventory[i]);
                }
            }
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