using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject Load_UI;
    public GameObject ScreenButton;

    public static bool ScreenStartCheck = false;

    private FadeManager fadeManager;

    public void LoadUI() //불러오기 버튼
    {
        if(Load_UI.activeSelf == false)
            Load_UI.SetActive(true);
        else if(Load_UI.activeSelf == true)
            Load_UI.SetActive(false);
    }

    public void StartButton_Fade() //새 게임 버튼
    {
        StartCoroutine(fadeManager.FadeOut());
        Invoke("tutorial_start", 1f);
    }

    public void SettingButton() //설정 버튼
    {
        SettingUI.SetActive(true);
    }

    public void ExitButton() //나가기 버튼
    {
        Application.Quit();
    }

    public void ScreenClickButton()
    {
        ScreenButton.SetActive(false);
        ScreenStartCheck = true;
    }

    void tutorial_start()
    {
        SceneManager.LoadScene("GameScene");
    }

    public Collider2D flag;
    public Transform flag_pos;
    public GameObject TeddyBear;
    public GameObject TeddyBear_yellow;
    public GameObject TeddyBear_pink;

    void MainObjectClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider == flag)
            {   
                int Rand = Random.Range(1,4);

                switch(Rand)
                {
                    case 1: Instantiate(TeddyBear, new Vector2(Random.Range(2.8f, 7.5f),flag_pos.position.y), flag.transform.rotation); break;
                    case 2: Instantiate(TeddyBear_yellow, new Vector2(Random.Range(2.8f, 7.5f),flag_pos.position.y), flag.transform.rotation); break;
                    case 3: Instantiate(TeddyBear_pink, new Vector2(Random.Range(2.8f, 7.5f),flag_pos.position.y), flag.transform.rotation); break;
                }                    
            }
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();

        if(!ScreenStartCheck)
        {
            StartCoroutine(fadeManager.FadeIn());
            ScreenButton.SetActive(true);
        }

        else
            ScreenButton.SetActive(false);
    }
    void Update()
    {
        MainObjectClick();

        if(Load_UI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) LoadUI();
    }
}