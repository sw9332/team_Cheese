using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class TutorialManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private UIManager uiManager;

    public IEnumerator ShowTutorialUI()
    {
        while (dialogueManager.dialogue_continue)
        {
            yield return null;
        }

        uiManager.TutorialUI.SetActive(true);
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
}