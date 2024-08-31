using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Vector2 direction;



    // Start is called before the first frame update
    void Start()
    {
        
        if (Player.playerDirection == 1)  
        {
            direction = Vector2.up;
        }
        else if (Player.playerDirection == 2)
        {
            direction = Vector2.down;
        }
        else if (Player.playerDirection == 3 ) 
        {
            direction = Vector2.left;
        }
        else if (Player.playerDirection == 4)
        {
            direction = Vector2.right;
        }

        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        // 총알을 설정된 방향으로 이동
        transform.Translate(direction * speed * Time.deltaTime);

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, distance, isLayer);
        if (ray.collider != null)
        {
            {
                Debug.Log(ray.collider.name + " 명중!");
            }
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
