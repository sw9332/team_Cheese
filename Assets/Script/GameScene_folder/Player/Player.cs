using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting; // ?占쏙옙?占쏙옙?占쏙옙 荑쇰━ ?占쏙옙?占쏙옙 

public class player : MonoBehaviour
{
    //?占쏙옙踰ㅽ넗占�? 占�? ?占쏙옙?占쏙옙?占쏙옙?占쏙옙 泥섎━ ------------------------------------------------------------------------------------------------------------------------

    //?占쏙옙占�? 諛곗뿴
    public string[] item_main_slot = new string[4];
    public Image[] item_main_slot_Image = new Image[4];

    //?占쏙옙?占쏙옙?占쏙옙?占쏙옙占�? ?占쏙옙釉뚯젥?占쏙옙?占쏙옙 異⑸룎 以묒씤占�? 泥댄겕 (?占쏙옙/?占쏙옙占�?)
    public static string object_collision = "?占쏙옙";

    //?占쏙옙?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙釉뚯젥?占쏙옙 ?占쏙옙占�?
    public static string ObjectName;

    //?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙釉뚯젥?占쏙옙
    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;
    public GameObject Camera;

    //?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙?占쏙옙?占쏙옙?占쏙옙
    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;


    //?占쏙옙踰ㅽ넗占�?---------------------------------------------------------------------------------------------------------


    private DialogueManager dialogueManager;

    //????占쏙옙?占쏙옙?占쏙옙
    [SerializeField]
    public Dialogue d_cake;

    [SerializeField]
    public Dialogue d_camera;

    [SerializeField]
    public Dialogue d_photo;

    public GameObject CameraUI;

    //?占쏙옙占�?1 ?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 ?占쏙옙占�? 踰꾪듉
    public void Slot1()

    /* ?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙占�?/?占쏙옙?占쏙옙?占쏙옙占�? */

    {
        if(object_collision == "?占쏙옙")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Player_pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }

