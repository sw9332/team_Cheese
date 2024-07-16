using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public static Vector3 Object_pos;
    public GameObject CamaraEvent;

    //void OnCollisionEnter2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.object_collision = "사물";
            Object_pos = transform.position;
        }

        if(other.gameObject.tag == "Cake") //케이크를 테이블에 놓았을때 생기는 이벤트 오브젝트
        {
            CamaraEvent.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.object_collision = "땅";
        }
    }

}
