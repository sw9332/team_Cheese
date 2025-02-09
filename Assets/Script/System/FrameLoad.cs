using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLoad : MonoBehaviour
{
    public static int Frame = 60;

    void Start()
    {
        Frame = PlayerPrefs.GetInt("Frame", 60);
        Application.targetFrameRate = Frame;
    }
}