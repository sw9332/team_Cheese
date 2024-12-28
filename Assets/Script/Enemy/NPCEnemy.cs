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

    private Rigidbody2D rigid;

    public GameObject CtrlKey;
    private Color originalColor;

    public int HP = 5;
    public int HP2 = 5;

    private bool FreezeX = false;
    private bool FreezeY = false;

    public bool event1 = false;
    public bool event2 = false;

    void Talk1()
    {
        HP--;
        StartCoroutine(FlashRed());

        switch (HP)
        {
            case 3: dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_2); break;
            case 1: dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_3); break;
            case -1: GameManager.GameState = "CutScene2"; CtrlKey.SetActive(false); break;
        }
    }

    void Talk2()
    {
        HP2--;
        StartCoroutine(FlashRed());

        switch (HP2)
        {
            case 3: dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_3); break;
            case 1: dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_4); break;
            case -1: dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_5); GameManager.Demo = true; CtrlKey.SetActive(false); break;
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !event1 && !event2) Talk1();
        else if (other.CompareTag("Bullet") && event1 && event2) Talk2();

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
    }

    void Update()
    {
        if (!event1 && !event2)
        {
            if (!playerControl.isMove || HP <= 2) CtrlKey.SetActive(false);
            else CtrlKey.SetActive(true);
        }

        else
        {
            if (!playerControl.isMove) CtrlKey.SetActive(false);
            else if (playerControl.isMove && event1 && event2) CtrlKey.SetActive(true);
        }

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

        rigid = GetComponent<Rigidbody2D>();
        originalColor = spriteRenderer.color;
    }
}