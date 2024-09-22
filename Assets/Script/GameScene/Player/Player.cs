using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Slider stamina;

    private DialogueManager dialogueManager;

    [SerializeField]
    public Dialogue d_cake;
    [SerializeField]
    public Dialogue d_photo;
    [SerializeField]
    public Dialogue d_cutScene;

    public static Vector3 pos;
    public static string object_collision = "땅"; //충돌 확인(땅/사물)

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "사물")
        {
            object_collision = "사물";
        }
    }

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

        if (other.gameObject.tag == "Tutorial Go")
        {
            transform.position = new Vector3(57.52f, -1.8f, 0);
        }

        if (other.gameObject.tag == "Tutorial Exit")
        {
            transform.position = new Vector3(57.52f, -11.3f, 0);
        }

        if(other.gameObject.tag == "RoomA Go")
        {
            transform.position = new Vector3(43.88f, 7.5f, 0f);
        }

        if (other.gameObject.tag == "RoomA Exit")
        {
            transform.position = new Vector3(43.88f, -2.33f, 0f);
        }

        if (other.gameObject.tag == "RoomB Go")
        {
            transform.position = new Vector3(59f, 19.67f, 0f);
        }

        if (other.gameObject.tag == "RoomB Exit")
        {
            transform.position = new Vector3(46.61f, 19.67f, 0f);
        }

        if (other.gameObject.tag == "RoomC Go")
        {
            transform.position = new Vector3(11.96f, -1.7f, 0f);
        }

        if (other.gameObject.tag == "RoomC Exit")
        {
            transform.position = new Vector3(11.96f, -11.6f, 0f);
        }

        if (other.gameObject.tag == "RoomD Go")
        {
            transform.position = new Vector3(11.96f, 18.5f, 0f);
        }

        if (other.gameObject.tag == "RoomD Exit")
        {
            transform.position = new Vector3(11.96f, 8.6f, 0f);
        }

        if (other.gameObject.tag == "RoomE Go")
        {
            transform.position = new Vector3(27.76f, -49.45f, 0f);
        }

        if (other.gameObject.tag == "RoomE Exit")
        {
            transform.position = new Vector3(41.15f, -42.31f, 0f);
        }

        if (other.gameObject.tag == "RoomF Go")
        {
            transform.position = new Vector3(40.97f, -58.25f, 0f);
        }

        if (other.gameObject.tag == "RoomF Exit")
        {
            transform.position = new Vector3(28.05f, -63f, 0f);
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
}