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
        // player의 스프라이트를 받아오기 위해 Find 함수 사용 
        GameObject otherObject = GameObject.Find("Player");
        SpriteRenderer playerRenderer = otherObject.GetComponent<SpriteRenderer>();

        // 플레이어의 SpriteRenderer 중 flip X를 이용해서 총알 방향 설정
        if (playerRenderer.flipX == true)   // 플레이어가 오른쪽을 보는 경우
        {
            direction = Vector2.right;
        }
        else  // 플레이어가 왼쪽을 보는 경우
        {
            direction = Vector2.left;
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
            if (ray.collider.tag == "Enemy")
            {
                Debug.Log("명중!");
            }
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
