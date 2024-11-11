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
    private Inventory inventory;

    private bool isCake = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (dialogueManager.dialogue_continue) return;

        var TeddyBearItem = other.gameObject.GetComponent<TeddyBearItem>();

        if (TeddyBearItem != null && TeddyBearItem.isInstalled) return;

        if (other.CompareTag("Cake") && !isCake) //케이크를 테이블에 놓았을때 생기는 이벤트 오브젝트
        {
            CamaraEvent.SetActive(true);
            UIManager.is_cake = true;
            isCake = true;
        }

        else if (other.CompareTag("BrownTeddyBear") || other.CompareTag("PinkTeddyBear") || other.CompareTag("YellowTeddyBear"))
        {
            dialogueManager.ShowDialogue(dialogueContentManager.d_not_a_cake);

            for (int i = 0; i < inventory.SlotDB.Length; i++)
                if (inventory.SlotDB[i] == null)
                {
                    inventory.SlotDB[i] = other.tag;
                    inventory.SlotImageDB[i].sprite = inventory.GetItemSprite(other.tag);
                    TeddyBearItem.isInstalled = true;
                    Destroy(other.gameObject);
                    break;
                }
        }
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        inventory = FindFirstObjectByType<Inventory>();
    }
}