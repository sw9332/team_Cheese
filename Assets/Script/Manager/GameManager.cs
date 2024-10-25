using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Æ©Åä¸®¾ó";
    public string GameStatePrint;

    void Start()
    {
        GameState = "Æ©Åä¸®¾ó";
    }

    void Update()
    {
        GameStatePrint = GameState;
    }
}