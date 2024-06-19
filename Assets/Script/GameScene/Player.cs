using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //튜토리얼 플레이어 스크립트

    //플레이어 인벤토리-------------------------------------------------------------------------------------------------------------

    //아이템 두기/올려두기 버튼

    //(슬롯을 누르면 나오는 두기, 올려두기 버튼)

    //아이템 올려두기 버튼
    public GameObject Slot1_Button_objects; //슬롯1 아이템 올려두기 버튼
    public GameObject Slot2_Button_objects;
    public GameObject Slot3_Button_objects;
    public GameObject Slot4_Button_objects;

    //아이템 두기 버튼
    public GameObject Slot1_Button_floor; //슬롯1 아이템 두기 버튼
    public GameObject Slot2_Button_floor;
    public GameObject Slot3_Button_floor;
    public GameObject Slot4_Button_floor;

    public string[] item_main_slot = new string[4];
    public Image[] item_main_slot_Image = new Image[4];

    public static string object_collision = "땅";

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

    //아이템 줍기---------------------------------------------------------------------------------------------------------------------

    //아이템 오브젝트
    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;

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

    //아이템 스프라이트
    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;

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
                    UIManager.Next_value = 13; //쓰러진 곰돌이를 주웠을 때 스토리값을 13으로 (카메라 발견)
                    break;
                }
            }
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

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "B")
        {
            UIManager.Camera_setactive = false; //카메라 UI 효과 끄기
        }
    }

    //Player이동----------------------------------------------------------------------------------------------------------------------

    public static Vector3 Player_pos;
    public SpriteRenderer rend; //플레이어 스프라이트 (바라보는 방향 설정)
    public Animator Player_move; //플레이어 이동 애니메이션

    public static float Velocity;
    public float moveSpeed = 2.8f;

    //walking the vertical up
    //stop vertical
    
    //walking the horizontal
    //stop horizontal

    void Player_Move()
    {
        Player_pos = transform.position; //업데이트 될 때 마다 위치 초기화
        Player_move.speed = 1;
        Velocity = 0;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Velocity = moveSpeed;
            transform.Translate(Vector3.up * Velocity * Time.deltaTime);

            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                Player_move.Play("walking the horizontal");
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                Player_move.Play("walking the horizontal");
            else
                Player_move.Play("walking the vertical up");
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Velocity = moveSpeed;
            transform.Translate(Vector3.down * Velocity * Time.deltaTime);
            Player_move.Play("walking the horizontal");
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Velocity = moveSpeed;
            transform.Translate(Vector3.left * Velocity * Time.deltaTime);
            Player_move.Play("walking the horizontal");
            rend.flipX = false;
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Velocity = moveSpeed;
            transform.Translate(Vector3.right * Velocity * Time.deltaTime);
            Player_move.Play("walking the horizontal");
            rend.flipX = true;
        }            

        if((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)))
            Player_move.Play("stop vertical");

        else if((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
            Player_move.Play("stop horizontal");
    }

    //--------------------------------------------------------------------------------------------

    void Start()
    {
        Player_move.Play("stop horizontal");
    }

    void Update()
    {
        Player_Move();
    }
}