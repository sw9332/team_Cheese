using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    private PlayerControl playerControl;

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();

        if (playerControl != null)
        {
            Vector3 directionToPlayer = (playerControl.transform.position - transform.position).normalized;
            transform.up = directionToPlayer;
        }

        Invoke("DestroyBullet", 1f);
    }
}