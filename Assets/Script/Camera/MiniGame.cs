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
    private MainCamera mainCamera;
    private MiniGame photoCamera;
    private InventoryManager inventoryManager;

    private float ClampX;
    private float ClampY;

    private void PhotoMode()
    {
        if (is_take_photo && Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.GameState == "CutScene4" && !cutSceneManager.isCutScene4) return;

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

    private void ControlPhotoMode(float x_minValue, float x_maxValue, float y_minValue, float y_maxValue)
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

    private void TakePhoto()
    {
        if (is_minigame)
        {
            if (Input.GetKey(KeyCode.UpArrow)) photoCamera.transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
            if (Input.GetKey(KeyCode.DownArrow)) photoCamera.transform.Translate(Vector2.down * 1.5f * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftArrow)) photoCamera.transform.Translate(Vector2.left * 1.5f * Time.deltaTime);
            if (Input.GetKey(KeyCode.RightArrow)) photoCamera.transform.Translate(Vector2.right * 1.5f * Time.deltaTime);

            switch (GameManager.GameState)
            {
                case "튜토리얼": ControlPhotoMode(-78.2f, -76.2f, 48f, 50f);
                    if (Input.GetKey(KeyCode.Tab)
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
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_1());
                    }
                    break;

                case "창고": ControlPhotoMode(-50.5f, -48f, 32f, 33.5f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= -49.15f && x_Axis.value <= -48.8f
                        && y_Axis.value >= 32.3f && y_Axis.value <= 32.5f)
                    {
                        UIManager.is_NPC = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_2());
                    }
                    break;

                case "CutScene4": ControlPhotoMode(-50.5f, -48f, 32f, 33.5f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= -49.15f && x_Axis.value <= -48.8f
                        && y_Axis.value >= 32.3f && y_Axis.value <= 32.5f)
                    {
                        UIManager.is_CutScene_4 = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_5());
                    }
                    break;

                case "CutScene6": ControlPhotoMode(-50.5f, -48f, 32f, 33.5f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= -49.15f && x_Axis.value <= -48.8f
                        && y_Axis.value >= 32.3f && y_Axis.value <= 32.5f)
                    {
                        UIManager.is_NPC = false;
                        GameManager.Demo = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_7());
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
        playerControl = FindFirstObjectByType<PlayerControl>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        mainCamera = FindFirstObjectByType<MainCamera>();
        photoCamera = FindFirstObjectByType<MiniGame>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }
}