using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject enemy;
    // public Animator enemyEffect;
    public int hp = 3;
    public bool isDead = false;

     void destroyEnemy(GameObject enemy)
    {
        if(isDead == true)
        {
            Destroy(enemy);
        }
    }

    private void Start()
    {
        enemy = this.gameObject;
    }
    void Update()
    {
        destroyEnemy(enemy);
    }
}
