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

    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event":
                dialogueManager.ShowDialogue(dialogueContentManager.d_cake);
                Destroy(other.gameObject);
                break;
            case "Camera Event":
                dialogueManager.ShowDialogue(dialogueContentManager.d_photo);
                Destroy(other.gameObject);
                break;
        }

        switch(other.gameObject.tag)
        {
            case "Tutorial Go": teleportManager.teleport("Tutorial Room", playerCollider); break;
            case "Tutorial Exit": teleportManager.teleport("In front Tutorial Room", playerCollider); break;

            case "RoomA Go": teleportManager.teleport("RoomA Go", playerCollider); break;
            case "RoomA Exit": teleportManager.teleport("RoomA Exit", playerCollider); break;

            case "RoomB Go": teleportManager.teleport("RoomB Go", playerCollider); break;
            case "RoomB Exit": teleportManager.teleport("RoomB Exit", playerCollider); break;

            case "RoomC Go": teleportManager.teleport("RoomC Go", playerCollider); break;
            case "RoomC Exit": teleportManager.teleport("RoomC Exit", playerCollider); break;

            case "RoomD Go": teleportManager.teleport("RoomD Go", playerCollider); break;
            case "RoomD Exit": teleportManager.teleport("RoomD Exit", playerCollider); break;

            case "RoomE Go": teleportManager.teleport("RoomE Go", playerCollider); break;
            case "RoomE Exit": teleportManager.teleport("RoomE Exit", playerCollider); break;

            case "RoomF Go": teleportManager.teleport("RoomF Go", playerCollider); break;
            case "RoomF Exit": teleportManager.teleport("RoomF Exit", playerCollider); break;
        }
    }
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        teleportManager = FindObjectOfType<TeleportManager>();
        playerCollider = GetComponent<Collider2D>();
    }
}