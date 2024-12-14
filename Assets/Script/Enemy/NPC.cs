using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private PlayerControl player;
    private SpriteRenderer spriteRenderer;

    public Animator animator;

    public GameObject Bullet_Up;
    public GameObject Bullet_Down;
    public GameObject Bullet_Left;
    public GameObject Bullet_Right;

    public bool walking = false;
    public bool meleeAttack = false;
    public bool rangedAttack = false;
    public bool rushing = false;
    public bool isRush = false;

    public float speed = 0f;

    public string direction = "Down";
    private Vector3 lastPlayerDirection;

    private Vector3 targetPosition = new Vector3(-49f, 24f, 0);
    public bool isMoving = false;

    private const float RUSH_SPEED = 15f;
    private const float MOVE_STEP = 5f;
    private const float ANIMATION_DELAY = 0.1f;
    private const float MELEE_ATTACK_DELAY = 1f;

    public void Transformation(bool transformation)
    {
        animator.Play(transformation ? "transformation" : "NPC");
    }

    public void UpdateDirection()
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
            direction = toPlayer.x > 0 ? "Right" : "Left";
        else
            direction = toPlayer.y > 0 ? "Up" : "Down";
    }

    public void Walking()
    {
        animator.speed = 1f;
        UpdateDirection();

        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        transform.position += toPlayer * (PlayerControl.speed * 1.3f) * Time.deltaTime;

        animator.Play("Boss Walking");
    }

    public void Melee_Attack()
    {
        animator.speed = 1f;
        string animationName = direction switch
        {
            "Up" => "Boss Box Back",
            "Down" => "Boss Box Front",
            "Left" => "Boss Box Left",
            "Right" => "Boss Box Right",
            _ => ""
        };

        animator.Play(animationName);
    }

    public void Ranged_Attack()
    {
        animator.speed = 1f;
        string animationName = direction switch
        {
            "Up" => "Boss Box Back",
            "Down" => "Boss Box Front",
            "Left" => "Boss Box Left",
            "Right" => "Boss Box Right",
            _ => ""
        };

        animator.Play(animationName);
    }

    public void Rush()
    {
        if (!rushing)
        {
            rushing = true;
            direction = player.transform.position.x < transform.position.x ? "Left" : "Right";
            lastPlayerDirection = (player.transform.position - transform.position).normalized;
            StartCoroutine(PerformRush());
        }
    }

    IEnumerator PerformRush()
    {
        animator.Play("Boss Walking");

        while (rushing)
        {
            switch (direction)
            {
                case "Left": spriteRenderer.flipX = true; break;
                case "Right": spriteRenderer.flipX = false; break;
            }

            transform.position += lastPlayerDirection * RUSH_SPEED * Time.deltaTime;
            yield return null;
        }
    }

    public void Center()
    {
        isMoving = true;
        spriteRenderer.flipX = false;
    }

    void MoveTowardsTarget()
    {
        float step = MOVE_STEP * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        animator.Play("Boss Walking");

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) isMoving = false;
    }

    public IEnumerator RangedAttack(string direction)
    {
        animator.Play(direction);
        yield return new WaitForSeconds(ANIMATION_DELAY);

        GameObject bullet = direction switch
        {
            "Up" => Bullet_Up,
            "Down" => Bullet_Down,
            "Left" => Bullet_Left,
            "Right" => Bullet_Right,
            _ => null
        };

        Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(ANIMATION_DELAY);
    }

    public IEnumerator RangedAttackSequence()
    {
        Center();

        while (isMoving) yield return null;

        int number = 0;
        while (number < 5)
        {
            UpdateDirection();
            rangedAttack = true;
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(RangedAttack(direction));

            rangedAttack = false;
            yield return new WaitForSeconds(1f);
            number++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") && rushing)
        {
            rushing = false;
            isRush = false;

            animator.Play("Boss Damaged");
            spriteRenderer.flipX = direction == "Right";
        }
    }

    public IEnumerator Boss()
    {
        for (int i = 0; i < 4; i++)
        {
            meleeAttack = true;
            yield return new WaitForSeconds(MELEE_ATTACK_DELAY);
            meleeAttack = false;

            walking = true;
            yield return new WaitForSeconds(MELEE_ATTACK_DELAY);
            walking = false;
        }

        for (int i = 0; i < 3; i++)
        {
            isRush = true;
            while (isRush) yield return null;
            yield return new WaitForSeconds(3f);
        }

        yield return StartCoroutine(RangedAttackSequence());
        yield return StartCoroutine(Boss());
    }

    void Update()
    {
        if (walking) Walking();
        if (meleeAttack) Melee_Attack();
        if (rangedAttack) Ranged_Attack();
        if (isRush) Rush();
        if (isMoving) MoveTowardsTarget();
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.Play("NPC");
    }
}