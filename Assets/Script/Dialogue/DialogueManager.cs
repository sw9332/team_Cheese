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

    public int count; // 대화 진행상황 표시용, 확인 후 private 로 변경 필요
    public float delay = 0.03f;

    public bool dialogue_continue = false;
    public bool is_talking = false;
    public bool is_ChoiceButton = false;

    public PlayerControl playerControl;
    public MiniGame minigame;
    public FadeManager fadeManager;

    private void Start()
    {
        text.text = "";
        count = 0;
    }

    public void ShowDialogue(Dialogue dialogue) // dlalogue의 sprite정보와 contents 정보를 받아오는 함수
    {
        for (int i = 0; i < dialogue.contents.Length; i++)
        {
            contentsList.Add(dialogue.contents[i]);
            spriteList.Add(dialogue.sprites[i]);
        }
        dialogue_continue = true;
        button_text.text = "다음";
        ingameUiPanel.SetActive(false);
        DialoguePanel.SetActive(true);
        StartCoroutine(startDialogueCoroutine());
        playerControl.isMove = false;
    }

    public void ExitDialogue()
    {
        DialoguePanel = transform.GetChild(0).gameObject;
        text.text = "";
        contentsList.Clear();
        spriteList.Clear();
        count = 0;
        ingameUiPanel.SetActive(true);
        DialoguePanel.SetActive(false);
        dialogue_continue = false;
        if(!fadeManager.isFade) playerControl.isMove = true;
    }

    IEnumerator startDialogueCoroutine()
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

        if (count != 0) //인덱스 오류로 인해 0일때와 아닐때 구분
        {
            if (spriteList[count] != spriteList[count - 1])
            {
                sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];
            }
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

    public void ChoiceButton(bool isChoice, string ChoiceButton_1, string ChoiceButton_2)
    {
        if (isChoice)
        {
            ChoiceText_1.text = ChoiceButton_1;
            ChoiceText_2.text = ChoiceButton_2;
            ChoiceButtonPanel.SetActive(true);
            CloseButton.SetActive(false);
            is_ChoiceButton = true;
        }

        else
        {
            ChoiceButtonPanel.SetActive(false);
            CloseButton.SetActive(true);
            is_ChoiceButton = false;
        }
    }

    public void NextSentence()
    {
        if (dialogue_continue && is_talking == false)
        {
            if (count == contentsList.Count - 2)
            {
                button_text.text = "닫기";
            }
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

    private void Update()
    {
        if (dialogue_continue && !is_talking)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !is_ChoiceButton)
            {
                if (count == contentsList.Count - 2)
                {
                    button_text.text = "닫기";
                }
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

    //혹시나 버그날 때 복구해서 사용할 코드...

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
