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
        if (isFollow)
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

                else animator.Play("N_Player");

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

                else animator.Play("N_Player");
            }

            transform.position = player.transform.position - directionToPlayer * followDistance;
        }

        else animator.Play("N_Player");
    }

    void Update()
    {
        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.Q) && !isFollow) isFollow = true;
        else if (Input.GetKeyDown(KeyCode.Q) && isFollow) isFollow = false;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        animator = GetComponent<Animator>();
    }
}