using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private PlayerControl player;

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
    private const float RANGED_ATTACK_DELAY = 0.1f;
    private const float MELEE_ATTACK_DELAY = 1f;
    private const float DAMAGE_DELAY = 3f;

    public void Transformation(bool transformation) //NPC에서 보스로 변경
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

    public void Walking() //플레이어를 따라가는 함수
    {
        animator.speed = 1f;
        UpdateDirection();

        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        transform.position += toPlayer * (PlayerControl.speed * 1.3f) * Time.deltaTime;

        switch (direction)
        {
            case "Up": animator.Play("Boss Walking Up"); break;
            case "Down": animator.Play("Boss Walking Down"); break;
            case "Left": animator.Play("Boss Walking Left"); break;
            case "Right": animator.Play("Boss Walking Right"); break;
        }
    }

    public void Melee_Attack() //근접 공격 함수
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

    public void Ranged_Attack() //원거리 공격 함수
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

    public void Rush() //플레이어 방향으로 돌진
    {
        if (!rushing)
        {
            rushing = true;
            Vector3 toPlayer = player.transform.position - transform.position;

            if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y)) direction = toPlayer.x > 0 ? "Right" : "Left";
            else direction = toPlayer.y > 0 ? "Up" : "Down";

            lastPlayerDirection = toPlayer.normalized;
            StartCoroutine(PerformRush());
        }
    }

    IEnumerator PerformRush()
    {
        while (rushing)
        {
            switch (direction)
            {
                case "Up": animator.Play("Boss Walking Up"); break;
                case "Down": animator.Play("Boss Walking Down"); break;
                case "Left": animator.Play("Boss Walking Left"); break;
                case "Right": animator.Play("Boss Walking Right"); break;
            }

            transform.position += lastPlayerDirection * RUSH_SPEED * Time.deltaTime;
            yield return null;
        }
    }

    void MoveTowardsTarget() //가운데 위치
    {
        float step = MOVE_STEP * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        if (Mathf.Abs(directionToTarget.x) > Mathf.Abs(directionToTarget.y))
        {
            if (directionToTarget.x > 0)
            {
                direction = "Right";
                animator.Play("Boss Walking Right");
            }

            else
            {
                direction = "Left";
                animator.Play("Boss Walking Left");
            }
        }

        else
        {
            if (directionToTarget.y > 0)
            {
                direction = "Up";
                animator.Play("Boss Walking Up");
            }

            else
            {
                direction = "Down";
                animator.Play("Boss Walking Down");
            }
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) isMoving = false;
    }

    public IEnumerator RangedAttack(string direction)
    {
        animator.Play(direction);
        yield return new WaitForSeconds(RANGED_ATTACK_DELAY);

        GameObject bullet = direction switch
        {
            "Up" => Bullet_Up,
            "Down" => Bullet_Down,
            "Left" => Bullet_Left,
            "Right" => Bullet_Right,
            _ => null
        };

        Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(RANGED_ATTACK_DELAY);
    }

    public IEnumerator Boss()
    {
        //1번 패턴
        for (int i = 0; i < 4; i++)
        {
            meleeAttack = true;
            yield return new WaitForSeconds(MELEE_ATTACK_DELAY);
            meleeAttack = false;

            walking = true;
            yield return new WaitForSeconds(MELEE_ATTACK_DELAY);
            walking = false;
        }

        //2번 패턴
        for (int i = 0; i < 3; i++)
        {
            isRush = true;
            while (isRush) yield return null;
            yield return new WaitForSeconds(DAMAGE_DELAY);
        }

        //3번 패턴
        isMoving = true;
        while (isMoving) yield return null;

        for (int i = 0; i < 6; i++)
        {
            UpdateDirection();
            rangedAttack = true;
            yield return new WaitForSeconds(0.8f);

            yield return StartCoroutine(RangedAttack(direction));

            rangedAttack = false;
            yield return new WaitForSeconds(0.8f);
        }

        //패턴 반복
        yield return StartCoroutine(Boss());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") && rushing)
        {
            rushing = false;
            isRush = false;

            switch (direction)
            {
                case "Left": animator.Play("Boss Damaged Left"); break;
                case "Right": animator.Play("Boss Damaged Right"); break;
                default: animator.Play("Boss Damaged Left"); break;
            }
        }
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
        animator.Play("NPC");
    }
}