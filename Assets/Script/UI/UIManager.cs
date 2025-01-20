using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    private SaveManager saveManager;
    private FadeManager fadeManager;
    private InventoryManager inventoryManager;

    public GameObject CameraUI;
    public GameObject Pause_UI;
    public GameObject SettingUI;

    public Image fadeImage;
    public Image GameOver;

    //카메라 UI 효과
    public Animator Camera_Effect_Animation;

    //튜토리얼 맵 변수
    public static bool tutorialTrigger = false;
    public static bool is_bear = false;
    public static bool is_cake = false;
    public static bool is_playerPos = false;

    //Stage1 변수
    public static bool is_NPC = false;
    public static bool is_CutScene_4 = false;

    public void CameraEffect(bool isCameraEffect)
    {
        if (Camera_Effect_Animation.gameObject.activeInHierarchy)
        {
            switch (isCameraEffect)
            {
                case false: Camera_Effect_Animation.Play("Camera_Effect_false"); break;
                case true: Camera_Effect_Animation.Play("Camera_Effect_true"); break;
            }
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pause_UI.activeSelf)
            {
                Pause_UI.SetActive(true);
                Time.timeScale = 0;
            }

            else if (Pause_UI.activeSelf)
            {
                Pause_UI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void SettingButton()
    {
        SettingUI.SetActive(true);
    }

    public void NewGame()
    {
        StartCoroutine(fadeManager.ChangeSceneFade("GameScene"));
    }

    public void Load()
    {
        saveManager.Load();
        GameOver.gameObject.SetActive(false);
    }

    public void GameExit()
    {
        Pause_UI.SetActive(false);
        StartCoroutine(fadeManager.ChangeSceneFade("MainScene"));
        Time.timeScale = 1;
    }

    public void Close()
    {
        Pause_UI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Camera_ON_OFF()
    {
        if (inventoryManager.miniGameCamera) CameraUI.SetActive(true);
        else CameraUI.SetActive(false);
    }

    public void CameraEvent() //미니게임 실행 조건
    {
        if (is_bear && is_cake && is_playerPos) //튜토리얼 조건
            tutorialTrigger = true;
        else
            tutorialTrigger = false;

        if (inventoryManager.miniGameCamera)
        {
            if (tutorialTrigger || is_NPC || is_CutScene_4 || GameManager.Demo)
            {
                CameraEffect(true);
                MiniGame.is_take_photo = true;
            }

            else
            {
                CameraEffect(false);
                MiniGame.is_take_photo = false;
            }
        }
    }

    void Update()
    {
        Pause();
        Camera_ON_OFF();
        CameraEvent();
    }

    void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        CameraEffect(false);
    }
}