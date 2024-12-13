using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    public void Choice_1()
    {
        dialogueManager.ChoiceButton(false, "", "");
        Clear();

        switch (GameManager.GameState)
        {
            case "CutScene5": dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_3); break;
        }
    }

    public void Choice_2()
    {
        dialogueManager.ChoiceButton(false, "", "");
        Clear();

        switch (GameManager.GameState)
        {
            case "CutScene5": dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_3); break;
        }
    }

    void Clear()
    {
        dialogueManager.DialoguePanel = transform.GetChild(0).gameObject;
        dialogueManager.text.text = "";
        dialogueManager.contentsList.Clear();
        dialogueManager.spriteList.Clear();
        dialogueManager.count = 0;
        dialogueManager.ingameUiPanel.SetActive(true);
        dialogueManager.DialoguePanel.SetActive(false);
        dialogueManager.dialogue_continue = false;
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
    }
}