using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject Load_UI;

    //메인화면 버튼---------------------------------------------------------------------

    public void LoadUI()
    {
        if(Load_UI.activeSelf == false)
            Load_UI.SetActive(true);
        else if(Load_UI.activeSelf == true)
            Load_UI.SetActive(false);
    }

    //새 게임---------------------------------
    public void StartButton_Fade()
    {
        StartCoroutine(FadeOut());
        Invoke("tutorial_start", 1f);
    }

    void tutorial_start()
    {
        SceneManager.LoadScene("GameScene");
    }

    //설정-------------------------------------
    public void SettingButton()
    {
        SettingUI.SetActive(true);
    }
    
    //나가기------------------------------------
    public void ExitButton()
    {
        Application.Quit();
    }

    //-------------------------------------------------------------------------------

    //페이드 인, 아웃
    public Image fadeImage;
    public float fadeDuration = 1f;

    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / fadeDuration);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / fadeDuration);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    //-------------------------------------------------------------------------------

    //메인화면 효과
    public Collider2D flag;
    public Transform flag_pos;
    public GameObject TeddyBear;
    public GameObject TeddyBear_yellow;
    public GameObject TeddyBear_pink;

    public Collider2D CameraObject;
    public ParticleSystem CameraEffect;

    void MainObjectClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider == flag)
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log("Clicked on: " + clickedObject.name);
                
                int Rand = Random.Range(1,4);

                switch(Rand)
                {
                    case 1:
                        Instantiate(TeddyBear, new Vector2(Random.Range(2.8f, 7.5f),flag_pos.position.y), flag.transform.rotation);
                        break;
                    case 2:
                        Instantiate(TeddyBear_yellow, new Vector2(Random.Range(2.8f, 7.5f),flag_pos.position.y), flag.transform.rotation);
                        break;
                    case 3:
                        Instantiate(TeddyBear_pink, new Vector2(Random.Range(2.8f, 7.5f),flag_pos.position.y), flag.transform.rotation);
                        break;
                }                    
            }

            if (hit.collider != null && hit.collider == CameraObject)
            {
                GameObject clickedObject = hit.collider.gameObject;
                CameraEffect.Play();
            }
        }
    }

    //-------------------------------------------------------------------------------

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        MainObjectClick();
    }
}