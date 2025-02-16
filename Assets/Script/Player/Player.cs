using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool objectCollision = false;

    private TeleportManager teleportManager;
    private TutorialManager tutorialManager;
    private CutSceneManager cutSceneManager;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event": StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_cake));
                tutorialManager.TutorialType(2);
                Destroy(other.gameObject);
                break;

            case "Camera Event": StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_photo));
                tutorialManager.TutorialType(7);
                Destroy(other.gameObject);
                break;

            case "Last Vibration": StartCoroutine(cutSceneManager.isVibrationEvent());
                Destroy(other.gameObject);
                break;

            case "Event 3 (Chapter 2)": dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event3);
                Destroy(other.gameObject);
                break;

            case "Event 6 (Chapter 2)": dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event6);
                Destroy(other.gameObject);
                break;

            case "Event 7 (Chapter 2)": StartCoroutine(cutSceneManager.CutScene_10_1());
                Destroy(other.gameObject);
                break;

            case "Event 9 (Chapter 2)": StartCoroutine(cutSceneManager.CutScene_10_2());
                Destroy(other.gameObject);
                break;

            case "��Ƽ�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "��Ƽ�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "��ȸ�� �Ա� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "��ȸ�� �Ա� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "��ȸ�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "��ȸ�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "â�� �Ա� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "â�� �Ա� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "â�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "â�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Stage1 (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "���� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomE Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomE Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomF Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomF Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 ��ȸ�� �Ա� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 ��ȸ�� �Ա� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 ��ȸ�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 ��ȸ�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 â�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 â�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 â�� �Ա� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 â�� �Ա� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 �ϴ� ���� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 �ϴ� ���� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 Enemy Room (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 Enemy Room (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 CCTV Room (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 CCTV Room (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
        }
    }

    void Start()
    {
        teleportManager = FindFirstObjectByType<TeleportManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();

        playerCollider = GetComponent<Collider2D>();
    }
}