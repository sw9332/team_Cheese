using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Æ©Åä¸®¾ó";
    public string GameStatePrint;

    private DialogueManager dialogueManager;
    private FadeManager fadeManager;

    void Start()
    {
        GameState = "Æ©Åä¸®¾ó"; /* Æ©Åä¸®¾ó -> Æ©Åä¸®¾ó ÄÆ¾À -> ÆÄÆ¼·ë -> Ã¢°í */

        dialogueManager = FindObjectOfType<DialogueManager>();
        fadeManager = FindObjectOfType<FadeManager>();
    }

    void Update()
    {
        if (GameState == "Æ©Åä¸®¾ó ÄÆ¾À")
            if (dialogueManager.button_text.text == "´Ý±â")
                if (Input.GetKeyDown(KeyCode.Z))
                    StartCoroutine(fadeManager.ChangeStateFade("ÆÄÆ¼·ë"));
        GameStatePrint = GameState;
    }
}