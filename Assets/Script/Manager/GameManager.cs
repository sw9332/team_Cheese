using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Tutorial";

    private DialogueManager dialogueManager;
    private FadeManager fadeManager;

    void Start()
    {
        GameState = "Tutorial"; /* Tutorial -> Tutorial Cut Scene -> Stage1 -> Demo */

        dialogueManager = FindObjectOfType<DialogueManager>();
        fadeManager = FindObjectOfType<FadeManager>();
    }

    void Update()
    {
        if (GameState == "Tutorial Cut Scene")
        {
            if (dialogueManager.button_text.text == "´Ý±â")
                if (Input.GetKeyDown(KeyCode.Z))
                    StartCoroutine(fadeManager.ChangeStateFade("Stage1"));
        }
    }
}