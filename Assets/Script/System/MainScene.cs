using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject Load_UI;

    private FadeManager fadeManager;

    public void LoadUI() //불러오기 버튼
    {
        Load_UI.SetActive(!Load_UI.activeSelf);
    }

    public void NewGameStartButton() //새 게임 버튼
    {
        StartCoroutine(fadeManager.ChangeSceneFade("GameScene"));
    }

    public void SettingButton() //설정 버튼
    {
        SettingUI.SetActive(true);
    }

    public void ExitButton() //나가기 버튼
    {
        Application.Quit();
    }

    void Update()
    {
        if (Load_UI.activeSelf && Input.GetKeyDown(KeyCode.Escape)) LoadUI();
    }

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}