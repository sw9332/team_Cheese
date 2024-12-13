using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private PlayerControl player;
    private SpriteRenderer spriteRenderer;

    public Animator animator;

    public bool walking = false;
    public bool attack = false;
    public bool rushing = false;
    public bool isRush = false;

    public float speed = 0f;

    public string direction = "Down";
    private Vector3 lastPlayerDirection;

    public void Transformation(bool transformation)
    {
        switch (transformation)
        {
            case true: animator.Play("transformation"); break;
            case false: animator.Play("NPC"); break;
        }
    }

    public void UpdateDirection()
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
        {
            if (toPlayer.x > 0) direction = "Right";
            else direction = "Left";
        }
        else
        {
            if (toPlayer.y > 0) direction = "Up";
            else direction = "Down";
        }
    }

    public void Walking()
    {
        animator.speed = 1f;

        if (walking)
        {
            UpdateDirection();

            Vector3 toPlayer = (player.transform.position - transform.position).normalized;
            transform.position += toPlayer * (PlayerControl.speed * 1.3f) * Time.deltaTime;

            animator.Play("Boss Walking");
        }
    }

    public void Attack()
    {
        animator.speed = 1f;

        if (attack)
        {
            switch (direction)
            {
                case "Left": animator.Play("Boss Box Left"); break;
                case "Right": animator.Play("Boss Box Right"); break;
                case "Up": animator.Play("Boss Box Back"); break;
                case "Down": animator.Play("Boss Box Front"); break;
            }
        }
    }

    public void Rush()
    {
        if (!rushing)
        {
            rushing = true;

            if (player.transform.position.x < transform.position.x)
            {
                direction = "Left";
            }

            else
            {
                direction = "Right";
            }

            lastPlayerDirection = (player.transform.position - transform.position).normalized;
            StartCoroutine(PerformRush());
        }
    }

    IEnumerator PerformRush()
    {
        animator.Play("Boss Walking");

        while (rushing)
        {
            transform.position += lastPlayerDirection * 15f * Time.deltaTime;
            yield return null;
        }
    }

    private Vector3 targetPosition = new Vector3(-49f, 24f, 0);
    public bool isMoving = false;


    public void Ranged_Attack()
    {
        isMoving = true;
    }

    private void MoveTowardsTarget()
    {
        float step = 5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        animator.Play("Boss Walking");

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    public IEnumerator RangedAttack()
    {
        Ranged_Attack();

        while (isMoving) yield return null;

        while (true)
        {
            UpdateDirection();
            attack = true;
            yield return new WaitForSeconds(1f);
            attack = false;

            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") && rushing)
        {
            rushing = false;
            isRush = false;

            switch (direction)
            {
                case "Left": animator.Play("Boss Damaged"); spriteRenderer.flipX = false; break;
                case "Right": animator.Play("Boss Damaged"); spriteRenderer.flipX = true; break;
                default: animator.Play("Boss Damaged"); break;
            }
        }
    }

    void Update()
    {
        Walking();
        Attack();

        if (isRush)
        {
            Rush();
        }

        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.Play("NPC");
    }
}