using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;

public class PlayerAttack : MonoBehaviour
{
    private PlayerControl playerControl;
    private EnemyManager enemyManager;

    // 원거리 공격 관련
    public GameObject bullet;
    public Transform bulletPos;
    public float fireCooltime;
    private float fireCurtime;

    // 근접공격 및 enemy와 충돌
    [SerializeField] List<GameObject> hp = new List<GameObject>();
    private Collider2D[] meleeAttackableEnemies;
    private Vector2 meleeAttackBoxSize;
    private Vector2 nearEnemyBoxSize;
    
    // 근접 공격에서 enemy 정보를 받아오기 위해서 설정
    private Collider2D enemyCollider;

    void PlayerAttacks()
    {
        enemyCollider = meleeAttackableEnemy();

        // 근접 공격 처리
        if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider != null && playerControl.isMove)  // 근접 공격 범위 내에 적군이 감지되었다면
        {
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                meleeAttack();
                enemyManager.takeDamage(enemyCollider.tag);
            }
        }
        // 원거리 공격 처리
        else if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider == null && fireCurtime <= 0 && playerControl.isMove) // 쿨타임 확인
        {
            rangedAttack();
        }

        attackStop();

        // bullet에 있던 코드를 끌어옴 , 단발 사격
        if (fireCurtime > 0)
        {
            fireCurtime -= Time.deltaTime;  // 쿨타임 감소
        }
    }

    void meleeAttack()
    {
        if (playerControl.Direction == 1)
        {
            playerControl.animator.Play("PlayerMeleeAttackBack");
        }
        if (playerControl.Direction == 2)
        {
            playerControl.animator.Play("PlayerMeleeAttackFront");
        }
        if (playerControl.Direction == 3)
        {
            playerControl.animator.Play("PlayerMeleeAttackLeft");
        }
        if (playerControl.Direction == 4)
        {
            playerControl.animator.Play("PlayerMeleeAttackRight");
        }
    }

    void rangedAttack()
    {
        // 방향에 따른 애니메이션 설정
        if (playerControl.Direction == 1) // 뒤
        {
            playerControl.animator.Play("PlayerLongAttackBack");
        }
        else if (playerControl.Direction == 2) // 정면
        {
            playerControl.animator.Play("PlayerLongAttackFront");
        }
        else if (playerControl.Direction == 3) // 왼쪽
        {
            playerControl.animator.Play("PlayerLongAttackLeft");
        }
        else if (playerControl.Direction == 4) // 오른쪽
        {
            playerControl.animator.Play("PlayerLongAttackRight");
        }


        // 발사 쿨타임이 끝났을 때만 총알 발사
        Instantiate(bullet, bulletPos.position, transform.rotation);  // 총알 생성
        fireCurtime = fireCooltime; // 쿨타임 초기화
    }

    void attackStop()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) && playerControl.isMove)
        {
            if (playerControl.Direction == 1)
            {
                playerControl.animator.Play("PlayerUp_Stop");
            }
            else if (playerControl.Direction == 2)
            {
                playerControl.animator.Play("PlayerBack_Stop");
            }
            else if (playerControl.Direction == 3)
            {
                playerControl.animator.Play("PlayerLeft_Stop");
            }
            else if (playerControl.Direction == 4)
            {
                playerControl.animator.Play("PlayerRight_Stop");
            }
        }
    }


    // 근접 공격   -------------------------------------------------------------------------------------------

    public bool showRangeGizmo = false;
    /* Player의 enemy 탐지 Gizmo */
    private void OnDrawGizmosSelected()
    {
        if (showRangeGizmo)
        {
            Gizmos.color = new Color(1.0f, 0f, 0f, 0.8f);
            Gizmos.DrawCube(this.transform.position + playerControl.CenterOffset, new Vector2(meleeAttackBoxSize.x, meleeAttackBoxSize.y));
        }
    }

    /*  근접 공격 설명
     linq(데이터 쿼리 언어)를 이용해서 빠른 정렬
     Gizmo의 범위 안에 존재하는 모든 2D 콜라이더를 가져옴
     => : 람다
        Where : 조건을 만족하는 요소 필터링
        OrderBy: 오름차순 정렬
        oArray: 배열로 변환
     'enemy' 태그를 가진 PolygonCollider2D만 필터링

     */

    private Collider2D meleeAttackableEnemy()
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerControl.CenterOffset, meleeAttackBoxSize, 0f);

        meleeAttackableEnemies = enemyArray
        .Where(collider => collider.gameObject.layer == 6 /*6번 Layer가 enemy, LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
        .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
        .ToArray();


        if (meleeAttackableEnemies.Length > 0)
        {
            Debug.Log("Melee Attackable Enemy: " + meleeAttackableEnemies[0].name);
            return meleeAttackableEnemies[0];
        }
        else
            return null;
    }


    // Player HP ---------------------------------------------------------------------
    void getPlayerHP()
    {
        int numHp = GameObject.Find("playerHP").transform.childCount;
        for (int i = 0; i < numHp; i++)
        {
            GameObject hpObj = GameObject.Find("playerHP").transform.GetChild(i).gameObject;
            hp.Add(hpObj);
        }
    }

    /* HP 관련 Gizmo */
    public bool showHPGizmo = false;
    private void OnDrawGizmos()
    {
        if (showHPGizmo)
        {
            Gizmos.color = new Color(0f, 3f, 0f, 0.7f);
            Gizmos.DrawCube(this.transform.position + playerControl.CenterOffset, new Vector2(nearEnemyBoxSize.x, nearEnemyBoxSize.y));
        }
    }


    private Collider2D[] nearEnemies;
    public float elapsedTime = 0f;
    private float destroyTime = 1f;
    private bool isCollidingWithEnemy = false;

    public bool isChangingSprite = false; // playerControl.MoveControl에서 사용하기 위해 public - isMove

    /* CollideWithEnemy 함수 설명
   'enemy' 태그를 가진 polygonCollider2D만 필터링
    => : 람다
     Where : 조건을 만족하는 요소 필터링
     OrderBy: 오름차순 정렬
     oArray: 배열로 변환
  */

    public bool CollideWithEnemy()
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerControl.CenterOffset, nearEnemyBoxSize, 0f);

        nearEnemies = enemyArray
            .Where(collider => collider.gameObject.layer == 6 /*LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
            .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
            .ToArray();

        if (nearEnemies.Length > 0)
        {
            Debug.Log("Near Enemy: " + nearEnemies[0].name);
            return true;
        }
        else
            return false;
    }

     void Player_Collision()
    {
        if (hp != null)
        {
            if (CollideWithEnemy() == true)
            {
                isCollidingWithEnemy = true;
            }
            else
            {
                isCollidingWithEnemy = false;
                elapsedTime = 0f;
            }

            // 1초이상 적과 대면 시 hp--
            if (isCollidingWithEnemy == true  && isChangingSprite != true)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= destroyTime && hp.Count > 0)
                {
                    GameObject lastHp = hp[hp.Count - 1];
                    StartCoroutine(changeToDamaged());
                    hp.RemoveAt(hp.Count - 1);
                    Destroy(lastHp);
                    elapsedTime = 0f; // 다시 시간 초기화
                }
            }
        }
    }

    //void returnToNormalSprite()
    //{
    //}
    IEnumerator changeToDamaged()
    {
        isChangingSprite = true;
        playerControl.isMove = false;
        if (playerControl.Direction == 1)
        {
            playerControl.animator.Play("playerDamagedBack");
        }
        if (playerControl.Direction == 2)
        {
            playerControl.animator.Play("playerDamagedFront");
        }
        if (playerControl.Direction == 3)
        {
            playerControl.animator.Play("playerDamagedLeft");
        }
        if (playerControl.Direction == 4)
        {
            playerControl.animator.Play("playerDamagedRight");
        }
        yield return new WaitForSeconds(0.5f);
        isChangingSprite = false;
        playerControl.isMove = true;
        elapsedTime = 0f;
    }
    

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        enemyManager = FindObjectOfType<EnemyManager>();

        getPlayerHP();

        // 범위 판정 offset 값
        meleeAttackBoxSize = new Vector2(2.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);
        fireCooltime = 0.2f;
    }

    void Update()
    {
        PlayerAttacks();
        Player_Collision();
    }
}
