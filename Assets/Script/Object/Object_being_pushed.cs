using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Being_Pushed : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool FreezeX = false;
    private bool FreezeY = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) return;
        if (collider.gameObject.CompareTag("Cart Position")) return;
        if (collider.gameObject.CompareTag("Rabbit Statue Position")) return;

        FreezeX = Mathf.Abs(collider.transform.position.x - transform.position.x) > Mathf.Abs(collider.transform.position.y - transform.position.y);
        FreezeY = !FreezeX;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) return;
        if (collider.gameObject.CompareTag("Cart Position")) return;
        if (collider.gameObject.CompareTag("Rabbit Statue Position")) return;

        FreezeX = false;
        FreezeY = false;
    }

    void Update()
    {
        if (PlayerControl.speed == 2 && PlayerControl.isPush)
        {
            if (PlayerControl.MoveX && !PlayerControl.MoveY && !FreezeX) // 가로로 밀었을 때
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY; // Y축 고정
                rb.freezeRotation = true;
            }

            else if (PlayerControl.MoveY && !PlayerControl.MoveX && !FreezeY) // 세로로 밀었을 때
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX; // X축 고정
                rb.freezeRotation = true;
            }

            else if (!FreezeX && !FreezeY) rb.constraints = RigidbodyConstraints2D.None;
            else rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        else rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}