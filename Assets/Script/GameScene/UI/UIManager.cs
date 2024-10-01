using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public FadeManager fadeManager;

    public GameObject CameraUI;
    public GameObject Pause_UI;
    public GameObject SettingUI;

    public Image fadeImage;

    //카메라 UI 효과
    public Animator Camera_Effect_Animation;
    public bool isCameraEffect = false;

    //튜토리얼 맵 변수
    public static bool tutorialTrigger = false;
    public static bool is_bear = false;
    public static bool is_cake = false;
    public static bool is_playerPos = false;

    void Camera_effect()
    {
        if (Camera_Effect_Animation.gameObject.activeInHierarchy)
        {
            switch (isCameraEffect)
            {
                case false:
                    Camera_Effect_Animation.Play("Camera_Effect_false");
                    break;

                case true:
                    Camera_Effect_Animation.Play("Camera_Effect_true");
                    break;
            }
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause_UI.activeSelf == false)
            {
                Pause_UI.SetActive(true);
                Time.timeScale = 0;
            }

            else if (Pause_UI.activeSelf == true)
            {
                Pause_UI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Setting()
    {
        SettingUI.SetActive(true);
    }

    public void Game_Exit_Yes()
    {
        Pause_UI.SetActive(false);
        StartCoroutine(fadeManager.FadeOut());
        Invoke("MainScene", 1f);
        Time.timeScale = 1;
    }

    public void Game_Exit_No()
    {
        Pause_UI.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Start()
    {
        Time.timeScale = 1;
        fadeImage.gameObject.SetActive(true);
        isCameraEffect = false;
    }

    void Update()
    {
        Camera_effect(); //카메라 UI 효과
        Pause();

        if (is_bear == true && is_cake == true && is_playerPos == true)
        {
            tutorialTrigger = true;
        }
        else
        {
            tutorialTrigger = false;
        }

        if(tutorialTrigger == true) //카메라 UI 반짝임 애니메이션을 제어하는 조건문 , 스테이지 추가 할 때 마다 || 연산자를 사용하여 조건식에 추가 해 줄것.
        {
            isCameraEffect = true;
            MiniGame.is_take_photo = true;
        }
        else
        {
            isCameraEffect = false;
            MiniGame.is_take_photo = false;
        }
    }
}