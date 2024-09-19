using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public static bool is_take_photo;
    public bool is_minigame = false;
    public GameObject mainCamera;
    public GameObject photoCamera;
    public Slider x_Axis;
    public Slider y_Axis;
    public GameObject minigamePanel;
    public GameObject ingameUIPanel;
    public GameObject player;
    public Image fadeImage;
    public float fadeDuration = 1f;
    // E키 카메라앨범 이미지에 어떤 이미지가 들어가야 할 지 판단하는 변수들
    public static bool isImageChange = false;

    //private bool is_next_stage = false;
    //private bool is_transition = false;

   
    void Update()
    {
        PhotoMode();
        TakePhoto();
    }

    private void PhotoMode()
    {
        if (is_take_photo == true && Input.GetKeyDown(KeyCode.P))
        {
            player.SetActive(false);
            mainCamera.GetComponent<Camera>().enabled = false;
            photoCamera.GetComponent<Camera>().enabled = true;
            ingameUIPanel.SetActive(false);
            minigamePanel.SetActive(true);
            is_minigame = true;
        }
    }

    private void TakePhoto()
    {
        if(GameManager.GameState == "Tutorial")
        {
            if (is_minigame == true)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    photoCamera.transform.Translate(-1f * 1.5f * Time.deltaTime, 0f, 0f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    photoCamera.transform.Translate(1f * 1.5f * Time.deltaTime, 0f, 0f);
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    photoCamera.transform.Translate(0f, 1f * 1.5f * Time.deltaTime, 0f);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    photoCamera.transform.Translate(0f, -1f * 1.5f * Time.deltaTime, 0f);
                }

                float ClampX = Mathf.Clamp(photoCamera.transform.GetComponent<Transform>().position.x, -78.2f, -76.2f);
                float ClampY = Mathf.Clamp(photoCamera.transform.GetComponent<Transform>().position.y, 48f, 50f);

                photoCamera.transform.GetComponent<Transform>().position = new Vector3(ClampX, ClampY, -1f);

                x_Axis.GetComponent<Slider>().value = photoCamera.transform.position.x;
                y_Axis.GetComponent<Slider>().value = photoCamera.transform.position.y;

                if (Input.GetKey(KeyCode.F)
                    && x_Axis.GetComponent<Slider>().value <= -77f && x_Axis.GetComponent<Slider>().value >= -77.4f
                    && y_Axis.GetComponent<Slider>().value <= 49.2f && y_Axis.GetComponent<Slider>().value >= 48.8f)
                {
                    //is_next_stage = true;
                    UIManager.Camera_setactive = false;
                    is_take_photo = false;
                    is_minigame = false;
                    GameManager.GameState = "InGame";
                    isImageChange = true;
                    StartCoroutine(TransitionToNextStage());
                }
            }

            //if (is_next_stage && !is_transition)
            //{
                
            //}
        }
    }

    IEnumerator TransitionToNextStage()
    {
        //is_transition = true;
        yield return StartCoroutine(FadeOut());

        player.transform.position = new Vector3(60, 0, 0);
        mainCamera.transform.position = new Vector3(60, 0, -10);
        player.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = true;
        photoCamera.GetComponent<Camera>().enabled = false;
        ingameUIPanel.SetActive(true);
        minigamePanel.SetActive(false);
        is_minigame = false;
        ///is_next_stage = false;

        yield return StartCoroutine(FadeIn());
        //is_transition = false;
    }

    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        float timer = 0f;
        while (timer < fadeDuration)
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
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / fadeDuration);
            yield return null;
        }

        fadeImage.color = Color.black;
    }
}
