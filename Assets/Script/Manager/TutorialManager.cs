using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class TutorialManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private UIManager uiManager;

    private bool isTutorialUIClose = false;

    void OnDestroy()
    {
        uiManager.tutorialUIClose -= OnTutorialUIClose;
    }

    void OnTutorialUIClose()
    {
        isTutorialUIClose = true;
    }

    public IEnumerator ShowTutorialUI()
    {
        while (dialogueManager.dialogue_continue)
        {
            yield return null;
        }

        uiManager.TutorialUI.SetActive(true);
        isTutorialUIClose = false;

        while(!isTutorialUIClose)
        {
            if(Input.GetKeyDown(KeyCode.Z)) uiManager.CloseTutorialUI();
            yield return null;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        uiManager = FindObjectOfType<UIManager>();

        uiManager.tutorialUIClose += OnTutorialUIClose;
    }
}
