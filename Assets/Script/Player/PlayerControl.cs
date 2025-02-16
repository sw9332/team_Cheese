using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private Stamina stamina;
    private CutSceneManager cutSceneManager;
    private TutorialManager tutorialManager;
    private GameManager gameManager;
    private NPC npc;
    private DialogueManager dialogueManager;

    public Animator animator; // player attack and movement

    public static float speed = 2.5f;
    public static bool isPush = false;
    public static bool isPull = false;

    public static bool MoveX = false;
    public static bool MoveY = false;

    public bool isMove = true; // if isMove == false -> can't move
    public bool stop = true;

    public Vector3 CenterOffset; // player Gizmo function related
    public string Direction = "Down"; // Up, Down, Left, Right

    public void MoveDirection(string direction)
    {
        switch (direction)
        {
            case "Up": animator.Play("PlayerUp"); Direction = direction; break;
            case "Down": animator.Play("PlayerDown"); Direction = direction; break;
            case "Left": animator.Play("PlayerLeft"); Direction = direction; break;
            case "Right": animator.Play("PlayerRight"); Direction = direction; break;
        }

        stop = false;
    }

    public void StopDirection(string direction)
    {
        switch (direction)
        {
            case "Up": animator.Play("PlayerUp_Stop"); Direction = direction; break;
            case "Down": animator.Play("PlayerDown_Stop"); Direction = direction; break;
            case "Left": animator.Play("PlayerLeft_Stop"); Direction = direction; break;
            case "Right": animator.Play("PlayerRight_Stop"); Direction = direction; break;
            case "lying down": animator.Play("lying down"); Direction = direction; break;
            case "Wake Up": animator.Play("Wake Up"); Direction = direction; break;
            case "Right Block": animator.Play("Right Block"); Direction = direction; break;
        }

        stop = true;
    }

    void MoveControl()
    {
        if (isMove && cutSceneManager.Move && !cutSceneManager.isCutScene && !Damage.Instance.isChangingSprite && !playerAttack.isAttacking && !tutorialManager.TutorialUI.activeSelf)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        MoveDirection("Left");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        MoveDirection("Right");
                    else if (Input.GetKey(KeyCode.DownArrow))
                        StopDirection("Down");
                    else
                        MoveDirection("Up");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        MoveDirection("Left");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        MoveDirection("Right");
                    else if (Input.GetKey(KeyCode.UpArrow))
                        StopDirection("Down");
                    else
                        MoveDirection("Down");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.RightArrow)) StopDirection("Down");
                    else MoveDirection("Left");
                }

                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) StopDirection("Down");
                    else MoveDirection("Right");
                }


                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && Direction == "Up") StopDirection("Up");
            if (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && Direction == "Down") StopDirection("Down");
            if (Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && Direction == "Left") StopDirection("Left");
            if (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Direction == "Right") StopDirection("Right");

            if (Input.GetKey(KeyCode.LeftShift) && stamina.Hp > 1)
            {
                if (!isPush)
                {
                    speed = 4f;
                    animator.speed = 2f;
                    stamina.isPlayerRunning = true;
                }

                else
                {
                    speed = 2f;
                    animator.speed = 1f;
                    stamina.isPlayerRunning = false;
                }
            }

            else
            {
                animator.speed = 1;
                speed = 2.5f;
                stamina.isPlayerRunning = false;
            }
        }

        else if (!isMove || tutorialManager.TutorialUI.activeSelf)
        {
            switch (Direction)
            {
                default: StopDirection(Direction); break;
            }

            animator.speed = 1;
        }

        if (dialogueManager.dialogue_continue) stamina.isPlayerRunning = false;
    }

    void PushControl()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isPush)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) animator.Play("LeftPush");
            else if (Input.GetKey(KeyCode.RightArrow)) animator.Play("RightPush");
            else if (Input.GetKey(KeyCode.UpArrow)) animator.Play("UpPush");
            else if (Input.GetKey(KeyCode.DownArrow)) animator.Play("DownPush");
        }

        else isPush = false;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (npc.attackDamage && (other.CompareTag("Boss") || other.CompareTag("Boss Bullet")))
            StartCoroutine(Damage.Instance.ChangeToDamaged(1.0f));
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Push_Object")) isPush = true;
        if (other.CompareTag("MiniGame_Tutorial")) UIManager.is_playerPos = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Push_Object")) isPush = false;
        if (other.CompareTag("MiniGame_Tutorial")) UIManager.is_playerPos = false;
    }

    void Update()
    {
        MoveControl();
        PushControl();
    }

    void Start()
    {
        stamina = FindFirstObjectByType<Stamina>();
        playerAttack = FindFirstObjectByType<PlayerAttack>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        npc = FindFirstObjectByType<NPC>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
}