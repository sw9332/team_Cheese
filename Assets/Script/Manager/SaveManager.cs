using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerControl player;
    private ItemManager itemManager;
    private InventoryManager inventoryManager;

    public Vector2 PlayerPosition;
    public string GameSate;
    public string[] Inventory;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPosition.x = player.transform.position.x;
            PlayerPosition.y = player.transform.position.y;
            GameSate = GameManager.GameState;

            for(int i = 0; i < Inventory.Length; i++)
            {
                Inventory[i] = inventoryManager.SlotDB[i];
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PlayerPosition != null && GameSate != null && Inventory != null)
            {
                player.transform.position = PlayerPosition;
                GameManager.GameState = GameSate;

                for (int i = 0; i < Inventory.Length; i++)
                {
                    inventoryManager.SlotDB[i] = Inventory[i];
                    inventoryManager.SlotImageDB[i].sprite = itemManager.GetItemSprite(Inventory[i]);
                }
            }

            else return;
        }
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        itemManager = FindFirstObjectByType<ItemManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }
}
