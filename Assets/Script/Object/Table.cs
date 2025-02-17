using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Table : MonoBehaviour
{
    public static bool trigger = false;

    public GameObject CameraEvent;

    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private InventoryManager inventoryManager;
    private ItemManager itemManager;

    private bool isCake = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) trigger = true;
        if (dialogueManager.dialogue_continue) return;

        if (other.CompareTag("Cake") && !isCake) //����ũ�� ���̺� �÷�������
        {
            isCake = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) trigger = false;
    }

    void Update()
    {
        if (inventoryManager.miniGameCamera && isCake) //����ũ�� �÷����� ���¿��� ī�޶� ȹ�� ������
        {
            if (CameraEvent != null && UIManager.is_bear) CameraEvent.SetActive(true);
            else if (CameraEvent != null && !UIManager.is_bear) CameraEvent.SetActive(false);
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