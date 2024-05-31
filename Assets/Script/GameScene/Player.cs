using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //튜토리얼 플레이어 스크립트

    //플레이어 인벤토리---------------------------------------------------------------------------------------

    //아이템 스프라이트
    public Sprite item1Sprite;
    public Sprite item2Sprite;
    public Sprite item3Sprite;
    public Sprite item4Sprite;

    //아이템 오브젝트
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;

    //아이템 이름, 아이템 이미지 관리
    public string[] item_main_slot = new string[4];
    public Image[] item_main_slot_Image = new Image[4];

    public static string object_collision = "땅"; //테이블과 충돌 중인지 확인하는 변수.

    //아이템 두기/올려두기---------------------------------------------------------------

    //아이템 올려두기 버튼
    public GameObject Slot1_Button_objects;
    public GameObject Slot2_Button_objects;
    public GameObject Slot3_Button_objects;
    public GameObject Slot4_Button_objects;

    //아이템 두기 버튼
    public GameObject Slot1_Button_floor;
    public GameObject Slot2_Button_floor;
    public GameObject Slot3_Button_floor;
    public GameObject Slot4_Button_floor;

    //슬롯 버튼-----------------------------------------------------------------------------

    //슬롯1 아이템 땅에 두기 버튼
    public void slot_button1_floor()
    {
        Slot1_Button_floor.SetActive(false);
        Slot1_Button_objects.SetActive(false);

        if(object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Player_pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }
    }

    //슬롯2 아이템 오브젝트 위에 올려두기 버튼
    public void slot_button1_objects()
    {
        Slot1_Button_floor.SetActive(false);
        Slot1_Button_objects.SetActive(false);

        if(object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Object.Object_pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }
    }

    //슬롯1 UI버튼
    public void Slot1_UI_Button()
    {
        if(object_collision == "땅")
        {
            Slot1_Button_floor.SetActive(true);
            Slot1_Button_objects.SetActive(false);
        }

        if(object_collision == "사물")
        {
            Slot1_Button_floor.SetActive(true);
            Slot1_Button_objects.SetActive(true);
        }
    }

    //-------------------------------------------------------------------------------------------

    //슬롯2 아이템 땅에 두기 버튼
    public void slot_button2_floor()
    {
        Slot2_Button_floor.SetActive(false);
        Slot2_Button_objects.SetActive(false);

        if(object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Player_pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }
    }

    //슬롯2 아이템 오브젝트 위에 올려두기 버튼
    public void slot_button2_objects()
    {
        Slot2_Button_floor.SetActive(false);
        Slot2_Button_objects.SetActive(false);

        if(object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Object.Object_pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }
    }

    //슬롯2 UI버튼
    public void Slot2_UI_Button()
    {
        if(object_collision == "땅")
        {
            Slot2_Button_floor.SetActive(true);
            Slot2_Button_objects.SetActive(false);
        }

        if(object_collision == "사물")
        {
            Slot2_Button_floor.SetActive(true);
            Slot2_Button_objects.SetActive(true);
        }
    }

    //-------------------------------------------------------------------------------------

    //슬롯3 아이템 땅에 두기 버튼
    public void slot_button3_floor()
    {
        Slot3_Button_floor.SetActive(false);
        Slot3_Button_objects.SetActive(false);

        if(object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Player_pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }
    }

    //슬롯3 아이템 오브젝트 위에 두기 버튼
    public void slot_button3_objects()
    {
        Slot3_Button_floor.SetActive(false);
        Slot3_Button_objects.SetActive(false);

        if(object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Object.Object_pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }
    }

    //슬롯3 UI버튼
    public void Slot3_UI_Button()
    {
        if(object_collision == "땅")
        {
            Slot3_Button_floor.SetActive(true);
            Slot3_Button_objects.SetActive(false);
        }

        if(object_collision == "사물")
        {
            Slot3_Button_floor.SetActive(true);
            Slot3_Button_objects.SetActive(true);
        }
    }

    //------------------------------------------------------------------------------------------

    //슬롯4 아이템 땅에 두기 버튼
    public void slot_button4_floor()
    {
        Slot4_Button_floor.SetActive(false);
        Slot4_Button_objects.SetActive(false);

        if(object_collision == "땅")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Player_pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }
    }

    //슬롯4 아이템 오브젝트 위에 올려두기 버튼
    public void slot_button4_objects()
    {
        Slot4_Button_floor.SetActive(false);
        Slot4_Button_objects.SetActive(false);

        if(object_collision == "사물")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Object.Object_pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }
    }

    //슬롯4 UI버튼
    public void Slot4_UI_Button()
    {
        if(object_collision == "땅")
        {
            Slot4_Button_floor.SetActive(true);
            Slot4_Button_objects.SetActive(false);
        }

        if(object_collision == "사물")
        {
            Slot4_Button_floor.SetActive(true);
            Slot4_Button_objects.SetActive(true);
        }
    }

    //아이템 줍기-----------------------------------------------------------------------

    //오브젝트 아이템 이름(string)이 "item0" 이라면 "item0" 오브젝트(GameObject)를 반환.
    GameObject GetItemObject(string item_name)
    {
        if (item_name == "item1")
            return item1;
        else if (item_name == "item2")
            return item2;
        else if (item_name == "item3")
            return item3;
        else if (item_name == "item4")
            return item4;

        return null;
    }

    //오브젝트 아이템 이름(string)이 "item0" 이라면 "item0" 스프라이트를 반환.
    Sprite GetItemSprite(string item_name)
    {
        if (item_name == "item1")
            return item1Sprite;
        else if (item_name == "item2")
            return item2Sprite;
        else if (item_name == "item3")
            return item3Sprite;
        else if (item_name == "item4")
            return item4Sprite;

        return null;
    }

    //콜라이더 관리-------------------------------------------------------------------------

    //충돌 중일때 실행
    void OnTriggerStay2D(Collider2D other)
    {
        //플레이어와 아이템이 충돌 중에 Z키를 누르면 해당 아이템을 줍고 슬롯에 저장.
        if(other.gameObject.tag == "item1" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null) //i번째 슬롯에 아이템이 없는지 확인
                {
                    item_main_slot[i] = "item1"; //i번째 슬롯에 item1적용.
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]); //슬롯 이미지(스프라이트)를 해당 아이템 스프라이트로 변경.
                    Destroy(other.gameObject); //땅에 있던 아이템 오브젝트는 제거.
                    break;
                }
            }
        }

        if(other.gameObject.tag == "item2" && Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "item2";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if(other.gameObject.tag == "item3" && Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "item3";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if(other.gameObject.tag == "item4" && Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<item_main_slot.Length; i++)
            {
                if(item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "item4";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    UIManager.Next_value = 10;
                    break;
                }
            }
        }

        //오브젝트 상호작용---------------------------------------
        if(other.gameObject.tag == "사물")
        {
            object_collision = "사물";
        }

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

    public static string ObjectName;//상호작용 오브젝트 이름

    //충돌 했을때 실행
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "A") //케이크 이벤트
        {
            UIManager.Next_value = 6;
            Destroy(other.gameObject);
        }
    }

    //충돌이 끝났을때 실행
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "B")
        {
            UIManager.Camera_setactive = false; //카메라 UI 효과 끄기
        }
    }

    //Player이동----------------------------------------------------------------------------

    public static Vector3 Player_pos; //플레이어 현재 위치
    public SpriteRenderer rend; //플레이어 스프라이트
    public Animator Player_move; //플레이어 이동 애니메이션

    public static float Player_speed = 3.5f;

    void Player_Move()
    {
        Player_pos = transform.position;
        Player_move.speed = 0;

        //위쪽으로 이동----------
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Player_speed * Time.deltaTime);
            
            if(Player_speed == 3.5f)
            {
                Player_move.speed = 1;
                Player_move.Play("walking the vertical up"); //수직 이동 모션
            }

            else if(Player_speed == 7f)
            {
                Player_move.speed = 2;
                Player_move.Play("walking the vertical up");
            }
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            Player_move.Play("stop vertical"); //수직으로 멈춤
        }

        //아래쪽으로 이동----------
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Player_speed * Time.deltaTime);

            if(Player_speed == 3.5f)
            {
                Player_move.speed = 1;
                Player_move.Play("walking the horizontal"); //수평선 이동 모션
            }

            else if(Player_speed == 7f)
            {
                Player_move.speed = 2;
                Player_move.Play("walking the horizontal");
            }
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            Player_move.Play("stop horizontal"); //수평선으로(?) 멈춤
        }

        //왼쪽으로 이동----------
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Player_speed * Time.deltaTime);
            rend.flipX = false;
            
            if(Player_speed == 3.5f)
            {
                Player_move.speed = 1;
                Player_move.Play("walking the horizontal");
            }

            else if(Player_speed == 7f)
            {
                Player_move.speed = 2;
                Player_move.Play("walking the horizontal");
            }
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            Player_move.Play("stop horizontal");
        }

        //오른쪽으로 이동----------
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Player_speed * Time.deltaTime);
            rend.flipX = true;
            
            if(Player_speed == 3.5f)
            {
                Player_move.speed = 1;
                Player_move.Play("walking the horizontal");
            }

            else if(Player_speed == 7f)
            {
                Player_move.speed = 2;
                Player_move.Play("walking the horizontal");
            }
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            Player_move.Play("stop horizontal");
        }

        //달리기
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Player_speed = 7f;
        }

        //Shift키를 누르지 않을땐 기본속도
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Player_speed = 3.5f;
        }
    }

    //--------------------------------------------------------------------------------------------

    void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate; //모니터에 맞는 주사율을 현재 프레임으로 지정 currentResolution.refreshRate <- 구형 버전으로 경고 뜰 수 있다.
    }

    void Start()
    {
        Player_move.speed = 0;
    }

    void Update()
    {
        Player_Move();
    }
}