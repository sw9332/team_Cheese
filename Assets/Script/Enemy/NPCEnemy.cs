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

    private bool FreezeX = false;
    private bool FreezeY = false;

    public bool trigger = false;

    void TakeDamage()
    {
        StartCoroutine(FlashRed());
        HP--;
        StartCoroutine(Talk());
    }

    IEnumerator Talk()
    {
        switch(HP)
        {
            case 4: yield return null; dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_2); break;
            case 3: dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_3); break;
            case 2: GameManager.GameState = "CutScene2"; CtrlKey.SetActive(false); break;
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
        if (other.CompareTag("Bullet")) TakeDamage();
        if (other.CompareTag("NPC Boss Event")) trigger = true;
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