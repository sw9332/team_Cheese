using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Player : MonoBehaviour
{
    private PlayerControl player;
    private Animator animator;

    public bool isFollow = false;

    public float followDistance = 1f;

    void FollowPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        directionToPlayer.Normalize();

        if (distanceToPlayer < followDistance)
        {
            animator.Play("N_Player");
            return;
        }

        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
        {
            if (!player.stop)
            {
                if (directionToPlayer.x > 0)
                    animator.Play("N_Player Right");
                else
                    animator.Play("N_Player Left");
            }
            
        }

        else
        {
            if (!player.stop)
            {
                if (directionToPlayer.y > 0)
                    animator.Play("N_Player Up");
                else
                    animator.Play("N_Player Down");
            }
        }

        transform.position = player.transform.position - directionToPlayer * followDistance;
    }

    void Update()
    {
        if (isFollow) FollowPlayer();
        if (player.stop) animator.Play("N_Player");
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        animator = GetComponent<Animator>();
    }
}