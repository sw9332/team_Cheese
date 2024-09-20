using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Being_Pushed : MonoBehaviour
{
    public Rigidbody2D rb;

    private bool FreezeX = false;
    private bool FreezeY = false;

    void Update()
    {
        if (Player.moveSpeed == 5)
        {
            if (Player.MoveX && !Player.MoveY && !FreezeX) // 가로로 밀었을 때
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY; // Y축 고정
                rb.freezeRotation = true;
            }

            else if (Player.MoveY && !Player.MoveX && !FreezeY) // 세로로 밀었을 때
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX; // X축 고정
                rb.freezeRotation = true;
            }

            else if (!FreezeX && !FreezeY)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }

            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) return;

        FreezeX = Mathf.Abs(collider.transform.position.x - transform.position.x) > Mathf.Abs(collider.transform.position.y - transform.position.y);
        FreezeY = !FreezeX;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) return;

        FreezeX = false;
        FreezeY = false;
    }
}