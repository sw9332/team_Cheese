using System.Collections;
using UnityEngine;

public class NPCEnemy : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;
    private SpriteRenderer spriteRenderer;
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
            case 2: StartCoroutine(fadeManager.ChangeSceneFade("MainScene")); break;
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
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        fadeManager = FindObjectOfType<FadeManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
}