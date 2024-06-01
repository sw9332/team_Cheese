using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "테이블")
        {
            UIManager.Camera_setactive = true;
            UIManager.Next_value = 19;
        }
    }
}
