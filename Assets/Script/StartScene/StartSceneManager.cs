using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public static int Screen_Frame;

    //Screen.currentReolution.refreshRate 현재 모니터 주사율을 가져옴으로써 개인 컴퓨터의 최적화 된 프레임으로 동기화 가능.

    void Awake()
    {
        Screen_Frame = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = Screen_Frame;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
