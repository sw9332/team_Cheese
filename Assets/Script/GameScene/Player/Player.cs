using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static string object_collision = "땅"; //충돌 확인 (땅/사물)
    public Vector3 pos; //Player의 현재 위치

    private DialogueManager dialogueManager;

    public Slider stamina;
    public List<GameObject> hp = new List<GameObject>();

    [SerializeField]
    public Dialogue d_cake;
    [SerializeField]
    public Dialogue d_photo;
    [SerializeField]
    public Dialogue d_cutScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Cake Event") //케이크 이벤트
        {
            dialogueManager.ShowDialogue(d_cake);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Camera Event") // 케이크를 테이블에 놓았을 때 생기는 이벤트에 닿았을 때
        {
            dialogueManager.ShowDialogue(d_photo);
            //UIManager.is_cake = true;
            Destroy(other.gameObject);
            //MiniGame.is_take_photo = true;
            //MiniGame.is_minigame = true;
        }

        switch(other.gameObject.tag)
        {
            case "Tutorial Go": transform.position = new Vector3(57.52f, -1.8f, 0); break;
            case "Tutorial Exit": transform.position = new Vector3(57.52f, -11.3f, 0); break;

            case "RoomA Go": transform.position = new Vector3(43.88f, 7.5f, 0f); break;
            case "RoomA Exit": transform.position = new Vector3(43.88f, -2.33f, 0f); break;

            case "RoomB Go": transform.position = new Vector3(59f, 19.67f, 0f); break;
            case "RoomB Exit": transform.position = new Vector3(46.61f, 19.67f, 0f); break;

            case "RoomC Go": transform.position = new Vector3(11.96f, -1.7f, 0f); break;
            case "RoomC Exit": transform.position = new Vector3(11.96f, -11.6f, 0f); break;

            case "RoomD Go": transform.position = new Vector3(11.96f, 18.5f, 0f); break;
            case "RoomD Exit": transform.position = new Vector3(11.96f, 8.6f, 0f); break;

            case "RoomE Go": transform.position = new Vector3(27.76f, -49.45f, 0f); break;
            case "RoomE Exit": transform.position = new Vector3(41.15f, -42.31f, 0f); break;

            case "RoomF Go": transform.position = new Vector3(40.97f, -58.25f, 0f); break;
            case "RoomF Exit": transform.position = new Vector3(28.05f, -63f, 0f); break;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        pos = transform.position;
    }
}