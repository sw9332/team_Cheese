using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBeingPushed : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        if (Player.moveSpeed == 5)
        {
            if (Player.MoveX == true && Player.MoveY == false) //가로로 밀었을 때
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.freezeRotation = true;
            }

            else if (Player.MoveY == true && Player.MoveX == false) //세로로 밀었을 때
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.freezeRotation = true;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        Vector2 collisionPoint = collision.contacts[0].point;
        Vector2 objectPosition = transform.position;
        Vector2 direction = collisionPoint - objectPosition;
        direction.Normalize();

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //X축 충돌
        {
            if (direction.x > 0)
            {
                Debug.Log("오른쪽에서 충돌");
            }

            else
            {
                Debug.Log("왼쪽에서 충돌");
            }
        }

        else
        {
            if (direction.y > 0) //Y축 충돌
            {
                Debug.Log("위쪽에서 충돌");
            }

            else
            {
                Debug.Log("아래쪽에서 충돌");
            }
        }
    }
}
