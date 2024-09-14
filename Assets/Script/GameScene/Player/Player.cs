using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting; // 데이터 쿼리 언어

public class Player : MonoBehaviour
{
// 인벤토리 및 상호작용 처리

// 슬롯 배열
    public string[] item_main_slot = new string[4];
    public Image[] item_main_slot_Image = new Image[4];

    // 플레이어가 오브젝트에 충동 줄인지 체크 (땅/사물)
    public static string object_collision = "땅";

    // 상호작용 오브젝트 이름
    public static string ObjectName;

    // 아이템 오브젝트 및 스프라이트
    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;
    public GameObject Camera;

    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;

    public EnemyManager enemyManager;
    private DialogueManager dialogueManager;

    // 대화내용
    [SerializeField]
    public Dialogue d_cake;
    [SerializeField]
    public Dialogue d_camera;
    [SerializeField]
    public Dialogue d_photo;

    public GameObject CameraUI;


    // 아이템  두기 / 올려두기
    public void Slot1()
    {
        if(object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Player_pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }

        if(object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Object.Object_pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }
    }

    public void Slot2()
    {
        if (object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Player_pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }

        if (object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Object.Object_pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }
    }

    public void Slot3()
    {
        if (object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Player_pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }

        if (object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Object.Object_pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }
    }

    public void Slot4()
    {
        if (object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Player_pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }

        if (object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Object.Object_pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }
    }


    // 아이템 줍기 
    GameObject GetItemObject(string item_name)
    {
        if (item_name == "BrownTeddyBear")
            return BrownTeddyBear_Object;
        else if (item_name == "PinkTeddyBear")
            return PinkTeddyBear_Object;
        else if (item_name == "YellowTeddyBear")
            return YellowTeddyBear_Object;
        else if (item_name == "Cake")
            return Cake_Object;

        return null;
    }

