using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Table : MonoBehaviour
{
    public GameObject CamaraEvent;

    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private InventoryManager inventoryManager;
    private ItemManager itemManager;

    private bool isCake = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (dialogueManager.dialogue_continue) return;

        var TeddyBear = other.gameObject.GetComponent<TeddyBear>();

        if (TeddyBear != null && TeddyBear.isInstalled) return;

        if (other.CompareTag("Cake") && !isCake && inventoryManager.Camera) //케이크를 테이블에 놓았을때 생기는 이벤트 오브젝트
        {
            isCake = true;
            CamaraEvent.SetActive(true);
            UIManager.is_cake = true;
        }

        else if (other.CompareTag("BrownTeddyBear") || other.CompareTag("PinkTeddyBear") || other.CompareTag("YellowTeddyBear"))
        {
            dialogueManager.ShowDialogue(dialogueContentManager.d_not_a_cake);

            for (int i = 0; i < inventoryManager.SlotDB.Length; i++)
                if (inventoryManager.SlotDB[i] == null)
                {
                    inventoryManager.SlotDB[i] = other.tag;
                    inventoryManager.SlotImageDB[i].sprite = itemManager.GetItemSprite(other.tag);
                    TeddyBear.isInstalled = true;
                    Destroy(other.gameObject);
                    break;
                }
        }
    }

    void Update()
    {
        if (inventoryManager.Camera)
        {
            if (CamaraEvent != null) CamaraEvent.SetActive(true);
            UIManager.is_cake = true;
        }
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
    }
}