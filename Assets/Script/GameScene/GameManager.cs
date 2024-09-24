using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Tutorial";

    private DialogueManager dialogueManager;
    public FadeManager fadeManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        GameState = "Tutorial"; /* Tutorial -> Tutorial Cut Scene -> InGame */
    }

    void Update()
    {
        if(GameState == "Tutorial Cut Scene")
        {
            if (dialogueManager.dialogue_continue && dialogueManager.is_talking == false)
                if (dialogueManager.button_text.text == "´Ý±â")
                    if (Input.GetKeyDown(KeyCode.Z))
                        StartCoroutine(fadeManager.NextSceneFade("InGame"));
        }
    }
}