        if(object_collision == "?占쏙옙占�?")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Object.Object_pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }
    }

    public void Slot2()
    {
        if (object_collision == "?占쏙옙")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Player_pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }

        if (object_collision == "?占쏙옙占�?")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Object.Object_pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }
    }

    public void Slot3()
    {
        if (object_collision == "?占쏙옙")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Player_pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }

        if (object_collision == "?占쏙옙占�?")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Object.Object_pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }
    }

    public void Slot4()
    {
        if (object_collision == "?占쏙옙")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Player_pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }

        if (object_collision == "?占쏙옙占�?")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Object.Object_pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }
    }

    /* ?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙占�?/?占쏙옙?占쏙옙?占쏙옙占�? ?占쏙옙 */


    /* ?占쏙옙?占쏙옙?占쏙옙 以띻린 */

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

                    dialogueManager.ShowDialogue(d_camera); //?占쏙옙?占쏙옙占�? 怨곕룎?占쏙옙占�? 二쇱썱?占쏙옙 ?占쏙옙 ?占쏙옙?占쏙옙由ш컪?占쏙옙 13?占쏙옙占�? (移대찓?占쏙옙 諛쒓껄)
                    CameraUI.SetActive(true);

                    break;
                }
            }
        }

        if(other.gameObject.tag == "Camera" && Input.GetKeyDown(KeyCode.Space))
        {
           // UIManager.Next_value = 13; //移대찓?占쏙옙占�? 二쇱썱?占쏙옙 ?占쏙옙 ?占쏙옙?占쏙옙由ш컪?占쏙옙 13?占쏙옙占�? (移대찓?占쏙옙 諛쒓껄)
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

    /* ?占쏙옙?占쏙옙?占쏙옙 以띻린 ?占쏙옙 */



    /* ?占쏙옙?占쏙옙?占쏙옙?占쏙옙 泥섎━ */

            //?占쏙옙臾쇱뿉 ?占쏙옙?占쏙옙?占쏙옙?占쏙옙 "?占쏙옙占�?"占�? 泥섎━
        if (other.gameObject.tag == "?占쏙옙占�?")
        {
            object_collision = "?占쏙옙占�?";
        }

        //?占쏙옙釉뚯젥?占쏙옙 ?占쏙옙?占쏙옙?占쏙옙?占쏙옙
        if(other.gameObject.tag == "?占쏙옙?占쏙옙")
        {
            if(Input.GetKeyDown(KeyCode.Space))
                ObjectName = "?占쏙옙?占쏙옙";
        }

        if(other.gameObject.tag == "?占쏙옙?占쏙옙")
        {
            if(Input.GetKeyDown(KeyCode.Space))
                ObjectName = "?占쏙옙?占쏙옙";
        }

        if(other.gameObject.tag == "?占쏙옙占�??占쏙옙 占�?")
        {
            if(Input.GetKeyDown(KeyCode.Space))
                ObjectName = "?占쏙옙占�??占쏙옙 占�?";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Cake Event") //占�??占쏙옙?占쏙옙 ?占쏙옙踰ㅽ듃
        {
            dialogueManager.ShowDialogue(d_cake);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Camera Event") //占�??占쏙옙?占쏙옙占�? ?占쏙옙?占쏙옙釉붿뿉 ?占쏙옙?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙湲곕뒗 ?占쏙옙踰ㅽ듃?占쏙옙 ?占쏙옙?占쏙옙?占쏙옙?占쏙옙
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



    //?占쏙옙踰ㅽ넗占�? 占�? ?占쏙옙?占쏙옙?占쏙옙?占쏙옙 泥섎━ ?占쏙옙 ---------------------------------------------------------------------------------------------------------------



    //Player ?占쏙옙?占쏙옙 占�? 而⑦듃占�?. ?占쏙옙嫄곕━ 怨듦꺽 ---------------------------------------------------------------------------------------------------------

    //Player ?占쏙옙?占쏙옙 占�? 而⑦듃占�?
    public static Vector3 Player_pos;
    public SpriteRenderer rend; //?占쏙옙?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙?占쏙옙?占쏙옙?占쏙옙 (諛붾씪蹂대뒗 諛⑺뼢 ?占쏙옙?占쏙옙)
    public Animator Player_control; //?占쏙옙?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙硫붿씠?占쏙옙
    public static float Velocity;
    public float moveSpeed = 2.5f;
    public static bool MoveX = false;
    public static bool MoveY = false;
    public Slider playerStamina;


    [SerializeField] Vector3 playerCenterOffset; // player?占쏙옙 踰붿쐞 ?占쏙옙蹂꾩쓣 ?占쏙옙?占쏙옙 offset

    //?占쏙옙嫄곕━ 怨듦꺽 占�??占쏙옙
    public GameObject bullet;
    public Transform bulletPos;
    private float fireCooltime = 0.3f;
    private float fireCurtime;


    // 洹쇱젒 怨듦꺽 占�? enemy??? 異⑸룎
    private Collider2D[] meleeAttackableEnemies; // 洹쇱젒 怨듦꺽 占�??占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 Collider 2D 諛곗뿴
    [SerializeField] Vector2 meleeAttackBoxSize; // Player?占쏙옙 洹쇱젒 怨듦꺽 踰붿쐞 GizmoBox?占쏙옙 ?占쏙옙占�?
    [SerializeField] Vector2 nearEnemyBoxSize; // Player??? Enemy?占쏙옙 Collision 泥댄겕占�? ?占쏙옙?占쏙옙 offset

    // 異뷀썑?占쏙옙 怨듦꺽 ?占쏙옙?占쏙옙硫붿씠?占쏙옙 異뷂옙??
    // public Animator Player_Attack;


    /* Player ?占쏙옙?占쏙옙 占�? 而⑦듃占�? 占�??占쏙옙 */

    void PlayerControl() //?占쏙옙?占쏙옙?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 占�? ?占쏙옙踰ㅽ넗占�? 而⑦듃占�?

    {
        Player_pos = transform.position; //?占쏙옙?占쏙옙?占쏙옙?占쏙옙 ?占쏙옙 ?占쏙옙 留덈떎 ?占쏙옙占�? 珥덇린?占쏙옙
        Player_control.speed = 1;
        Velocity = 0;

        //?占쏙옙占�? ?占쏙옙?占쏙옙
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //????占쏙옙李쎌씠 耳쒖졇?占쏙옙?占쏙옙?占쏙옙 ???吏곸씠占�? ?占쏙옙占�?
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //?占쏙옙占�? 寃뱀낀?占쏙옙?占쏙옙
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_control.Play("PlayerLeft");
            else if (Input.GetKey(KeyCode.RightArrow))
                Player_control.Play("PlayerRight");
            else
                Player_control.Play("PlayerUp");

            if (Input.GetKey(KeyCode.DownArrow))
                Player_control.Play("PlayerUp");

            MoveX = true;
            MoveY = false;

            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        //?占쏙옙?占쏙옙占�? ?占쏙옙?占쏙옙
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //????占쏙옙李쎌씠 耳쒖졇?占쏙옙?占쏙옙?占쏙옙 ???吏곸씠占�? ?占쏙옙占�?
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //?占쏙옙占�? 占�?爾ㅼ쓣?占쏙옙
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_control.Play("PlayerLeft");
            else if (Input.GetKey(KeyCode.RightArrow))
                Player_control.Play("PlayerRight");
            else
                Player_control.Play("PlayerBack");

            if (Input.GetKey(KeyCode.UpArrow))
                Player_control.Play("PlayerUp");

            MoveX = true;
            MoveY = false;

            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        //?占쏙옙履쎌쑝占�? ?占쏙옙?占쏙옙
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //????占쏙옙李쎌씠 耳쒖졇?占쏙옙?占쏙옙?占쏙옙 ???吏곸씠占�? ?占쏙옙占�?
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //?占쏙옙占�? 寃뱀낀?占쏙옙?占쏙옙
            if (Input.GetKey(KeyCode.RightArrow))
                Player_control.Play("PlayerLeft");
            else
                Player_control.Play("PlayerLeft");

            MoveX = false;
            MoveY = true;

            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //?占쏙옙瑜몄そ?占쏙옙占�? ?占쏙옙?占쏙옙
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //????占쏙옙李쎌씠 耳쒖졇?占쏙옙?占쏙옙?占쏙옙 ???吏곸씠占�? ?占쏙옙占�?
            //if (UIManager.StoryUI == true)
            //    Velocity = 0;
            //else
            //    Velocity = moveSpeed;

            //?占쏙옙占�? 寃뱀낀?占쏙옙?占쏙옙
            if (Input.GetKey(KeyCode.LeftArrow))
                Player_control.Play("PlayerLeft");
            else
                Player_control.Play("PlayerRight");

            MoveX = false;
            MoveY = true;

            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }


        // 諛⑺뼢?占쏙옙占�? ?占쏙옙占�?
        if (Input.GetKeyUp(KeyCode.UpArrow))
            Player_control.Play("PlayerUp_Stop");
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            Player_control.Play("PlayerBack_Stop");
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            Player_control.Play("PlayerLeft_Stop");
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            Player_control.Play("PlayerRight_Stop");
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Player_control.Play("PlayerStopX");
            // 諛⑺뼢?占쏙옙占�? ?占쏙옙 寃쎌슦 ?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙?占쏙옙 offset?占쏙옙占�? 占�?占�?
            playerCenterOffset.x = -0.25f;
        }

        //?占쏙옙由ш린
        if (Input.GetKey(KeyCode.LeftShift))
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

        /* Player ?占쏙옙踰ㅽ넗占�? */

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
            if (Input.GetKeyDown(KeyCode.LeftControl)) // 洹쇱쿂?占쏙옙 ?占쏙옙援곗씠 媛먲옙???占쏙옙?占쏙옙占�?
            {
                meleeAttack();
            }
        }
         
            if (Input.GetKeyDown(KeyCode.LeftControl) && meleeAttackableEnemy() == false)
            // 媛먲옙???占쏙옙 ?占쏙옙援곗씠 ?占쏙옙?占쏙옙占�? -> ?占쏙옙嫄곕━ 怨듦꺽
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


    // 洹쇱젒 怨듦꺽 -------------------------------------------------------------------------------------------

    
    /* Player?占쏙옙 enemy ?占쏙옙占�? Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(meleeAttackBoxSize.x, meleeAttackBoxSize.y));
    }
    */

    /*  洹쇱젒 怨듦꺽 ?占쏙옙占�?
     *  
      linq(?占쏙옙?占쏙옙?占쏙옙 荑쇰━ ?占쏙옙?占쏙옙)占�? ?占쏙옙?占쏙옙?占쏙옙?占쏙옙 鍮좊Ⅸ ?占쏙옙?占쏙옙
      Gizmo?占쏙옙 踰붿쐞 ?占쏙옙?占쏙옙 議댁옱?占쏙옙?占쏙옙 紐⑤뱺 2D 肄쒕씪?占쏙옙?占쏙옙占�? 占�??占쏙옙?占쏙옙
      => ?占쏙옙?占쏙옙
      Where: 議곌굔?占쏙옙 留뚯”?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙占�?  ,  OrderBy: ?占쏙옙由꾩감?占쏙옙 ?占쏙옙?占쏙옙 ,   ToArray: 諛곗뿴占�? 占�??占쏙옙
      'enemy' ?占쏙옙洹몌옙?? 占�?占�? PolygonCollider2D占�? ?占쏙옙?占쏙옙占�?

     */

    private bool meleeAttackableEnemy() 
    {
        // Gizmo?占쏙옙 踰붿쐞 ?占쏙옙?占쏙옙 議댁옱?占쏙옙?占쏙옙 紐⑤뱺 2D 肄쒕씪?占쏙옙?占쏙옙占�? 占�??占쏙옙?占쏙옙,
        // Where: 議곌굔?占쏙옙 留뚯”?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙占�?, OrderBy: ?占쏙옙由꾩감?占쏙옙 ?占쏙옙?占쏙옙, ToArray: 諛곗뿴占�? 占�??占쏙옙
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerCenterOffset, meleeAttackBoxSize, 0f);

        // 'enemy' ?占쏙옙洹몌옙?? 占�?占�? PolygonCollider2D占�? ?占쏙옙?占쏙옙占�?
        // => ?占쏙옙?占쏙옙
            meleeAttackableEnemies = enemyArray
            .Where(collider => collider.gameObject.layer == 6 /*LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
            .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
            .ToArray();

        // ?占쏙옙?占쏙옙 李억옙?? 寃쎌슦?占쏙옙占�? 占�??占쏙옙 占�?源뚯슫 enemy 異쒕젰
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

    /* HP 占�??占쏙옙 Gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f,3f,0f,0.5f);
        Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(nearEnemyBoxSize.x, nearEnemyBoxSize.y));
    }
    */

    /* CollideWithEnemy ?占쏙옙?占쏙옙 ?占쏙옙占�?
      'enemy' ?占쏙옙洹몌옙?? 占�?占�? PolygonCollider2D占�? ?占쏙옙?占쏙옙占�?
       => : ?占쏙옙?占쏙옙

       Where: 議곌굔?占쏙옙 留뚯”?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙 ?占쏙옙?占쏙옙占�?
       OrderBy: ?占쏙옙由꾩감?占쏙옙 ?占쏙옙?占쏙옙
       ToArray: 諛곗뿴占�? 占�??占쏙옙
       ?占쏙옙?占쏙옙 李억옙?? 寃쎌슦?占쏙옙占�? 占�??占쏙옙 占�?源뚯슫 enemy 異쒕젰
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

    // Hp UI ?占쏙옙占�? 
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
                    elapsedTime = 0f; // ?占쏙옙?占쏙옙 ?占쏙옙占�? 珥덇린?占쏙옙
                }
            }
        }
    }

    // -----------------------------------------------------------------------------------------

    void Start()
    {
        // 踰붿쐞 ?占쏙옙?占쏙옙 offset 占�?
        playerCenterOffset = new Vector3(0f, -0.4f, 0f);
        meleeAttackBoxSize = new Vector2(1.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);

        Player_control.Play("PlayerBack_Stop");
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        PlayerAttack();
        PlayerControl();
    }
}