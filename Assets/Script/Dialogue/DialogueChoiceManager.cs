using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    public bool choice1 = true;
    public bool choice2 = false;

    public Image choiceButton1;
    public Image choiceButton2;

    public void Choice_1()
    {
        dialogueManager.ShowChoiceDialogue(false, "", "");
        Clear();

        switch (GameManager.GameState)
        {
            case "CutScene5": dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_3); break;
        }
    }

    public void Choice_2()
    {
        dialogueManager.ShowChoiceDialogue(false, "", "");
        Clear();

        switch (GameManager.GameState)
        {
            case "CutScene5": dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_3); break;
        }
    }

    void ChoiceButton()
    {
        if (dialogueManager.dialogue_continue && dialogueManager.is_ChoiceButton)
        {
            if (choice1)
            {
                choiceButton1.color = new Color32(170, 170, 170, 255);
                choiceButton2.color = Color.white;

                if (Input.GetKeyDown(KeyCode.Space)) Choice_1();
            }

            else if (choice2)
            {
                choiceButton1.color = Color.white;
                choiceButton2.color = new Color32(170, 170, 170, 255);

                if (Input.GetKeyDown(KeyCode.Space)) Choice_2();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                choice1 = true;
                choice2 = false;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                choice1 = false;
                choice2 = true;
            }
        }
    }

    void Clear()
    {
        dialogueManager.text.text = "";
        dialogueManager.contentsList.Clear();
        dialogueManager.nameList.Clear();
        dialogueManager.count = 0;
        dialogueManager.ingameUiPanel.SetActive(true);
        dialogueManager.dialogue_continue = false;
        dialogueManager.is_ChoiceButton = false;
        dialogueManager.is_ChoiceExpected = false;
        dialogueManager.Player.gameObject.SetActive(false);
        dialogueManager.NPC.gameObject.SetActive(false);
    }

    void Update()
    {
        ChoiceButton();
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
    }
}