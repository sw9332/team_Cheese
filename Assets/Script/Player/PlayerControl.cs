using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    private PlayerAttack playerAttack;
    private Stamina stamina;

   // public SpriteRenderer rend; // player 스프라이트 (바라보는 방향 설정)
    public Animator animator; // player 이동 및 공격 애니메이션

    public static float speed = 2.5f;

    public static bool MoveX = false;
    public static bool MoveY = false;

    public bool isMove = true; // isMove가 false 일때는 움직일 수 없음.
    public bool isPush = false; // isPush가 false 일때는 Push Object를 밀 수 없음.

    public Vector3 CenterOffset; // player 범위판별 offset
    public int Direction = 2; // 1: 뒤, 2: 정면, 3: 왼쪽, 4: 오른쪽

    void OnTriggerStay2D(Collider2D other)
    {
        //오브젝트 밀기
        if (other.gameObject.CompareTag("Push_Object"))
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
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Push_Object")) isPush = false;
    }

    void MoveControl() //플레이어의 이동
    {
        animator.speed = 1;

        if (!isMove && !playerAttack.isChangingSprite) animator.Play("PlayerBack_Stop");

        if (isMove)
        {
            //위로 이동
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) animator.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow)) animator.Play("PlayerRight");
                    else animator.Play("PlayerUp");
                    if (Input.GetKey(KeyCode.DownArrow)) animator.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * speed * Time.deltaTime);
                Direction = 1;  // 위 방향
            }

            //아래로 이동
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) animator.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow)) animator.Play("PlayerRight");
                    else animator.Play("PlayerBack");
                    if (Input.GetKey(KeyCode.UpArrow)) animator.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * speed * Time.deltaTime);
                Direction = 2;  // 아래 방향
            }

            //왼쪽으로 이동
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!isPush) animator.Play("PlayerLeft");

                MoveX = true;
                MoveY = false;

                CenterOffset.x = -0.05f;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                Direction = 3;  // 왼쪽 방향
            }

            //오른쪽으로 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isPush) animator.Play("PlayerRight");

                MoveX = true;
                MoveY = false;

                CenterOffset.x = 0.05f;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                Direction = 4;  // 오른쪽 방향
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && Direction == 1) animator.Play("PlayerUp_Stop");
            else if (Input.GetKeyUp(KeyCode.DownArrow) && Direction == 2) animator.Play("PlayerBack_Stop");

            else if (Input.GetKeyUp(KeyCode.LeftArrow) && Direction == 3 && !Input.GetKey(KeyCode.RightArrow))
            {
                CenterOffset = new Vector3(0f, -0.4f, 0f);
                animator.Play("PlayerLeft_Stop");
            }

            else if (Input.GetKeyUp(KeyCode.RightArrow) && Direction == 4 && !Input.GetKey(KeyCode.LeftArrow))
            {
                CenterOffset = new Vector3(0f, -0.4f, 0f);
                animator.Play("PlayerRight_Stop");
            }

            //달리기
            if (Input.GetKey(KeyCode.LeftShift) && playerStamina.value > 0.01f)
            {
                speed = 5;

                if (!isPush) animator.speed = 2;
                else animator.speed = 1;

                if (!isPush) stamina.isPlayerRunning = true;
                else stamina.isPlayerRunning = false;
            }

            else
            {
                animator.speed = 1;
                speed = 2.5f;
                stamina.isPlayerRunning = false;
            }
        }
    }

    public bool Minigame_PlayerPos()
    {
        //아래 조건문에도 스테이지 별로 || 연산자를 이용하여 조건식을 추가해줄 것.
        if (transform.position.y <= 48.5 && transform.position.y >= 47.5 && transform.position.x <= -76 && transform.position.x >= -78) //튜토리얼 Pos값.
        {
            return true;
        }
        return false;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        stamina = FindObjectOfType<Stamina>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    void Update()
    {
        MoveControl();
        UIManager.is_playerPos = Minigame_PlayerPos();
    }
}