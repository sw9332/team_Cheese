using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public List<string> contentsList;
    public List<string> nameList;
    public List<int> fontSizeList;

    public Text text;
    public Text button_text;

    public Image Player;
    public Image NPC;

    public GameObject ingameUiPanel;
    public GameObject DialoguePanel;
    public GameObject ChoiceButtonPanel;

    public Text ChoiceText_1;
    public Text ChoiceText_2;

    public GameObject CloseButton;

    public Animator animator;

    public int count; // ��ȭ �����Ȳ ǥ�ÿ�, Ȯ�� �� private �� ���� �ʿ�
    public float delay = 0.03f;

    public bool dialogue_continue = false;
    public bool is_talking = false;
    public bool is_ChoiceButton = false;
    public bool is_ChoiceExpected = false;

    public PlayerControl playerControl;
    private FadeManager fadeManager;

    public void ShowImage()
    {
        if (count < nameList.Count)
        {
            switch (nameList[count])
            {
                case "": Player.gameObject.SetActive(false); NPC.gameObject.SetActive(false); break;
                case "���ΰ�": Player.gameObject.SetActive(true); NPC.gameObject.SetActive(false); break;
                case "NPC": Player.gameObject.SetActive(false); NPC.gameObject.SetActive(true); break;
            }
        }
    }

    public void ShowDialogue(Dialogue dialogue) // dlalogue�� sprite������ contents ������ �޾ƿ��� �Լ�
    {
        contentsList.Clear();
        nameList.Clear();
        fontSizeList.Clear();

        for (int i = 0; i < dialogue.contents.Length; i++)
        {
            contentsList.Add(dialogue.contents[i]);
            nameList.Add(dialogue.name[i]);
            fontSizeList.Add(dialogue.fontSize[i]);
        }

        is_talking = true;
        animator.Play("Dialogue Up");
        StartCoroutine(WaitForDialogue());
        dialogue_continue = true;
        button_text.text = "����";
        ingameUiPanel.SetActive(false);
        playerControl.isMove = false;
    }

    IEnumerator WaitForDialogue()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(startDialogueCoroutine());
    }

    public void ShowChoiceDialogue(bool isChoice, string ChoiceButton_1, string ChoiceButton_2)
    {
        if (isChoice)
        {
            animator.Play("Dialogue Up");
            dialogue_continue = true;
            ChoiceText_1.text = ChoiceButton_1;
            ChoiceText_2.text = ChoiceButton_2;
            ChoiceButtonPanel.SetActive(true);
            CloseButton.SetActive(false);
            is_ChoiceButton = true;
        }

        else
        {
            dialogue_continue = false;
            ChoiceButtonPanel.SetActive(false);
            CloseButton.SetActive(true);
            is_ChoiceButton = false;
        }
    }

    public void ExitDialogue()
    {
        if (!is_ChoiceExpected) animator.Play("Dialogue Down");
        text.text = "";
        contentsList.Clear();
        nameList.Clear();
        fontSizeList.Clear();
        count = 0;
        ingameUiPanel.SetActive(true);
        dialogue_continue = false;
        Player.gameObject.SetActive(false);
        NPC.gameObject.SetActive(false);
        if (!fadeManager.isFade) playerControl.isMove = true;
    }

    public IEnumerator startDialogueCoroutine()
    {
        if (count == 0)
        {
            is_talking = true;

            ShowImage();
            text.fontSize = fontSizeList[count];

            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(delay);
            }

            is_talking = false;
        }

        if (count != 0) //�ε��� ������ ���� 0�϶��� �ƴҶ� ����
        {
            is_talking = true;

            ShowImage();
            text.fontSize = fontSizeList[count];

            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(delay);
            }

            is_talking = false;
        }

        yield return null;
    }

    public void NextSentence()
    {
        if (dialogue_continue && !is_ChoiceButton && is_talking == false)
        {
            if (count >= contentsList.Count - 1) button_text.text = "�ݱ�";

            count++;
            text.text = "";

            ShowImage();

            if (count >= contentsList.Count)
            {
                StopCoroutine(startDialogueCoroutine());
                ExitDialogue();
            }

            else
            {
                StopCoroutine(startDialogueCoroutine());
                StartCoroutine(startDialogueCoroutine());
            }
        }
    }

    void Update()
    {
        if (dialogue_continue && !is_talking)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !is_ChoiceButton)
            {
                if (count >= contentsList.Count - 1) button_text.text = "�ݱ�";

                count++;
                text.text = "";

                if (count >= contentsList.Count)
                {
                    StopCoroutine(startDialogueCoroutine());
                    ExitDialogue();
                }

                else
                {
                    StopCoroutine(startDialogueCoroutine());
                    StartCoroutine(startDialogueCoroutine());
                }
            }

           
        }
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();

        text.text = "";
        count = 0;
    }

    //Ȥ�ó� ���׳� �� �����ؼ� ����� �ڵ�...

    //public void NextSentence()
    //{
    //    if (dialogue_continue)
    //    {
    //        count++;
    //        text.text = "";
    //        if (count == contentsList.Count - 1)
    //        {
    //            StopCoroutine(startDialogueCoroutine());
    //        }
    //        else
    //        {
    //            StopCoroutine(startDialogueCoroutine());
    //            StartCoroutine(startDialogueCoroutine());
    //        }
    //    }        
    //}

    //private void Update()
    //{
    //    if (dialogue_continue)
    //    {
    //        if(Input.GetKeyDown(KeyCode.Z))
    //        {
    //            //count++;
    //            //text.text = "";
    //            //if (count == contentsList.Count)
    //            //{
    //            //    StopCoroutine(startDialogueCoroutine());
    //            //    ExitDialogue();

    //            //}
    //            //else
    //            //{
    //            //    StopCoroutine(startDialogueCoroutine());
    //            //    StartCoroutine(startDialogueCoroutine());
    //            //}
    //        }
    //    }
    //}
}
