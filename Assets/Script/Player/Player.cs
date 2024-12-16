using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool objectCollision = false;

    private PlayerAttack playerAttack;
    private TeleportManager teleportManager;
    private DialogueContentManager dialogueContentManager;
    private UIManager uiManager;
    private TutorialManager tutorialManager;
    private CutSceneManager cutSceneManager;

    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event":
                StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_cake));
                tutorialManager.TutorialType(2);
                Destroy(other.gameObject);
                break;

            case "Camera Event":
                StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_photo));
                tutorialManager.TutorialType(7);
                Destroy(other.gameObject);
                break;

            case "Last Vibration":
                StartCoroutine(cutSceneManager.isVibrationEvent());
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
        }

        if (other.CompareTag("Boss") || other.CompareTag("Boss Bullet"))
        {
            if (playerAttack.hp != null && playerAttack.hp.Count > 0)
            {
                GameObject lastHp = playerAttack.hp[playerAttack.hp.Count - 1];
                playerAttack.hp.RemoveAt(playerAttack.hp.Count - 1);
                Destroy(lastHp);
                StartCoroutine(Damage());
            }
        }
    }

    public IEnumerator Damage()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    void Start()
    {
        playerAttack = FindFirstObjectByType<PlayerAttack>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        teleportManager = FindFirstObjectByType<TeleportManager>();
        uiManager = FindFirstObjectByType<UIManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        playerCollider = GetComponent<Collider2D>();
    }
}