using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Text text;
    public SpriteRenderer sprite;
    public Text button_text;

    public List<string> contentsList;
    public List<Sprite> spriteList;

    public GameObject ingameUiPanel;
    public GameObject DialoguePanel;
    public GameObject ChoiceButtonPanel;

    public Text ChoiceText_1;
    public Text ChoiceText_2;

    public GameObject CloseButton;

    public int count; // ��ȭ �����Ȳ ǥ�ÿ�, Ȯ�� �� private �� ���� �ʿ�
    public float delay = 0.03f;

    public bool dialogue_continue = false;
    public bool is_talking = false;
    public bool is_ChoiceButton = false;
    public bool is_ChoiceExpected = false;

    public PlayerControl playerControl;
    public MiniGame minigame;
    public FadeManager fadeManager;

    public Animator animator;

    public void ShowDialogue(Dialogue dialogue) // dlalogue�� sprite������ contents ������ �޾ƿ��� �Լ�
    {
        for (int i = 0; i < dialogue.contents.Length; i++)
        {
            contentsList.Add(dialogue.contents[i]);
            spriteList.Add(dialogue.sprites[i]);
        }

        animator.Play("Dialogue Up");
        StartCoroutine(startDialogueCoroutine());
        dialogue_continue = true;
        button_text.text = "����";
        ingameUiPanel.SetActive(false);
        playerControl.isMove = false;
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
        DialoguePanel = transform.GetChild(0).gameObject;
        text.text = "";
        contentsList.Clear();
        spriteList.Clear();
        count = 0;
        ingameUiPanel.SetActive(true);
        dialogue_continue = false;
        if (!fadeManager.isFade) playerControl.isMove = true;
    }

    public IEnumerator startDialogueCoroutine()
    {
        if (count == 0)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];

            is_talking = true;

            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(delay);
            }

            is_talking = false;
        }

        if (count != 0) //�ε��� ������ ���� 0�϶��� �ƴҶ� ����
        {
            if (spriteList[count] != spriteList[count - 1]) sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];

            is_talking = true;

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
            if (count == contentsList.Count - 2) button_text.text = "�ݱ�";

            count++;
            text.text = "";

            if (count == contentsList.Count)
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
                if (count == contentsList.Count - 2) button_text.text = "�ݱ�";

                count++;
                text.text = "";

                if (count == contentsList.Count)
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
