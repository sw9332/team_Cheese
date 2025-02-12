using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    // 싱글톤 인스턴스
    // public static Bullet Instance { get; private set; }

    public float speed;
    public float distance;
    public LayerMask isLayer;
    public int bulletNum = 20;

    private Vector2 direction;
    private PlayerControl playerControl;
    private TilemapCollider2D wall;
    private EnemyManager enemyList;

    // 싱글톤 설정

    // 총알이 사용 가능한지 확인
    public bool IsBulletAvailable()
    {
        return bulletNum > 0;
    }

    // 총알의 방향 설정
    void BulletDirectionSettings()
    {

        if (playerControl.Direction == "Up") direction = Vector2.up;
        else if (playerControl.Direction == "Down") direction = Vector2.down;
        else if (playerControl.Direction == "Left") direction = Vector2.left;
        else if (playerControl.Direction == "Right") direction = Vector2.right;
        Invoke("DestroyBullet", 1);
    }

    // 총알 이동
    void BulletMove()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // 총알 파괴
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // 충돌 처리
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy")
            || other.gameObject.layer == LayerMask.NameToLayer("attackable object")) // 적과 충돌
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Enemy 스크립트 참조
            if (enemy != null)
            {
                Debug.Log(enemy.name + " 명중!");
                enemyList.takeDamage(enemy.name); // 적에게 데미지 입힘
            }
            DestroyBullet(); // 충돌 후 총알 파괴
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("wall")) // 벽과 충돌
        {
            DestroyBullet(); // 충돌 후 총알 파괴
        }

    }

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        enemyList = FindObjectOfType<EnemyManager>();

        // playerControl이 필요하기 때문에 이후 실행
        BulletDirectionSettings();
    }

    void Update()
    {
        BulletMove();
    }
}
