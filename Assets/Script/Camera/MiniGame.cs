using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public static bool is_take_photo;
    public bool is_minigame = false;
    public Slider x_Axis;
    public Slider y_Axis;
    public GameObject minigamePanel;
    public GameObject ingameUIPanel;

    // E키 카메라앨범 이미지에 어떤 이미지가 들어가야 할 지 판단하는 변수들
    public static bool isImageChange = false;

    private PlayerControl playerControl;
    private CutSceneManager cutSceneManager;
    private CameraManager mainCamera;
    private MiniGame photoCamera;

    private float ClampX;
    private float ClampY;

    //private bool is_next_stage = false;
    //private bool is_transition = false;

    void PhotoMode()
    {
        if (is_take_photo && Input.GetKeyDown(KeyCode.P))
        {
            playerControl.gameObject.SetActive(false);
            mainCamera.GetComponent<Camera>().enabled = false;
            photoCamera.GetComponent<Camera>().enabled = true;
            ingameUIPanel.SetActive(false);
            minigamePanel.SetActive(true);
            is_minigame = true;
        }
    }

    public void ClearPhotoMode()
    {
        playerControl.gameObject.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = true;
        photoCamera.GetComponent<Camera>().enabled = false;
        ingameUIPanel.SetActive(true);
        minigamePanel.SetActive(false);
        is_minigame = false;
    }

    void PhotoModeControl(float x_minValue, float x_maxValue, float y_minValue, float y_maxValue)
    {
        x_Axis.minValue = x_minValue;
        x_Axis.maxValue = x_maxValue;

        y_Axis.minValue = y_minValue;
        y_Axis.maxValue = y_maxValue;

        ClampX = Mathf.Clamp(photoCamera.transform.position.x, x_minValue, x_maxValue);
        ClampY = Mathf.Clamp(photoCamera.transform.position.y, y_minValue, y_maxValue);

        photoCamera.transform.position = new Vector3(ClampX, ClampY, -1f);

        x_Axis.value = photoCamera.transform.position.x;
        y_Axis.value = photoCamera.transform.position.y;
    }

    void TakePhoto()
    {
        if (is_minigame)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) photoCamera.transform.Translate(-1f * 1.5f * Time.deltaTime, 0f, 0f);
            if (Input.GetKey(KeyCode.RightArrow)) photoCamera.transform.Translate(1f * 1.5f * Time.deltaTime, 0f, 0f);
            if (Input.GetKey(KeyCode.UpArrow)) photoCamera.transform.Translate(0f, 1f * 1.5f * Time.deltaTime, 0f);
            if (Input.GetKey(KeyCode.DownArrow)) photoCamera.transform.Translate(0f, -1f * 1.5f * Time.deltaTime, 0f);

            switch(GameManager.GameState)
            {
                case "튜토리얼": PhotoModeControl(-78.2f, -76.2f, 48f, 50f);
                    if (Input.GetKey(KeyCode.F)
                        && x_Axis.value <= -76.9f && x_Axis.value >= -77.1f
                        && y_Axis.value <= 48.7f && y_Axis.value >= 48.35f)
                    {
                        UIManager.is_cake = false;
                        UIManager.is_bear = false;
                        UIManager.tutorialTrigger = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        StartCoroutine(cutSceneManager.TutorialCutScene());
                    }
                    break;

                case "창고": PhotoModeControl(11.8f, 12.8f, 29.5f, 30.5f);
                    if (Input.GetKey(KeyCode.F)
                        && x_Axis.value >= 12.33f && x_Axis.value <= 12.44f
                        && y_Axis.value >= 29.65f && y_Axis.value <= 29.85f)
                    {
                        UIManager.is_NPC = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        StartCoroutine(cutSceneManager.NpcCutScene());
                    }
                    break;
            }
        }
    }

    void Update()
    {
        PhotoMode();
        TakePhoto();
    }

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        cutSceneManager = FindObjectOfType<CutSceneManager>();
        mainCamera = FindObjectOfType<CameraManager>();
        photoCamera = FindObjectOfType<MiniGame>();
    }
}