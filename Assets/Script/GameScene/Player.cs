using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting; // 데이터 쿼리 언어 

public class Player : MonoBehaviour
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


    /* 아이템 두기/올려두기 */

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
                    Camera.SetActive(true); //쓰러진 곰돌이를 획득하면 카메라가 생김.
                    break;
                }
            }
        }

        if(other.gameObject.tag == "Camera" && Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.Next_value = 13; //카메라를 주웠을 때 스토리값을 13으로 (카메라 발견)
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
        if(other.gameObject.tag == "사물")
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
            UIManager.Next_value = 7;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Camera Event") //케이크를 테이블에 놓았을때 생기는 이벤트에 닿았을때
        {
            UIManager.Next_value = 19;
            UIManager.Camera_setactive = true;
            Destroy(other.gameObject);
        }
    }

    /* 상호작용 처리 끝 */

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

    //원거리 공격 관련 , bullet
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;

    // 추후에 공격 애니메이션 추가
    // public Animator Player_attack;
    // Player_attack에서는 2개 (근접, 원거리)



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
            if (UIManager.StoryUI == true)
                Velocity = 0;
            else
                Velocity = moveSpeed;

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

            transform.Translate(Vector3.up * Velocity * Time.deltaTime);
        }

        //아래로 이동
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            if (UIManager.StoryUI == true)
                Velocity = 0;
            else
                Velocity = moveSpeed;

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

            transform.Translate(Vector3.down * Velocity * Time.deltaTime);
        }

        //왼쪽으로 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            if (UIManager.StoryUI == true)
                Velocity = 0;
            else
                Velocity = moveSpeed;

            //키가 겹쳤을때
            if (Input.GetKey(KeyCode.RightArrow))
                Player_move.Play("PlayerLeft");
            else
                Player_move.Play("PlayerLeft");

            MoveX = false;
            MoveY = true;

            transform.Translate(Vector3.left * Velocity * Time.deltaTime);
        }

        //오른쪽으로 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //대화창이 켜져있을땐 움직이지 않게
            if (UIManager.StoryUI == true)
                Velocity = 0;
            else
                Velocity = moveSpeed;

            //키가 겹쳤을때
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_move.Play("PlayerLeft");
            else
                Player_move.Play("PlayerRight");

            MoveX = false;
            MoveY = true;

            transform.Translate(Vector3.right * Velocity * Time.deltaTime);
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

        //달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Player_move.speed = 2;
            moveSpeed = 5;
        }

        else
        {
            Player_move.speed = 1;
            moveSpeed = 2.5f;
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

    /* Player 이동 및 컨트롤 관련 끝 */



    /* 원거리 공격 */

    void PlayerAttack()
    {
        if (DetectEnemies() == true)
        { 
        }
        else
        {
            if (curtime <= 0)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    Instantiate(bullet, pos.position, transform.rotation);
                }
                curtime = cooltime;
            }
            curtime -= Time.deltaTime;
            // }
        }
    }

    // Player의 범위를 세팅하주는 offset값들
    public Vector3 boxCenterOffset = new Vector3(0.3f, -0.1f);
    public Vector2 boxSize = new Vector2(2f, 2.2f);

    // 감지한 적을 담는 Collider 2D 배열
    private Collider2D[] detectedEnemies;

    // Player의 enemy 탐지 범위
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position + boxCenterOffset, new Vector2(boxSize.x, boxSize.y));
    }

    //linq(데이터 쿼리 언어)를 이용해서 빠른 정렬
    private bool DetectEnemies()
    {

        // Gizmo의 범위 안에 존재하는 모든 2D 콜라이더를 가져옴
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)boxCenterOffset, boxSize, 0f);

        // 'enemy' 태그를 가진 PolygonCollider2D만 필터링
        // => 람다
        detectedEnemies = enemyArray
            // Where: 조건을 만족하는 요소 필터링
            .Where(collider => collider.CompareTag("Enemy") && collider is PolygonCollider2D)
            // OrderBy: 오름차순 정렬
            .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
            // ToArray: 배열로 변환
            .ToArray();

        // 적을 찾은 경우에만 가장 가까운 enemy 출력
        if (detectedEnemies.Length > 0)
        {
            Debug.Log("Closest enemy: " + detectedEnemies[0].name);
            return true;
        }
        else
            return false;
    }

    /* 원거리 공격 끝 */

    //Player 이동 및 컨트롤. 원거리 공격 끝 ------------------------------------------------------------------------------------------------



    //메인 메소드 --------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        Player_move.Play("PlayerBack_Stop");
    }

    void Update()
    {
        DetectEnemies();
        PlayerAttack();
        PlayerControl();
    }
}