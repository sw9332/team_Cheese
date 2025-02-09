using System.Collections;
using UnityEngine;

public class NPCEnemy : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;
    private SpriteRenderer spriteRenderer;
    private PlayerControl playerControl;
    private CutSceneManager cutSceneManager;
    private NPC npc;

    private Animator animator;
    private Rigidbody2D rigid;
    public GameObject CtrlKey;

    public int HP = 5;
    public int HP2 = 5;

    private bool FreezeX = false;
    private bool FreezeY = false;

    public bool event1 = false;
    public bool event2 = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Bullet") && !event1 && !event2)
        {
            animator.SetTrigger("Damage");
            HP--;
            StartCoroutine(WaitForDamage());
        }

        if (other.CompareTag("Bullet") && event1 && event2)
        {
            animator.SetTrigger("Damage");
            HP2--;
            StartCoroutine(WaitForDamage());
        }

        if (other.CompareTag("NPC Boss Event 1"))
        {
            event1 = true;
            other.gameObject.SetActive(false);
            dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_1);
        }

        if (other.CompareTag("NPC Boss Event 2"))
        {
            event2 = true;
            other.gameObject.SetActive(false);
            dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_2);
        }

        if (other.CompareTag("NPC Boss Event 3")) other.gameObject.SetActive(false);
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetTrigger("idle");
    }

    void Update()
    {
        if (dialogueManager.dialogue_continue) CtrlKey.SetActive(false);

        if (PlayerControl.speed == 2 && npc.die)
        {
            if (PlayerControl.MoveX && !PlayerControl.MoveY && !FreezeX)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
                rigid.freezeRotation = true;
            }

            else if (PlayerControl.MoveY && !PlayerControl.MoveX && !FreezeY)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
                rigid.freezeRotation = true;
            }

            else if (!FreezeX && !FreezeY) rigid.constraints = RigidbodyConstraints2D.None;
            else rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        else rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        playerControl = FindFirstObjectByType<PlayerControl>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        npc = FindFirstObjectByType<NPC>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
}