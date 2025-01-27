using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Player : MonoBehaviour
{
    private PlayerControl player;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    private Animator animator;

    public bool isFollow = false;
    public bool event4 = false;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Event 1 (Chapter 2)":
                dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event1);
                Destroy(other.gameObject);
                break;

            case "Event 2 (Chapter 2)":
                dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event2);
                Destroy(other.gameObject);
                break;

            case "Event 4 (Chapter 2)":
                dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event4);
                event4 = true;
                isFollow = false;
                Destroy(other.gameObject);
                break;
        }

        if (other.CompareTag("Player") && event4)
        {
            dialogueManager.ShowDialogue(dialogueContentManager.chapter2_event5);
            event4 = false;
        }
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
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();

        animator = GetComponent<Animator>();
    }
}