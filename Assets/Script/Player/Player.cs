using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool objectCollision = false; //충돌 확인 (땅 == false / 사물 == true)
    public Vector3 pos; //Player의 현재 위치

    private TeleportManager teleportManager;
    private DialogueManager dialogueManager;

    public Slider stamina;

    private Collider2D PlayerCollider;

    [SerializeField]
    public Dialogue d_cake;
    [SerializeField]
    public Dialogue d_photo;
    [SerializeField]
    public Dialogue d_camera;
    [SerializeField]
    public Dialogue d_cutScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event":
                dialogueManager.ShowDialogue(d_cake);
                Destroy(other.gameObject);
                break;
            case "Camera Event":
                dialogueManager.ShowDialogue(d_photo);
                //UIManager.is_cake = true;
                Destroy(other.gameObject);
                //MiniGame.is_take_photo = true;
                //MiniGame.is_minigame = true;
                break;
        }

        switch(other.gameObject.tag)
        {
            case "Tutorial Go": teleportManager.teleport("Tutorial Room", PlayerCollider); break;
            case "Tutorial Exit": teleportManager.teleport("In front Tutorial Room", PlayerCollider); break;

            case "RoomA Go": teleportManager.teleport("RoomA Go", PlayerCollider); break;
            case "RoomA Exit": teleportManager.teleport("RoomA Exit", PlayerCollider); break;

            case "RoomB Go": teleportManager.teleport("RoomB Go", PlayerCollider); break;
            case "RoomB Exit": teleportManager.teleport("RoomB Exit", PlayerCollider); break;

            case "RoomC Go": teleportManager.teleport("RoomC Go", PlayerCollider); break;
            case "RoomC Exit": teleportManager.teleport("RoomC Exit", PlayerCollider); break;

            case "RoomD Go": teleportManager.teleport("RoomD Go", PlayerCollider); break;
            case "RoomD Exit": teleportManager.teleport("RoomD Exit", PlayerCollider); break;

            case "RoomE Go": teleportManager.teleport("RoomE Go", PlayerCollider); break;
            case "RoomE Exit": teleportManager.teleport("RoomE Exit", PlayerCollider); break;

            case "RoomF Go": teleportManager.teleport("RoomF Go", PlayerCollider); break;
            case "RoomF Exit": teleportManager.teleport("RoomF Exit", PlayerCollider); break;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        teleportManager = FindObjectOfType<TeleportManager>();
        PlayerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        pos = transform.position;
    }
}