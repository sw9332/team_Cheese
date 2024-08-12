using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting; // 데이터 쿼리 언어 

public class player : MonoBehaviour
{
    //인벤토리 및 상호작용 처리 ------------------------------------------------------------------------------------------------------------------------

    //슬롯 배열
    public string[] item_main_slot = new string[4];
    public Image[] item_main_slot_Image = new Image[4];

    //플레이어가 오브젝트에 충돌 중인지 체크 (땅/사물)
    public static string object_collision = "땅";

    //상호작용 오브젝트 이름
    public static string ObjectName;

    //아이템 오브젝트
    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;
    public GameObject Camera;

    //아이템 스프라이트
    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;




    //인벤토리---------------------------------------------------------------------------------------------------------


    private DialogueManager dialogueManager;

    //대화내용
    [SerializeField]
    public Dialogue d_cake;

    [SerializeField]
    public Dialogue d_camera;

    [SerializeField]
    public Dialogue d_photo;

    public GameObject CameraUI;

    //슬롯1 아이템 땅에 두기 버튼
    public void Slot1()

    /* 아이템 두기/올려두기 */

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

    /* 아이템 두기/올려두기 끝 */



    /* 아이템 줍기 */

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

                    dialogueManager.ShowDialogue(d_camera); //쓰러진 곰돌이를 주웠을 때 스토리값을 13으로 (카메라 발견)
                    CameraUI.SetActive(true);

