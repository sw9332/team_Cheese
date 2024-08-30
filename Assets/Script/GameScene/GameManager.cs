using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Tutorial";

    void Start()
    {
        GameState = "Tutorial"; /* Tutorial / InGame */
    }
}