using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    private PlayerControl player;
    private GameManager gameManager;
    private CutSceneManager cutSceneManager;

    public Animator animator;

    public Slider Hp;

    public GameObject Bullet_Up;
    public GameObject Bullet_Down;
    public GameObject Bullet_Left;
    public GameObject Bullet_Right;

    public string direction = "Down";

    public float speed = 5f;

    private bool meleeAttack = false;
    public bool attackDamage = false;
    private bool rushing = false;
    private bool wall = false;
    public bool die = false;

    private float RUSH_SPEED = 15f;
    private float MOVE_STEP = 5f;
    private float RANGED_ATTACK_DELAY = 0.1f;
    private float MELEE_ATTACK_DELAY = 1f;
    private float DAMAGE_DELAY = 3f;

    private Vector3 lastPlayerDirection;
    private Vector3 targetPosition = new Vector3(-49f, 24f, 0);

    public void Transformation(bool transformation) //NPC에서 보스로 변경
    {
        animator.Play(transformation ? "transformation" : "NPC");
    }

    public void UpdateDirection() //방향 업데이트 (플레이어가 있는 방향)
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
            direction = toPlayer.x > 0 ? "Right" : "Left";
        else
            direction = toPlayer.y > 0 ? "Up" : "Down";
    }

    public void AnimationDirection(string animation, float speed) //해당 방향으로 애니메이션 출력
    {
        string animationName = direction switch
        {
            "Up" => "Boss " + animation + " Up",
            "Down" => "Boss " + animation + " Down",
            "Left" => "Boss " + animation + " Left",
            "Right" => "Boss " + animation + " Right",
            _ => ""
        };

        animator.speed = speed;
        animator.Play(animationName);
    }

    IEnumerator Melee_Attack(int repeat) //근접 공격 패턴
    {
        for (int i = 0; i < repeat; i++)
        {
            if (die) yield break;

            while (!meleeAttack)
            {
                UpdateDirection();
                AnimationDirection("Walking", 1f);

                Vector3 toPlayer = (player.transform.position - transform.position).normalized;
                transform.position += toPlayer * speed * Time.deltaTime;

                yield return null;
            }

            AnimationDirection("Melee Attack", 1f);
            attackDamage = true;
            yield return new WaitForSeconds(MELEE_ATTACK_DELAY);
            meleeAttack = false;
            attackDamage = false;
        }
    }

    IEnumerator Rush(int repeat) //돌진 패턴
    {
        for (int i = 0; i < repeat; i++)
        {
            if (die) yield break;

            while (!wall)
            {
                rushing = true;
                attackDamage = true;
                animator.speed = 2;

                Vector3 toPlayer = player.transform.position - transform.position;
                lastPlayerDirection = toPlayer.normalized;

                if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
                    direction = toPlayer.x > 0 ? "Right" : "Left";
                else
                    direction = toPlayer.y > 0 ? "Up" : "Down";

                while (rushing)
                {
                    AnimationDirection("Walking", 2f);
                    transform.position += lastPlayerDirection * RUSH_SPEED * Time.deltaTime;
                    yield return null;
                }
            }

            yield return new WaitForSeconds(3f);
            wall = false;
            attackDamage = false;
        }
    }

    public IEnumerator Ranged_Attack(int repeat) //원거리 공격 패턴
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            if (die) yield break;

            float step = MOVE_STEP * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            Vector3 directionToTarget = (targetPosition - transform.position).normalized;

            if (Mathf.Abs(directionToTarget.x) > Mathf.Abs(directionToTarget.y))
            {
                direction = directionToTarget.x > 0 ? "Right" : "Left";
                AnimationDirection("Walking", 1f);
            }

            else
            {
                direction = directionToTarget.y > 0 ? "Up" : "Down";
                AnimationDirection("Walking", 1f);
            }

            yield return null;
        }

        for (int i = 0; i < repeat; i++)
        {
            if (die) yield break;

            UpdateDirection();
            AnimationDirection("Ranged Attack", 1f);

            yield return new WaitForSeconds(RANGED_ATTACK_DELAY);

            GameObject bullet = direction switch
            {
                "Up" => Bullet_Up,
                "Down" => Bullet_Down,
                "Left" => Bullet_Left,
                "Right" => Bullet_Right,
                _ => null
            };

            if (bullet)
            {
                attackDamage = true;
                Instantiate(bullet, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
                attackDamage = false;
            }

            yield return new WaitForSeconds(RANGED_ATTACK_DELAY);
        }
    }

    public IEnumerator Damage()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    public IEnumerator Die()
    {
        animator.Play("Die");
        speed = 0;
        RUSH_SPEED = 0;
        die = true;
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(cutSceneManager.CutScene_6());
    }

    public IEnumerator Boss_Pattern()
    {
        while (true && !player.GameEnd && !die)
        {
            yield return StartCoroutine(Melee_Attack(5));
            yield return StartCoroutine(Rush(3));
            yield return StartCoroutine(Ranged_Attack(5));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") && rushing)
        {
            wall = true;
            rushing = false;
            AnimationDirection("Damaged", 1f);
        }

        if (other.CompareTag("Bullet") && !player.GameEnd && !die)
        {
            if (Hp.value > 0)
            {
                Hp.value -= 10f;
                StartCoroutine(Damage());
            }
            
            else if (Hp.value <= 1)
            {
                StartCoroutine(Die());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) meleeAttack = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) meleeAttack = false;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        gameManager = FindFirstObjectByType<GameManager>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();

        animator.Play("NPC");
    }
}