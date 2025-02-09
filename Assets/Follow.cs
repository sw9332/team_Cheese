using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private PlayerControl player;
    private N_Player n_Player;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    private Animator animator;

    public float followDistance = 2f;

    void FollowPlayer()
    {
        if (n_Player.isFollow)
        {
            Vector3 direction = player.transform.position - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            if (distance < followDistance)
            {
                animator.Play("N_Player");
                return;
            }

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (!player.stop)
                {
                    if (direction.x > 0)
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
                    if (direction.y > 0)
                        animator.Play("N_Player Up");
                    else
                        animator.Play("N_Player Down");
                }

                else animator.Play("N_Player");
            }

            transform.position = player.transform.position - direction * followDistance;
        }

        else animator.Play("N_Player");
    }

    void Update()
    {
        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.Q) && !n_Player.isFollow) n_Player.isFollow = true;
        else if (Input.GetKeyDown(KeyCode.Q) && n_Player.isFollow) n_Player.isFollow = false;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
        n_Player = FindFirstObjectByType<N_Player>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();

        animator = GetComponent<Animator>();
    }
}
