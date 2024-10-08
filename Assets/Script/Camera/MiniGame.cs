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
    
    // E키 카메라앨범 이미지에 어떤 이미지가 들어가야 할 지 판단하는 변수들
    public static bool isImageChange = false;

    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;

    //private bool is_next_stage = false;
    //private bool is_transition = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        fadeManager = FindObjectOfType<FadeManager>();
    }

    private void Update()
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
        //GameState가 Tutorial일 때.
        if (GameManager.GameState == "Tutorial")
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
                    && x_Axis.GetComponent<Slider>().value <= -76.9f && x_Axis.GetComponent<Slider>().value >= -77.1f
                    && y_Axis.GetComponent<Slider>().value <= 48.7f && y_Axis.GetComponent<Slider>().value >= 48.35f)
                {
                    //is_next_stage = true;
                    UIManager.is_cake = false;
                    UIManager.is_bear = false;
                    UIManager.tutorialTrigger = false;
                    is_take_photo = false;
                    is_minigame = false;
                    GameManager.GameState = "Tutorial Cut Scene";
                    isImageChange = true;
                    StartCoroutine(NextStage());
                    Invoke("CutSceneText", 1.5f);
                }
            }

            //if (is_next_stage && !is_transition)
            //{

            //}
        }

        //GameState가 Stage1일 때.
        else if (GameManager.GameState == "Stage1")
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

                float ClampX = Mathf.Clamp(photoCamera.transform.GetComponent<Transform>().position.x, 11.8f, 12.8f);
                float ClampY = Mathf.Clamp(photoCamera.transform.GetComponent<Transform>().position.y, 29.5f, 30.5f);

                photoCamera.transform.GetComponent<Transform>().position = new Vector3(ClampX, ClampY, -1f);

                x_Axis.GetComponent<Slider>().value = photoCamera.transform.position.x;
                y_Axis.GetComponent<Slider>().value = photoCamera.transform.position.y;
                Debug.Log("Slider X: " + x_Axis.value + " | Slider Y: " + y_Axis.value);

                if (Input.GetKey(KeyCode.F)
                    && x_Axis.GetComponent<Slider>().value <= -12.4f && x_Axis.GetComponent<Slider>().value >= -12.6f
                    && y_Axis.GetComponent<Slider>().value <= 29.9f && y_Axis.GetComponent<Slider>().value >= 30.1f)
                {
                    is_take_photo = false;
                    is_minigame = false;
                    isImageChange = true;
                }
            }
        }
    }

    void CutSceneText()
    {
        dialogueManager.ShowDialogue(dialogueContentManager.d_cutScene);
    }

    IEnumerator NextStage()
    {
        yield return StartCoroutine(fadeManager.FadeOut());
        //is_transition = true;
        player.transform.position = new Vector3(60, 0, 0);
        mainCamera.transform.position = new Vector3(60, 0, -10);
        player.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = true;
        photoCamera.GetComponent<Camera>().enabled = false;
        ingameUIPanel.SetActive(true);
        minigamePanel.SetActive(false);
        is_minigame = false;
        //is_transition = false;
        yield return StartCoroutine(fadeManager.FadeIn());
    }
}
