using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public Transform playerTransform;


    [SerializeField] Vector2 playerCheckBox;
    private Collider2D player;
    private SpriteRenderer spriteRenderer;
    private string enemyName;
    private GameObject enemy;
    private float moveSpeed;
    private Rigidbody2D rb;

    [SerializeField] Animator animator;

    public void destroyEnemy()
    {
        Destroy(gameObject);
        // Item Drop
        LootBag[] lootBags = GetComponents<LootBag>();
        foreach (LootBag lootBag in lootBags)
        {
            if (lootBag != null)
            {
                lootBag.InstantiateLoot(transform.position);
            }
        }
    }

    public IEnumerator PlayDeathAnimationAndDestroy()
    {
        if ((enemy.name + "Die") != null)
        {
            animator.Play(enemy.name + "Die");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        // 애니메이션 재생 후 오브젝트 삭제
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
        else
            return;
    }

    public bool showRangeGizmo = false;
    private void OnDrawGizmosSelected()
    {
        if (showRangeGizmo)
        {
            Gizmos.color = new Color(1.0f, 0f, 0f, 0.8f);
            Gizmos.DrawCube(this.transform.position, new Vector2(playerCheckBox.x, playerCheckBox.y));
        }

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
        if (gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            if (isNearPlayer()) 
            {
                // direction vector
                Vector2 direction = (playerTransform.position - this.transform.position).normalized;

                // enemy is on player's left
                if (playerTransform.position.x > this.transform.position.x)
                {
                    spriteRenderer.flipX = false;
                    spriteRenderer.flipY = false;
                }
                else // enemy is on player's right
                {
                    spriteRenderer.flipX = false;
                    spriteRenderer.flipY = true;
                }
                // move position 
                rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime);

                // Enemy rotate
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;

                animator.Play(enemyName + "Walk");
                Debug.Log($"Bear is walking towards {player.name}");
            }
            else 
            {
                bearIdle();
            }
        }
        else
            return;
    }

    void Update()
    {
        if (hp == 0)
        {
            StartCoroutine(PlayDeathAnimationAndDestroy());
        }
        else
        {
            bearMove();
        }
    }


    void Start()
    {
        enemy = this.gameObject;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCheckBox.x = 8.0f;
        playerCheckBox.y = 3.0f;
        moveSpeed = 2.0f;
        enemyName = enemy.name;
        animator = GetComponent<Animator>();
    }
}
