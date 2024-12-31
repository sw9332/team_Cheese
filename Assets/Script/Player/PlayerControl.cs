using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    private PlayerAttack playerAttack;
    private Stamina stamina;
    private CutSceneManager cutSceneManager;
    private TutorialManager tutorialManager;
    private GameManager gameManager;
    private NPC npc;
    private DialogueManager dialogueManager;

    public Animator animator; // player attack and movement

    public static float speed = 2.5f;

    public static bool MoveX = false;
    public static bool MoveY = false;

    public bool GameEnd = false;
    public bool isMove = true; // if isMove == false -> can't move
    public bool isPush = false; // if isPush == false -> can't push Push Object.

    public Vector3 CenterOffset; // player Gizmo function related
    public string Direction = "Down"; // Up, Down, Left, Right

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss") || other.CompareTag("Boss Bullet"))
        {
            if(npc.attackDamage)
            {
                if (playerAttack.hp != null && playerAttack.hp.Count > 1)
                {
                    GameObject lastHp = playerAttack.hp[playerAttack.hp.Count - 1];
                    playerAttack.hp.RemoveAt(playerAttack.hp.Count - 1);
                    Destroy(lastHp);
                }

                else if (playerAttack.hp.Count <= 1 && !GameEnd)
                {
                    StartCoroutine(gameManager.GameOver());
                    GameEnd = true;
                }

                StartCoroutine(Damage());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Push Object movement
        if (other.CompareTag("Push_Object"))
        {
            isPush = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isPush && Input.GetKey(KeyCode.LeftArrow)) animator.Play("LeftPush");
                else if (isPush && Input.GetKey(KeyCode.RightArrow)) animator.Play("RightPush");
                else if (isPush && Input.GetKey(KeyCode.UpArrow)) animator.Play("UpPush");
                else if (isPush && Input.GetKey(KeyCode.DownArrow)) animator.Play("DownPush");
            }

            else isPush = false;
        }

        if(other.CompareTag("MiniGame_Tutorial"))
        {
            UIManager.is_playerPos = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Push_Object")) isPush = false;

        if (other.CompareTag("MiniGame_Tutorial"))
        {
            UIManager.is_playerPos = false;
        }
    }

    public void MoveDirection(string direction)
    {
        switch (direction)
        {
            case "Up": animator.SetBool("Up", true); break;
            case "Down": animator.SetBool("Down", true); break;
            case "Left": animator.SetBool("Left", true); break;
            case "Right": animator.SetBool("Right", true); break;
        }
    }

    public void StopDirection(string direction)
    {
        switch(direction)
        {
            case "Up": animator.SetBool("Up", false); break;
            case "Down": animator.SetBool("Down", false); break;
            case "Left": animator.SetBool("Left", false); break;
            case "Right": animator.SetBool("Right", false); break;
        }
    }

    void MoveControl()
    {
        if (isMove && !cutSceneManager.isCutScene && !playerAttack.isChangingSprite && !playerAttack.isAttacking && !tutorialManager.TutorialUI.activeSelf && cutSceneManager.Move)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!isPush)
                {
                    MoveDirection("Up");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            else StopDirection("Up");

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!isPush)
                {
                    MoveDirection("Down");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }

            else StopDirection("Down");

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!isPush)
                {
                    MoveDirection("Left");
                }

                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            else StopDirection("Left");

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isPush)
                {
                    MoveDirection("Right");
                }


                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            else StopDirection("Right");

            if (Input.GetKey(KeyCode.LeftShift) && stamina.playerStaminaBar.value > 0.01f)
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
                case "Up": StopDirection(Direction); break;
                case "Down": StopDirection(Direction); break;
                case "Left": StopDirection(Direction); break;
                case "Right": StopDirection(Direction); break;
            }

            animator.speed = 1;
        }

        if (dialogueManager.dialogue_continue) stamina.isPlayerRunning = false;
    }

    void DamageControl()
    {
        if (isMove && playerAttack.isChangingSprite)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Direction = "Up";
                animator.Play("PlayerDamagedUp");

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                Direction = "Down";
                animator.Play("PlayerDamagedDown");

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Direction = "Left";
                animator.Play("PlayerDamagedLeft");

                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Direction = "Right";
                animator.Play("PlayerDamagedRight");

                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

    public IEnumerator Damage()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update()
    {
        MoveControl();
        DamageControl();
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        stamina = FindFirstObjectByType<Stamina>();
        playerAttack = FindFirstObjectByType<PlayerAttack>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        npc = FindFirstObjectByType<NPC>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
}