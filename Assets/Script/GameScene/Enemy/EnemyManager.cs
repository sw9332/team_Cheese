using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies = new();
    [SerializeField] List<Enemy> enemyList = new();
    [SerializeField] List<SpriteRenderer> enemySprites = new();
    [SerializeField] List<Animator> enemyEffects = new();  // Animator override 해서 하고 싶지만 실패, 추후 수정 예정

    private Bullet bullet;
    private bool isCoroutining = false; // 중첩되서 실행되는 것을 방지

    public void takeDamage(string enemyName) // name : bullet에서 가져온 enemy의 tag
    {
        (GameObject objEnemy, Enemy enemy, SpriteRenderer enemySprite, Animator enemyAni) 
            = getEnemyInformation(enemyName);

        enemy.hp -= 1;
        if (!isCoroutining && enemy.hp > 0)
        {
            Debug.Log("hp 까임");
            enemyAni.Play(enemy.name + "Hit");
            StartCoroutine(changeColor(enemySprite));
            StartCoroutine(ResetToDefaultState(enemyAni));
        }

        if (!isCoroutining && enemy.hp == 0)
        {
            destroyEnemy(objEnemy, enemy, enemySprite, enemyAni);  // 적 파괴
        }
    }

    // 실행하면 자동적으로 enemy관련 components들 list에 추가되도록 설정
     void addEnemyInformationInLists()
    {
            enemyList.AddRange(GetComponentsInChildren<Enemy>());
            enemySprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
            enemyEffects.AddRange(GetComponentsInChildren<Animator>());
    }

    // 튜플로 여러 형식을 반환하게 함
    // Damage를 받는 해당 오브젝트의 Components들을 list들에서 반환
    (GameObject , Enemy, SpriteRenderer , Animator) getEnemyInformation(string enemyName)
    {
        int objIndex = enemies.FindIndex(x => x.name.Equals(enemyName));
        GameObject obj = enemies[objIndex];

        int enemyIndex = enemyList.FindIndex(x => x.name.Equals(enemyName));
        Enemy enemy = enemyList[enemyIndex];

        int spriteIndex = enemySprites.FindIndex(x => x.name.Equals(enemyName));
        SpriteRenderer enemySprite = enemySprites[spriteIndex];

        int aniIndex = enemySprites.FindIndex(x => x.name.Equals(enemyName));
        Animator enemyAni = enemyEffects[aniIndex];

        return (obj, enemy, enemySprite, enemyAni);
    }

    void destroyEnemy(GameObject objEnemy, Enemy enemy, SpriteRenderer enemySprite, Animator enemyAni)
    {
        string enemyTag = enemy.name;
        switch (enemyTag)
        {
            case "pinkDollEnemy":
                {
                    deleteEnemyInLists(objEnemy, enemy, enemySprite, enemyAni);
                    break;
                }
            case "rabbitDollEnemy":
                {
                    deleteEnemyInLists(objEnemy, enemy, enemySprite, enemyAni);
                    break;
                }
        }
    }

    // 파괴되는 것 또한 자동으로 되게끔
    void deleteEnemyInLists(GameObject objEnemy, Enemy enemy, SpriteRenderer enemySprite, Animator enemyAni)
    {
        enemies.Remove(objEnemy);
        enemyList.Remove(enemy);
        enemySprites.Remove(enemySprite);
        enemyEffects.Remove(enemyAni);
    }

    IEnumerator changeColor(SpriteRenderer enemySprite)
    {
        isCoroutining = true;
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemySprite.color = Color.white;
        isCoroutining = false;
    }

    IEnumerator ResetToDefaultState(Animator enemyAni)   // 애니메이션을 기본 상태로 전환
    {
        yield return new WaitForSeconds(0.4f);
        enemyAni.Play("None");
    }

    // null reference 오류 발생을 안시키도록
    void removeNullEnemiesFromLists()
    {
        // 뒤에서부터 지워야 인덱스 꼬임이 안 생기므로, 역순으로 순회
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                // 해당 인덱스에 있는 요소들을 모두 제거
                enemies.RemoveAt(i);
                enemyList.RemoveAt(i);
                enemySprites.RemoveAt(i);
                enemyEffects.RemoveAt(i);
            }
        }
    }


    void Start()
    {
        bullet = FindObjectOfType<Bullet>();
        addEnemyInformationInLists();
    }

     void Update()
    {
        removeNullEnemiesFromLists();
    }
}
