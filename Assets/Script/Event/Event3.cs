using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private TutorialManager tutorialManager;

    private bool first = true;

    public int hp = 3;

    IEnumerator EnemyEvent()
    {
        if (first)
        {
            while (hp > 0) yield return null;
            yield return new WaitForSeconds(1.5f);
            dialogueManager.ShowDialogue(dialogueContentManager.d_event4);
            while (dialogueManager.dialogue_continue) yield return null;
            tutorialManager.TutorialUI.SetActive(true);
            tutorialManager.TutorialType(8);
            while (tutorialManager.TutorialUI.activeSelf) yield return null;
            first = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && first) hp--;
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();

        StartCoroutine(EnemyEvent());
    }
}
