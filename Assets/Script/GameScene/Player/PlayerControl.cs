using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting; // 데이터 쿼리 언어


public class PlayerControl : MonoBehaviour
{
    private Player player;
    private EnemyManager enemyManager;
    private Stamina stamina;

    public SpriteRenderer rend; // player 스프라이트 (바라보는 방향 설정)
    public Animator Player_control; // player 이동 및 공격 애니메이션

    public static float moveSpeed = 2.5f;

    public static bool MoveX = false;
    public static bool MoveY = false;

    public bool isMove = true; // isMove가 false 일때는 움직일 수 없음.
    public bool isPush = false; // isPush가 false 일때는 Push Object를 밀 수 없음.

    private Vector3 playerCenterOffset; // player 범위판별 offset

    // 원거리 공격 관련
    public GameObject bullet;
    public Transform bulletPos;
    public static int playerDirection = 2; // 1: 뒤, 2: 정면, 3: 왼쪽, 4: 오른쪽
    public float fireCooltime;
    private float fireCurtime;

    // 근접공격 및 enemy와 충돌
    [SerializeField] List<GameObject> hp = new List<GameObject>();
    private Collider2D[] meleeAttackableEnemies;
    private Vector2 meleeAttackBoxSize;
    private Vector2 nearEnemyBoxSize;

    // 근접 공격에서 enemy 정보를 받아오기 위해서 설정
    private Collider2D enemyCollider;

    void OnTriggerStay2D(Collider2D other)
    {
        //오브젝트 밀기
        if (other.gameObject.tag == "Push_Object")
        {
            isPush = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isPush && Input.GetKey(KeyCode.LeftArrow))
                    Player_control.Play("LeftPush");
                else if (isPush && Input.GetKey(KeyCode.RightArrow))
                    Player_control.Play("RightPush");
                else if (isPush && Input.GetKey(KeyCode.UpArrow))
                    Player_control.Play("UpPush");
                else if (isPush && Input.GetKey(KeyCode.DownArrow))
                    Player_control.Play("DownPush");
            }

