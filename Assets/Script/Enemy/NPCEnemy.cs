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

    public GameObject CtrlKey;
    private Color originalColor;
    public int HP = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) TakeDamage();
    }

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

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        playerControl = FindFirstObjectByType<PlayerControl>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (!playerControl.isMove || HP <= 2) CtrlKey.SetActive(false);
        else CtrlKey.SetActive(true);
    }
}