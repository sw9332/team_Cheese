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

    public bool trigger = false;

    void Talk1()
    {
        switch (HP)
        {
            case 3: dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_2); break;
            case 1: dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_3); break;
            case -1: GameManager.GameState = "CutScene2"; CtrlKey.SetActive(false); break;
        }
    }

    void Talk2()
    {
        switch (HP2)
        {
            case 3: dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_3); break;
            case 1: dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_4); break;
            case -1: dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_5); GameManager.Demo = true; CtrlKey.SetActive(false); break;
        }
    }

    void TakeDamage(int trigger)
    {
        switch(trigger)
        {
            case 1: HP--; Talk1(); break;
            case 2: HP2--; Talk2(); break;
        }

        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !trigger) TakeDamage(1);
        else if (other.CompareTag("Bullet") && trigger) TakeDamage(2);

        if (other.CompareTag("NPC Boss Event 1"))
        {
            trigger = true;
            other.gameObject.SetActive(false);
            dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_1);
        }

        if (other.CompareTag("NPC Boss Event 2"))
        {
            CtrlKey.SetActive(true);
            dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_2);
        }
    }

    void Update()
    {
        if (!playerControl.isMove || HP <= 2) CtrlKey.SetActive(false);
        else CtrlKey.SetActive(true);

        if (PlayerControl.speed == 2 && npc.die)
        {
            if (PlayerControl.MoveY && !PlayerControl.MoveX && !FreezeY) // 세로로 밀었을 때
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX; // X축 고정
                rigid.freezeRotation = true;
            }
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