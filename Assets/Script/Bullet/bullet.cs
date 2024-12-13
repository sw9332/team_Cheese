using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static Bullet Instance { get; private set; }

    public float speed;
    public float distance;
    public LayerMask isLayer;
    public int bulletNum = 20;

    private Vector2 direction;
    private PlayerControl playerControl;
    private TilemapCollider2D wall;
    private EnemyManager enemyList;

    // 싱글톤 설정
    private void Awake()
    {
        // 인스턴스가 이미 존재하는 경우, 새로 생성된 오브젝트를 삭제
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 싱글톤 인스턴스 설정
        Instance = this;

        // 오브젝트가 씬 변경 시에도 삭제되지 않도록 설정
        DontDestroyOnLoad(gameObject);

        // 초기화
        bulletNum = 20;

    }

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
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy")) // 적과 충돌
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Enemy 스크립트 참조
            if (enemy != null)
            {
                Debug.Log(enemy.tag + " 명중!");
                enemyList.takeDamage(enemy.tag); // 적에게 데미지 입힘
            }
            DestroyBullet(); // 충돌 후 총알 파괴
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("wall")) // 벽과 충돌
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
