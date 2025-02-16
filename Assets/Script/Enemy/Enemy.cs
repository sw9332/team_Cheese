using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public static bool layer = false;

    private PlayerControl playerControl;
    private Animator animator;
    private Collider2D player;
    private GameObject enemy;
    private Rigidbody2D rb;


    public Vector2 playerCheckBox;

    public int hp = 3;
    private float moveSpeed;
    private string enemyName;

    public bool attack = false;
    public bool isDamaging = false;
    public bool showRangeGizmo = false;

    public void destroyEnemy()
    {
        Destroy(gameObject);
        // Item Drop
        LootBag lootBag = GetComponent<LootBag>();
        lootBag.InstantiateLoot(transform.position);
    }

    public IEnumerator PlayDeathAnimationAndDestroy()
    {
        // For Box
        if (enemy.gameObject.layer == LayerMask.NameToLayer("attackable object")
         && enemy.tag == "Push_Object")
        {
            animator.Play(enemy.name + "Open");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        else if (enemy.gameObject.layer == LayerMask.NameToLayer("enemy")
            || enemy.gameObject.layer == LayerMask.NameToLayer("attackable object")) //(enemy.name + "Die") != null)
        { 
            animator.Play(enemy.name + "Die");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
 
        // �ִϸ��̼� ��� �� ������Ʈ ����
        destroyEnemy();
    }
    public void bearIdle()
    {
        if (gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            if (!animator.IsInTransition(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.Play(this.gameObject.name + "Idle");
            }
        }

        else return;
    }

    public bool isNearPlayer()
    {
        // check player
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, playerCheckBox, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                player = collider; 
                return true;
            }
        }

        return false;
    }

    public void bearMove()
    {
        if (!attack)
        {
            if (gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                if (isNearPlayer())
                {
                    // direction vector
                    Vector2 direction;

                    if (GameManager.GameState == "CutScene 10") direction = new Vector2(playerControl.transform.position.x - this.transform.position.x, 0).normalized;
                    else direction = (playerControl.transform.position - this.transform.position).normalized;

                    // enemy is on player's left
                    if (playerControl.transform.position.x > this.transform.position.x)
                    {
                        GetComponent<SpriteRenderer>().flipX = false;
                        GetComponent<SpriteRenderer>().flipY = false;
                    }
                    else // enemy is on player's right
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                        GetComponent<SpriteRenderer>().flipY = false;
                    }
                    // move position 
                    rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime);

                    animator.Play(enemyName + "Walk");
                }

                else bearIdle();
            }

            else return;
        }
    }

    IEnumerator Attack()
    {
        while (attack)
        {
            if (!isDamaging)
            {
                isDamaging = true;
                StartCoroutine(Damage.Instance.ChangeToDamaged(1.0f));
                animator.Play(this.gameObject.name + "Idle");
                yield return new WaitForSeconds(1.0f);
                isDamaging = false;
            }

            else yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showRangeGizmo)
        {
            Gizmos.color = new Color(1.0f, 0f, 0f, 0.8f);
            Gizmos.DrawCube(this.transform.position, new Vector2(playerCheckBox.x, playerCheckBox.y));
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && this.gameObject.layer== LayerMask.NameToLayer("enemy"))
        {
            attack = true;
            if (!isDamaging) StartCoroutine(Attack());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && this.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            attack = false;
            bearMove();
        }
    }

    void Update()
    {
        if (hp == 0) StartCoroutine(PlayDeathAnimationAndDestroy());
        else bearMove();

        if (layer)
        {
            playerCheckBox.x = 50.0f;
            playerCheckBox.y = 15.0f;
        }

        else
        {
            playerCheckBox.x = 8.0f;
            playerCheckBox.y = 3.0f;
        }
    }

    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        enemy = this.gameObject;
        enemyName = enemy.name;
        playerCheckBox.x = 8.0f;
        playerCheckBox.y = 3.0f;
        moveSpeed = 2.0f;
    }
}