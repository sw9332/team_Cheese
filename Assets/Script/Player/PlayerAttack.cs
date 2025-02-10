using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer playerSpriteRenderer;
    private PlayerControl playerControl;
    private EnemyManager enemyManager;
    private GameManager gameManager;
    private CutSceneManager cutSceneManager;
    public Bullet bullet;
    public Text bulletNumText;

    // 공격(공격 애니메이션)이 진행중인지 체크하는 변수
    public bool isAttacking = false;

    // 원거리 공격 관련
    public Transform bulletPos;
    public float fireCooltime;
    private float fireCurtime;


    // 근접공격 및 enemy와 충돌
    private Collider2D[] meleeAttackableEnemies;
    private Vector2 meleeAttackBoxSize;
    private Vector2 nearEnemyBoxSize;
    // [SerializeField] Animator hpAnimator;

    // 근접 공격에서 enemy 정보를 받아오기 위해서 설정
    private Collider2D enemyCollider;

    void PlayerAttacks()
    {
        enemyCollider = meleeAttackableEnemy();

        // 근접 공격 처리
        if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider != null && playerControl.isMove
            && !isAttacking && !cutSceneManager.isCutScene)  // 근접 공격 범위 내에 적군이 감지되었다면
        {
            meleeAttackMotion();
            enemyManager.takeDamage(enemyCollider.tag);
        }

        // 원거리 공격 처리
        else if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider == null && fireCurtime <= 0
            && playerControl.isMove && !isAttacking && bullet.IsBulletAvailable() == true && !cutSceneManager.isCutScene) // 쿨타임 확인
        {
            rangedAttackMotion();
        }

        // else if (근접, attackable object 관련 부분 코드 추가 예정)
        if (playerControl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            isAttacking = false;
            attackMotionStop();
        }

        // bullet에 있던 코드를 끌어옴 , 단발 사격
        if (fireCurtime > 0)
        {
            fireCurtime -= Time.deltaTime;  // 쿨타임 감소
        }
    }

    void meleeAttackMotion()
    {
        isAttacking = true;

        switch (playerControl.Direction)
        {
            case "Up": playerControl.animator.Play("PlayerMeleeAttackUp"); break;
            case "Down": playerControl.animator.Play("PlayerMeleeAttackDown"); break;
            case "Left": playerControl.animator.Play("PlayerMeleeAttackLeft"); break;
            case "Right": playerControl.animator.Play("PlayerMeleeAttackRight"); break;
        }
    }

    void rangedAttackMotion()
    {
        isAttacking = true;

        switch (playerControl.Direction)
        {
            case "Up": playerControl.animator.Play("PlayerLongAttackUp", 0, 0f); break;
            case "Down": playerControl.animator.Play("PlayerLongAttackDown", 0, 0f); break;
            case "Left": playerControl.animator.Play("PlayerLongAttackLeft", 0, 0f); break;
            case "Right": playerControl.animator.Play("PlayerLongAttackRight", 0, 0f); break;
        }

        // 발사 쿨타임이 끝났을 때만 총알 발사
        Instantiate(bullet, bulletPos.position, transform.rotation);  // 총알 생성
        bullet.bulletNum--;
        fireCurtime = fireCooltime; // 쿨타임 초기화
    }


    void attackMotionStop()
    {
        if (isAttacking)
        {
            switch (playerControl.Direction)
            {
                case "Up": playerControl.StopDirection(playerControl.Direction); break;
                case "Down": playerControl.StopDirection(playerControl.Direction); break;
                case "Left": playerControl.StopDirection(playerControl.Direction); break;
                case "Right": playerControl.StopDirection(playerControl.Direction); break;
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

<<<<<<< HEAD
        if (meleeAttackableEnemies.Length > 0)
        {
            Debug.Log("Melee Attackable Enemy: " + meleeAttackableEnemies[0].name);
            return meleeAttackableEnemies[0];
        }
        else
            return null;
    }


    // Player HP ---------------------------------------------------------------------

    private Collider2D[] nearEnemies;
    public float elapsedTime = 0f;
    private float destroyTime = 1f;
    private bool isCollidingWithEnemy = false;

    public bool isChangingSprite = false; // playerControl.MoveControl에서 사용하기 위해 public - isMove


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

            // 1초 이상 적과 대면 시 HP 감소
            if (isCollidingWithEnemy == true && isChangingSprite != true)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= destroyTime /*1f*/ && hp.Count > 0)
                {
                    GameObject lastHp = hp[hp.Count - 1];
                    // lastHp에서 Animator 가져오기
                    Animator hpAnimator = lastHp.GetComponent<Animator>();
                    // hpAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
                    if ((hp.Count) % 2 == 0)
                    {
                        StartCoroutine(playHPLoseAnimation(lastHp, hpAnimator, hp.Count));
                    }
                    else
                    {
                        StartCoroutine(playHPLoseAnimation(lastHp, hpAnimator, hp.Count));
                    }
                    hp.RemoveAt(hp.Count - 1);
                    StartCoroutine(changeToDamaged());

                    // 애니메이션이 끝난 후 오브젝트 삭제
                    elapsedTime = 0f; // 시간 초기화
                }
                else if (hp.Count < 1 && !playerControl.GameEnd)
                {
                    StartCoroutine(gameManager.GameOver());
                    GameManager.GameEnd = true;
                }
            }
        }
=======
        if (meleeAttackableEnemies.Length > 0) return meleeAttackableEnemies[0];
        else return null;
>>>>>>> 62490b494e8cb7dac4fe007f5d7baf09911fa9b4
    }

    // HP Lose 애니메이션 재생
    IEnumerator playHPLoseAnimation(GameObject obj, Animator animator, int hpCount)
    {
        if (animator != null)
        {
            if (hpCount % 2 == 0)
            {
                animator.Play("hprightlose");
                Debug.Log("현재 애니메이션 상태: " + animator.GetCurrentAnimatorStateInfo(0).IsName("hprightlose"));
            }

            else
            {
                animator.Play("hpleftlose");
                Debug.Log("현재 애니메이션 상태: " + animator.GetCurrentAnimatorStateInfo(0).IsName("hpleftlose"));
            }

            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
            Destroy(obj);
        }
    }

    void Update()
    {
        PlayerAttacks();

        bulletNumText.text = "" + bullet.bulletNum.ToString();
    }

    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        enemyManager = FindFirstObjectByType<EnemyManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();

        // Gizmo box size settings
        meleeAttackBoxSize = new Vector2(2.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);
        fireCooltime = 0.1f;
        bullet.bulletNum = 20;
    }
}