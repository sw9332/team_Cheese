using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class TutorialManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private UIManager uiManager;

    public GameObject TutorialUI;

    public GameObject Move_Image;
    public GameObject ObjectInstallation_Image;
    public GameObject ObjectPush_Image;
    public GameObject Attack_Image;
    public GameObject Camera_Image;

    public IEnumerator ShowTutorialUI(bool isTutorial, Dialogue dialoge)
    {
        if (isTutorial)
        {
            TutorialUI.SetActive(true);
            while (TutorialUI.activeSelf) yield return null;
            if(dialoge != null) dialogueManager.ShowDialogue(dialoge);
        }

        else
        {
            dialogueManager.ShowDialogue(dialoge);
            while (dialogueManager.dialogue_continue) yield return null;
            TutorialUI.SetActive(true);
            while(TutorialUI.activeSelf) yield return null;
        }
    }

    public void TutorialType(int type)
    {
        switch (type)
        {
            case 1:
                Move_Image.SetActive(true);
                ObjectInstallation_Image.SetActive(false);
                ObjectPush_Image.SetActive(false);
                Attack_Image.SetActive(false);
                Camera_Image.SetActive(false);
                break;

            case 2:
                Move_Image.SetActive(false);
                ObjectInstallation_Image.SetActive(true);
                ObjectPush_Image.SetActive(false);
                Attack_Image.SetActive(false);
                Camera_Image.SetActive(false);
                break;

            case 3:
                Move_Image.SetActive(false);
                ObjectInstallation_Image.SetActive(false);
                ObjectPush_Image.SetActive(true);
                Attack_Image.SetActive(false);
                Camera_Image.SetActive(false);
                break;

            case 4:
                Move_Image.SetActive(false);
                ObjectInstallation_Image.SetActive(false);
                ObjectPush_Image.SetActive(false);
                Attack_Image.SetActive(false);
                Camera_Image.SetActive(false);
                break;

            case 5:
                Move_Image.SetActive(false);
                ObjectInstallation_Image.SetActive(false);
                ObjectPush_Image.SetActive(false);
                Attack_Image.SetActive(false);
                Camera_Image.SetActive(false);
                break;

            case 6:
                Move_Image.SetActive(false);
                ObjectInstallation_Image.SetActive(false);
                ObjectPush_Image.SetActive(false);
                Attack_Image.SetActive(true);
                Camera_Image.SetActive(false);
                break;
             
            case 7:
                Move_Image.SetActive(false);
                ObjectInstallation_Image.SetActive(false);
                ObjectPush_Image.SetActive(false);
                Attack_Image.SetActive(false);
                Camera_Image.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if (TutorialUI.activeSelf == true && Input.GetKeyDown(KeyCode.Z))
        {
            TutorialUI.SetActive(false);
        }
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        uiManager = FindFirstObjectByType<UIManager>();
    }
}