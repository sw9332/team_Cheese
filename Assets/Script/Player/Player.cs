using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool objectCollision = false;

    private TeleportManager teleportManager;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private UIManager uiManager;
    private TutorialManager tutorialManager;

    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event":
                StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_cake));
                Destroy(other.gameObject);
                break;

            case "Camera Event":
                StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_photo));
                Destroy(other.gameObject);
                break;
        }

        switch(other.gameObject.tag)
        {
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

            case "RoomE Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomE Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomF Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomF Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        teleportManager = FindObjectOfType<TeleportManager>();
        playerCollider = GetComponent<Collider2D>();
        uiManager = FindObjectOfType<UIManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
}