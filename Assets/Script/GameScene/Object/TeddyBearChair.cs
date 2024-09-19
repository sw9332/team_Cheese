using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TeddyBearChair : MonoBehaviour
{

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BrownTeddyBear") //케이크를 테이블에 놓았을때 생기는 이벤트 오브젝트
        {
            UIManager.is_bear = true;
        }
    }
}