    Sprite GetItemSprite(string item_name)
    {
        if (item_name == "BrownTeddyBear")
            return BrownTeddyBear_Sprite;
        else if (item_name == "PinkTeddyBear")
            return PinkTeddyBear_Sprite;
        else if (item_name == "YellowTeddyBear")
            return YellowTeddyBear_Sprite;
        else if (item_name == "Cake")
            return Cake_Sprite;

        return null;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "DroppedBrownTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "BrownTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    dialogueManager.ShowDialogue(d_camera); // 쓰레진 곰돌이를 주웠을 때 스토리 값을 13으로 (카메라 발견)
                    CameraUI.SetActive(true);

                    Camera.SetActive(true); //쓰러진 곰돌이를 주우면 카메라 발견
                    break;
                }
            }
        }

        if(other.gameObject.tag == "Camera" && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.ShowDialogue(d_camera); //카메라를 주웠을 때 스토리값을 13으로
            CameraUI.SetActive(true);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "BrownTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "BrownTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if(other.gameObject.tag == "PinkTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "PinkTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if(other.gameObject.tag == "YellowTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "YellowTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if(other.gameObject.tag == "Cake" && Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "Cake";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }


        // 사물에 닿았을 때 "사물"로 처리 
        if (other.gameObject.tag == "사물")
        {
            object_collision = "사물";
        }

        // 오브젝트 상호작용
        if(other.gameObject.tag == "달력")
        {
            if(Input.GetKeyDown(KeyCode.Space))
                ObjectName = "달력";
        }

        if(other.gameObject.tag == "인형")
        {
            if(Input.GetKeyDown(KeyCode.Space))
                ObjectName = "인형";
        }

        if(other.gameObject.tag == "나가는 문")
        {
            if(Input.GetKeyDown(KeyCode.Space))
                ObjectName = "나가는 문";
        }

        //오브젝트 밀기
        if(other.gameObject.tag == "Push_Object")
        {
            is_Push = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (is_Push && Input.GetKey(KeyCode.LeftArrow))
                    Player_control.Play("LeftPush");
                else if (is_Push && Input.GetKey(KeyCode.RightArrow))
                    Player_control.Play("RightPush");
                else if (is_Push && Input.GetKey(KeyCode.UpArrow))
                    Player_control.Play("UpPush");
                else if (is_Push && Input.GetKey(KeyCode.DownArrow))
                    Player_control.Play("DownPush");
            }

            else
                is_Push = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Push_Object")
            is_Push = false;
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
            UIManager.Camera_setactive = true;
            Destroy(other.gameObject);
            MiniGame.is_take_photo = true;
            MiniGame.is_minigame = true;
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
            transform.position = new Vector3(43.88f, 7.15f, 0f);
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



    // Player 이동 및 컨트롤

    public static Vector3 Player_pos;
    public SpriteRenderer rend; // player 스프라이트 (바라보는 방향 설정)
    public Animator Player_control; // player 이동 및 공격 애니메이션
    public static float Velocity;
    public static float moveSpeed = 2.5f;
    public static bool MoveX = false;
    public static bool MoveY = false;
    public bool is_move = true; // is_move가 false 일때는 움직일 수 없음.

    public Slider playerStamina;

    private Vector3 playerCenterOffset; // player 범위판별 offset

    // 원거리 공격 관련
    public GameObject bullet;
    public Transform bulletPos;
    public static int playerDirection = 2; // 1: 뒤, 2: 정면, 3: 왼쪽, 4: 오른쪽 
    public float fireCooltime;
    private float fireCurtime;


    // 근접공격 및 enemy와 충돌
    private Collider2D[] meleeAttackableEnemies;
    public Vector2 meleeAttackBoxSize;
    public Vector2 nearEnemyBoxSize;

    // 근접 공격에서 enemy 정보를 받아오기 위해서 설정
    private Collider2D enemyCollider;

    public static bool is_Push = false;

    void PlayerControl() //플레이어의 이동 및 인벤토리 컨트롤
    {
        Player_pos = transform.position; //업데이트 될 때 마다 위치 초기화
        Player_control.speed = 1;
        Velocity = 0;

        if(is_move == false)
        {
            Player_control.Play("PlayerBack_Stop");
        }

        if(is_move == true)
        {
            //위로 이동
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!is_Push)
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

                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                playerDirection = 1;
            }

            //아래로 이동
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!is_Push)
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

                MoveX = true;
                MoveY = false;

                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                playerDirection = 2 ;
            }

            //왼쪽으로 이동
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(!is_Push)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                        Player_control.Play("PlayerLeft");
                    else
                        Player_control.Play("PlayerLeft");
                }
                
                MoveX = false;
                MoveY = true;

                playerCenterOffset.x = -0.05f;
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                playerDirection = 3;
            }

            //오른쪽으로 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!is_Push)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        Player_control.Play("PlayerLeft");
                    else
                        Player_control.Play("PlayerRight");
                }

                MoveX = false;
                MoveY = true;

                // player가 오른쪽으로 이동할 경우 중심이 변경, offset값으로 중심점을 항상 일치하도록 
                playerCenterOffset.x = 0.05f;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                playerDirection = 4;
            }


            // 방향키를 때면
            if (Input.GetKeyUp(KeyCode.UpArrow))
                Player_control.Play("PlayerUp_Stop");
            else if (Input.GetKeyUp(KeyCode.DownArrow))
                Player_control.Play("PlayerBack_Stop");
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerCenterOffset = new Vector3(0f, -0.4f, 0f);
                Player_control.Play("PlayerLeft_Stop");
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerCenterOffset = new Vector3(0f, -0.4f, 0f);
                Player_control.Play("PlayerRight_Stop");
            }

            //달리기
            if (Input.GetKey(KeyCode.LeftShift) && playerStamina.value > 0.01f)
            {
                Player_control.speed = 2;
                moveSpeed = 5;
                Stamina.isPlayerRunning = true;
            }
            else
            {
                Player_control.speed = 1;
                moveSpeed = 2.5f;
                Stamina.isPlayerRunning = false;
            }

            /* Player 인벤토리 */

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Slot1();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Slot2();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Slot3();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Slot4();
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
        if(playerDirection == 1)
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


    // Player HP ---------------------------------------------------------------------
    public List<GameObject> hp = new List<GameObject>();
    private Collider2D[] nearEnemies;
    private float elapsedTime = 0f;
    private float destroyTime = 1f;
    private bool isCollidingWithEnemy= false;



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
        if( hp != null) { 
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

    // -----------------------------------------------------------------------------------------

    void Start()
    {
        // 범위 판정 offset 값
        playerCenterOffset = new Vector3(0f, -0.4f, 0f);
        meleeAttackBoxSize = new Vector2(2.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);
        fireCooltime = 0.2f;

        Player_control.Play("PlayerBack_Stop");
        dialogueManager = FindObjectOfType<DialogueManager>();

    }

    void Update()
    {
        PlayerAttack();
        PlayerControl();
        Player_Collision();
    }
}