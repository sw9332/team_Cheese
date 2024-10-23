using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Vector2 direction;
    private PlayerControl playerControl;
    private TilemapCollider2D wall;
    private EnemyManager enemyList;

    void bulletDirectionSettings()
    {
        if (playerControl.Direction == 1) direction = Vector2.up;
        else if (playerControl.Direction == 2) direction = Vector2.down;
        else if (playerControl.Direction == 3) direction = Vector2.left;
        else if (playerControl.Direction == 4) direction = Vector2.right;
        Invoke("DestroyBullet", 1);
    }
    void bulletMove()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy"))  // 적 레이어와 충돌했을 때
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Enemy 스크립트 참조
            if (enemy != null)
            {
                Debug.Log(enemy.tag + " 명중!");
                enemyList.takeDamage(enemy.tag); // 적에게 데미지를 입힘
            }
            DestroyBullet(); // 충돌 후 총알 파괴
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            DestroyBullet(); // 충돌 후 총알 파괴
        }
    }

    void Update()
    {
        bulletMove();
    }

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        enemyList = FindObjectOfType<EnemyManager>();
        bulletDirectionSettings();
    }
}
