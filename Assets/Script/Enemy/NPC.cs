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

    public int wall_Crash_stack = 0;

    private bool meleeAttack = false;
    public bool attackDamage = false;
    private bool rushing = false;
    private bool wall = false;
    public bool die = false;

    private float RUSH_SPEED = 15f;
    private float MOVE_STEP = 5f;
    private float RANGED_ATTACK_DELAY = 0.1f;
    private float MELEE_ATTACK_DELAY = 1f;

    private Vector3 lastPlayerDirection;
    private Vector3 targetPosition = new Vector3(-49f, 24f, 0);

    public void Transformation(bool transformation) //NPC���� ������ ����
    {
        animator.Play(transformation ? "transformation" : "NPC");
    }

    public void UpdateDirection() //���� ������Ʈ (�÷��̾ �ִ� ����)
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
            direction = toPlayer.x > 0 ? "Right" : "Left";
        else
            direction = toPlayer.y > 0 ? "Up" : "Down";
    }

    public void AnimationDirection(string animation, float speed) //�ش� �������� �ִϸ��̼� ���
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

    IEnumerator Melee_Attack(int repeat) //���� ���� ����
    {
        for (int i = 0; i < repeat; i++)
        {
            if (die)
            {
                animator.Play("Die");
                yield break;
            }

            while (!meleeAttack)
            {
                UpdateDirection();
                AnimationDirection("Walking", 1f);

                if (die)
                {
                    animator.Play("Die");
                    yield break;
                }

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

    IEnumerator Rush(int repeat) //���� ����
    {
        for (int i = 0; i < repeat; i++)
        {
            if (die)
            {
                animator.Play("Die");
                yield break;
            }

            while (!wall)
            {
                rushing = true;
                attackDamage = true;
                animator.speed = 2;

                UpdateDirection();

                Vector3 toPlayer = player.transform.position - transform.position;

                if (wall_Crash_stack == 0 || wall_Crash_stack == 1) lastPlayerDirection = toPlayer.normalized;

                else
                {
                    if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
                    {
                        direction = toPlayer.x > 0 ? "Right" : "Left";
                        lastPlayerDirection = new Vector3(Mathf.Sign(toPlayer.x), 0, 0);
                        wall_Crash_stack = 0;
                    }

                    else
                    {
                        direction = toPlayer.y > 0 ? "Up" : "Down";
                        lastPlayerDirection = new Vector3(0, Mathf.Sign(toPlayer.y), 0);
                        wall_Crash_stack = 0;
                    }
                }

                while (rushing)
                {
                    AnimationDirection("Walking", 2f);
                    transform.position += lastPlayerDirection * RUSH_SPEED * Time.deltaTime;

                    if (die)
                    {
                        animator.Play("Die");
                        yield break;
                    }

                    yield return null;
                }
            }

            yield return new WaitForSeconds(3f);
            wall = false;
            attackDamage = false;
        }
    }

    public IEnumerator Ranged_Attack(int repeat) //���Ÿ� ���� ����
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            if (die)
            {
                animator.Play("Die");
                yield break;
            }

            float step = MOVE_STEP * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            Vector3 directionToTarget = (targetPosition - transform.position).normalized;

            if (Mathf.Abs(directionToTarget.x) > Mathf.Abs(directionToTarget.y))
            {
                direction = directionToTarget.x > 0 ? "Right" : "Left";
                AnimationDirection("Walking", 1f);

                if (die)
                {
                    animator.Play("Die");
                    yield break;
                }
            }

            else
            {
                direction = directionToTarget.y > 0 ? "Up" : "Down";
                AnimationDirection("Walking", 1f);

                if (die)
                {
                    animator.Play("Die");
                    yield break;
                }
            }

            yield return null;
        }

        for (int i = 0; i < repeat; i++)
        {
            if (die)
            {
                animator.Play("Die");
                yield break;
            }

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
        Hp.gameObject.SetActive(false);
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
            wall_Crash_stack++;
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