                    break;
                }
            }
        }

        if(other.gameObject.tag == "Camera" && Input.GetKeyDown(KeyCode.Space))
        {
           // UIManager.Next_value = 13; //카메라를 주웠을 때 스토리값을 13으로 (카메라 발견)
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

    /* 아이템 줍기 끝 */



    /* 상호작용 처리 */

            //사물에 닿았을때 "사물"로 처리
        if (other.gameObject.tag == "사물")
        {
            object_collision = "사물";
        }

        //오브젝트 상호작용
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Cake Event") //케이크 이벤트
        {
            dialogueManager.ShowDialogue(d_cake);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Camera Event") //케이크를 테이블에 놓았을때 생기는 이벤트에 닿았을때
        {
            dialogueManager.ShowDialogue(d_photo);
            UIManager.Camera_setactive = true;
            Destroy(other.gameObject);
            MiniGame.is_take_photo = true;
            MiniGame.is_minigame = true;
        }

        if(other.gameObject.tag == "Tutorial Exit")
        {
            transform.position = new Vector3(57.52f, -11f, 0);
        }
    }



    //인벤토리 및 상호작용 처리 끝 ---------------------------------------------------------------------------------------------------------------



    //Player 이동 및 컨트롤. 원거리 공격 ---------------------------------------------------------------------------------------------------------

    //Player 이동 및 컨트롤
    public static Vector3 Player_pos;
    public SpriteRenderer rend; //플레이어 스프라이트 (바라보는 방향 설정)
    public Animator Player_move; //플레이어 이동 애니메이션
    public static float Velocity;
    public float moveSpeed = 2.5f;
    public static bool MoveX = false;
    public static bool MoveY = false;
    public Slider playerStamina;


    [SerializeField] Vector3 playerCenterOffset; // player의 범위 판별을 위한 offset

    //원거리 공격 관련
    public GameObject bullet;
    public Transform bulletPos;
    private float fireCooltime = 0.3f;
    private float fireCurtime;


    // 근접 공격 및 enemy와 충돌
    private Collider2D[] meleeAttackableEnemies; // 근접 공격 가능한 적을 담는 Collider 2D 배열
    [SerializeField] Vector2 meleeAttackBoxSize; // Player의 근접 공격 범위 GizmoBox의 크기
    [SerializeField] Vector2 nearEnemyBoxSize; // Player와 Enemy의 Collision 체크를 위한 offset


    // 추후에 공격 애니메이션 추가
    // public Animator Player_Attack;


    /* Player 이동 및 컨트롤 관련 */

    void PlayerControl() //플레이어의 이동 및 인벤토리 컨트롤

    {
        Player_pos = transform.position; //업데이트 될 때 마다 위치 초기화
        Player_move.speed = 1;
        Velocity = 0;

        //위로 이동
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //키가 겹쳤을때
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_move.Play("PlayerLeft");
            else if (Input.GetKey(KeyCode.RightArrow))
                Player_move.Play("PlayerRight");
            else
                Player_move.Play("PlayerUp");

            if (Input.GetKey(KeyCode.DownArrow))
                Player_move.Play("PlayerUp");

            MoveX = true;
            MoveY = false;

            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        //아래로 이동
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //키가 켭쳤을때
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_move.Play("PlayerLeft");
            else if (Input.GetKey(KeyCode.RightArrow))
                Player_move.Play("PlayerRight");
            else
                Player_move.Play("PlayerBack");

            if (Input.GetKey(KeyCode.UpArrow))
                Player_move.Play("PlayerUp");

            MoveX = true;
            MoveY = false;

            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        //왼쪽으로 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //키가 겹쳤을때
            if (Input.GetKey(KeyCode.RightArrow))
                Player_move.Play("PlayerLeft");
            else
                Player_move.Play("PlayerLeft");

            MoveX = false;
            MoveY = true;

            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //오른쪽으로 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //키가 겹쳤을때
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_move.Play("PlayerLeft");
            else
                Player_move.Play("PlayerRight");

            MoveX = false;
            MoveY = true;

            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }


        // 방향키를 때면
        if (Input.GetKeyUp(KeyCode.UpArrow))
            Player_move.Play("PlayerUp_Stop");
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            Player_move.Play("PlayerBack_Stop");
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            Player_move.Play("PlayerLeft_Stop");
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            Player_move.Play("PlayerRight_Stop");
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            Player_move.Play("PlayerStopX");

        //달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
                Player_move.speed = 2;
                moveSpeed = 5;
                Stamina.isPlayerRunning = true;
        }

        else
        {
            Player_move.speed = 1;
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


    void PlayerAttack()
    {
        if (meleeAttackableEnemy())
        {
            if (Input.GetKeyDown(KeyCode.LeftControl)) // 근처의 적군이 감지됐다면
            {
                meleeAttack();
            }
        }
         
            if (Input.GetKeyDown(KeyCode.LeftControl) && meleeAttackableEnemy() == false)
            // 감지된 적군이 없다면 -> 원거리 공격
            {
                rangedAttack();

            }
    }

    void meleeAttack()
    {

    }

    void rangedAttack()
    {
        if (fireCurtime <= 0)
        {
            Instantiate(bullet, bulletPos.position, transform.rotation);
            fireCurtime = fireCooltime;
        }
        fireCurtime -= Time.deltaTime;
    }


    // 근접 공격 -------------------------------------------------------------------------------------------

    
    /* Player의 enemy 탐지 Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(meleeAttackBoxSize.x, meleeAttackBoxSize.y));
    }
    */

    /*  근접 공격 설명
     *  
      linq(데이터 쿼리 언어)를 이용해서 빠른 정렬
      Gizmo의 범위 안에 존재하는 모든 2D 콜라이더를 가져옴
      => 람다
      Where: 조건을 만족하는 요소 필터링  ,  OrderBy: 오름차순 정렬 ,   ToArray: 배열로 변환
      'enemy' 태그를 가진 PolygonCollider2D만 필터링

     */

    private bool meleeAttackableEnemy() 
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerCenterOffset, meleeAttackBoxSize, 0f);

      
        meleeAttackableEnemies = enemyArray
            .Where(collider => collider.gameObject.layer == 6 /*LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
            .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
             .ToArray();

        if (meleeAttackableEnemies.Length > 0)
        {
            Debug.Log("Melee Attackable Enemy: " + meleeAttackableEnemies[0].name);
            return true;
        }
        else
            return false;
    }


    // Player HP ---------------------------------------------------------------------
    public List<GameObject> hp = new List<GameObject> ();
    private Collider2D[] nearEnemies;

    /* HP 관련 Gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f,3f,0f,0.5f);
        Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(nearEnemyBoxSize.x, nearEnemyBoxSize.y));
    }
    */

    /* CollideWithEnemy 함수 설명
      'enemy' 태그를 가진 PolygonCollider2D만 필터링
       => : 람다

       Where: 조건을 만족하는 요소 필터링
       OrderBy: 오름차순 정렬
       ToArray: 배열로 변환
       적을 찾은 경우에만 가장 가까운 enemy 출력
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

    // Hp UI 파괴 
    private float elapsedTime = 0f;
    private float destroyTime = 1f;
    private bool isCollidingWithEnemy= false;

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
                    hp.RemoveAt(hp.Count - 1);
                    Destroy(lastHp);
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
        meleeAttackBoxSize = new Vector2(1.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);

        Player_move.Play("PlayerBack_Stop");
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        PlayerAttack();
        PlayerControl();
    }
}