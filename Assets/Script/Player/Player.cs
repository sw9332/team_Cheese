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

            case "파티룸 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "파티룸 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "연회장 입구 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "연회장 입구 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "연회장 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "연회장 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "창고 입구 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "창고 입구 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "창고 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "창고 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Stage1 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "보스 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomE Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomE Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomF Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomF Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 연회장 입구 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 연회장 입구 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 연회장 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 연회장 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 창고 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 창고 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 창고 입구 (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 창고 입구 (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "Chapter 2 CCTV Room (입구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "Chapter 2 CCTV Room (출구)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
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