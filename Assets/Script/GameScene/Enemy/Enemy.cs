using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 3;
    private bool isCoroutining = false; // 중첩되서 실행되는 것을 방지
    public Bullet bullet;
    public GameObject enemy;
    public SpriteRenderer enemySprite;
    public Animator enemyEffect;
    public bool isDead = false;


    public void takeDamage(string enemyName)
    {
        if (!isDead && !isCoroutining && hp > 0)
        {
            Debug.Log("hp 까임");
            hp-=1;
            enemyEffect.Play(enemy.tag+"Hit");
            StartCoroutine(ResetToDefaultState());
            StartCoroutine(changeColorToRed());

            if (hp == 0)
            {
                isDead = true;
                destroyEnemy();  // 적 파괴
            }
        }
    }



    IEnumerator changeColorToRed()
    {
       isCoroutining = true;
       enemySprite.color = Color.red;
       yield return new WaitForSeconds(0.3f);
       enemySprite.color = Color.white;
       isCoroutining = false;
    }

    IEnumerator ResetToDefaultState()
    {
        yield return new WaitForSeconds(0.4f); // 애니메이션 재생 시간만큼 대기
        enemyEffect.Play("None");    // 기본 상태로 전환 
    }

    void destroyEnemy()
    {
        string name = enemy.tag;
        switch (name)
        {
            case "pinkDollEnemy":
                {
                    Destroy(enemy);
                    enemyEffect.Play("pinkDollDying");
                    break;
                }
            case "rabbitDollEnemy":
                {
                    Destroy(enemy);
                    enemyEffect.Play("rabbitDying");
                    break;
                }
        }

    }


}
