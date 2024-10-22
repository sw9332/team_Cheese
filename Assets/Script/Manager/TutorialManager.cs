using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class TutorialManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private UIManager uiManager;

    public GameObject TutorialUI;

    public IEnumerator ShowTutorialUI(bool isTutorial, Dialogue dialoge)
    {
        if(isTutorial)
        {
            TutorialUI.SetActive(true);
            while (TutorialUI.activeSelf) yield return null;
            if(dialoge != null) dialogueManager.ShowDialogue(dialoge);
        }

        else
        {
            dialogueManager.ShowDialogue(dialoge);
            while(dialogueManager.dialogue_continue) yield return null;
            TutorialUI.SetActive(true);
            while(TutorialUI.activeSelf) yield return null;
        }
    }

    void Update()
    {
        if (TutorialUI.activeSelf == true && Input.GetKeyDown(KeyCode.Z)) TutorialUI.SetActive(false);
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
}