            else
                isPush = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Push_Object")
            isPush = false;
    }

    void Control() //플레이어의 이동
    {
        Player_control.speed = 1;

        if (!isMove)
        {
            Player_control.Play("PlayerBack_Stop");
        }

        if (isMove)
        {
            //위로 이동
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        Player_control.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        Player_control.Play("PlayerRight");
                    else
                        Player_control.Play("PlayerUp");

                    if (Input.GetKey(KeyCode.DownArrow))
                        Player_control.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                playerDirection = 1;  // 위 방향
            }

            //아래로 이동
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        Player_control.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        Player_control.Play("PlayerRight");
                    else
                        Player_control.Play("PlayerBack");

                    if (Input.GetKey(KeyCode.UpArrow))
                        Player_control.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                playerDirection = 2;  // 아래 방향
            }

            //왼쪽으로 이동
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!isPush)
                {
                    Player_control.Play("PlayerLeft");
                }

                MoveX = true;
                MoveY = false;

                playerCenterOffset.x = -0.05f;
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                playerDirection = 3;  // 왼쪽 방향
            }

            //오른쪽으로 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isPush)
                {
                    Player_control.Play("PlayerRight");
                }

                MoveX = true;
                MoveY = false;

                playerCenterOffset.x = 0.05f;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                playerDirection = 4;  // 오른쪽 방향
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && playerDirection == 1)
                Player_control.Play("PlayerUp_Stop");
            else if (Input.GetKeyUp(KeyCode.DownArrow) && playerDirection == 2)
                Player_control.Play("PlayerBack_Stop");

            else if (Input.GetKeyUp(KeyCode.LeftArrow) && playerDirection == 3 && !Input.GetKey(KeyCode.RightArrow))
            {
                playerCenterOffset = new Vector3(0f, -0.4f, 0f);
                Player_control.Play("PlayerLeft_Stop");
            }

            else if (Input.GetKeyUp(KeyCode.RightArrow) && playerDirection == 4 && !Input.GetKey(KeyCode.LeftArrow))
            {
                playerCenterOffset = new Vector3(0f, -0.4f, 0f);
                Player_control.Play("PlayerRight_Stop");
            }

            //달리기
            if (Input.GetKey(KeyCode.LeftShift) && player.stamina.value > 0.01f)
            {
                moveSpeed = 5;

                if (!isPush) Player_control.speed = 2;
                else Player_control.speed = 1;

                if (!isPush) stamina.isPlayerRunning = true;
                else stamina.isPlayerRunning = false;
            }

            else
            {
                Player_control.speed = 1;
                moveSpeed = 2.5f;
                stamina.isPlayerRunning = false;
            }
        }
    }

    void PlayerAttack()
    {
        enemyCollider = meleeAttackableEnemy();

        // 근접 공격 처리
        if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider != null)  // 근접 공격 범위 내에 적군이 감지되었다면
        {
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                meleeAttack();
                enemyManager.takeDamage(enemyCollider.tag);
            }
        }
        // 원거리 공격 처리
        else if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider == null && fireCurtime <= 0) // 쿨타임 확인
        {
            rangedAttack();
        }

        attackStop();

        // bullet에 있던 코드를 끌어옴 , 단발 사격
        if (fireCurtime > 0)
        {
            fireCurtime -= Time.deltaTime;  // 쿨타임 감소
        }
    }

    void meleeAttack()
    {
        if (playerDirection == 1)
        {
            Player_control.Play("PlayerMeleeAttackBack");
        }
        if (playerDirection == 2)
        {
            Player_control.Play("PlayerMeleeAttackFront");
        }
        if (playerDirection == 3)
        {
            Player_control.Play("PlayerMeleeAttackLeft");
        }
        if (playerDirection == 4)
        {
            Player_control.Play("PlayerMeleeAttackRight");
        }
    }

    void rangedAttack()
    {
        // 방향에 따른 애니메이션 설정
        if (playerDirection == 1) // 뒤
        {
            Player_control.Play("PlayerLongAttackBack");
        }
        else if (playerDirection == 2) // 정면
        {
            Player_control.Play("PlayerLongAttackFront");
        }
        else if (playerDirection == 3) // 왼쪽
        {
            Player_control.Play("PlayerLongAttackLeft");
        }
        else if (playerDirection == 4) // 오른쪽
        {
            Player_control.Play("PlayerLongAttackRight");
        }


        // 발사 쿨타임이 끝났을 때만 총알 발사
        Instantiate(bullet, bulletPos.position, transform.rotation);  // 총알 생성
        fireCurtime = fireCooltime; // 쿨타임 초기화
    }

    void attackStop()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (playerDirection == 1)
            {
                Player_control.Play("PlayerUp_Stop");
            }
            else if (playerDirection == 2)
            {
                Player_control.Play("PlayerBack_Stop");
            }
            else if (playerDirection == 3)
            {
                Player_control.Play("PlayerLeft_Stop");
            }
            else if (playerDirection == 4)
            {
                Player_control.Play("PlayerRight_Stop");
            }
        }
    }


    // 근접 공격   -------------------------------------------------------------------------------------------

    public bool showRangeGizmo = false;
    /* Player의 enemy 탐지 Gizmo */
    private void OnDrawGizmosSelected()
    {
        if (showRangeGizmo)
        {
            Gizmos.color = new Color(1.0f, 0f, 0f, 0.8f);
            Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(meleeAttackBoxSize.x, meleeAttackBoxSize.y));
        }
    }

    /*  근접 공격 설명
     linq(데이터 쿼리 언어)를 이용해서 빠른 정렬
     Gizmo의 범위 안에 존재하는 모든 2D 콜라이더를 가져옴
     => : 람다
        Where : 조건을 만족하는 요소 필터링
        OrderBy: 오름차순 정렬
        oArray: 배열로 변환
     'enemy' 태그를 가진 PolygonCollider2D만 필터링

     */

    private Collider2D meleeAttackableEnemy()
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerCenterOffset, meleeAttackBoxSize, 0f);

        meleeAttackableEnemies = enemyArray
        .Where(collider => collider.gameObject.layer == 6 /*6번 Layer가 enemy, LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
        .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
        .ToArray();


        if (meleeAttackableEnemies.Length > 0)
        {
            Debug.Log("Melee Attackable Enemy: " + meleeAttackableEnemies[0].name);
            return meleeAttackableEnemies[0];
        }
        else
            return null;
    }


    // Player HP ---------------------------------------------------------------------
    void getPlayerHP()
    {
        int numHp = GameObject.Find("playerHP").transform.childCount;
        for (int i = 0; i < numHp; i++)
        {
            GameObject hpObj = GameObject.Find("playerHP").transform.GetChild(i).gameObject;
            hp.Add(hpObj);
        }
    }

    /* HP 관련 Gizmo */
    public bool showHPGizmo = false;
    private void OnDrawGizmos()
    {
        if (showHPGizmo)
        {
            Gizmos.color = new Color(0f, 3f, 0f, 0.7f);
            Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(nearEnemyBoxSize.x, nearEnemyBoxSize.y));
        }
    }


    private Collider2D[] nearEnemies;
    private float elapsedTime = 0f;
    private float destroyTime = 1f;
    private bool isCollidingWithEnemy = false;

    /* CollideWithEnemy 함수 설명
   'enemy' 태그를 가진 polygonCollider2D만 필터링
    => : 람다
     Where : 조건을 만족하는 요소 필터링
     OrderBy: 오름차순 정렬
     oArray: 배열로 변환
  */

    public bool CollideWithEnemy()
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerCenterOffset, nearEnemyBoxSize, 0f);

        nearEnemies = enemyArray
            .Where(collider => collider.gameObject.layer == 6 /*LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
            .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
            .ToArray();

        if (nearEnemies.Length > 0)
        {
            Debug.Log("Near Enemy: " + nearEnemies[0].name);
            return true;
        }
        else
            return false;
    }

    public void Player_Collision()
    {
        if (hp != null)
        {
            if (CollideWithEnemy() == true)
            {
                isCollidingWithEnemy = true;
            }
            else
            {
                isCollidingWithEnemy = false;
                elapsedTime = 0f;
            }

            if (isCollidingWithEnemy == true)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= destroyTime && hp.Count > 0)
                {
                    GameObject lastHp = hp[hp.Count - 1];
                    Destroy(lastHp);
                    hp.RemoveAt(hp.Count - 1); 
                    elapsedTime = 0f; // 다시 시간 초기화
                }
            }
        }
    }

    public bool Minigame_PlayerPos()
    {
        //아래 조건문에도 스테이지 별로 || 연산자를 이용하여 조건식을 추가해줄 것.
        if (transform.position.y <= 48.5 && transform.position.y >= 47.5 && transform.position.x <= -76 && transform.position.x >= -78 //튜토리얼 Pos값.
            )
        {
            return true;
        }
        return false;
    }

    void Start()
    {
        getPlayerHP();

        player = FindObjectOfType<Player>();
        enemyManager = FindObjectOfType<EnemyManager>();
        stamina = FindObjectOfType<Stamina>();

        // 범위 판정 offset 값
        meleeAttackBoxSize = new Vector2(2.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);
        fireCooltime = 0.2f;
    }

    void Update()
    {
        Control();
        PlayerAttack();
        Player_Collision();
        UIManager.is_playerPos = Minigame_PlayerPos();
    }
}
