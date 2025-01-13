using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    private GameObject enemy;
    [SerializeField] Animator enemyEffects;  // Animator 컴포넌트

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
            enemyEffects.Play(enemy.name + "Die");
            yield return new WaitForSeconds(enemyEffects.GetCurrentAnimatorStateInfo(0).length);
        }
        // 애니메이션 재생 후 오브젝트 삭제
        destroyEnemy();
    }
    public void bearIdle()
    {
        string enemyName = enemy.name; // 오브젝트 이름 기반
        if (gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            // 전환 중이 아니고 상태가 Idle이 아니면 Idle 상태로 전환
            if (!enemyEffects.IsInTransition(0) && !enemyEffects.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                // Debug.Log($"Switching to Idle for {enemyName}");
                enemyEffects.Play(this.gameObject.name + "Idle");
            }
        }
    }



    public void bearMove()
    {
        string enemyName = enemy.name;
        if (gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            enemyEffects.Play(enemyName + "Walk");
        }
    }

    void Update()
    {
        if (hp == 0)
        {
            StartCoroutine(PlayDeathAnimationAndDestroy());
        }
        else
        {
            // Idle 상태를 지속적으로 유지
            bearIdle();
        }

       // Debug.Log("Current Animator State: " + enemyEffects.GetCurrentAnimatorStateInfo(0).IsName(this.name + "Idle"));
    }

    void Start()
    {
        enemy = this.gameObject;
        enemyEffects = GetComponent<Animator>();
    }
}
