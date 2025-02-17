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
    private UIManager uiManager;
    private InventoryManager inventoryManager;

    private float ClampX;
    private float ClampY;

    public float SliderSpeed = 1.5f;

    private void PhotoMode()
    {
        if (is_take_photo && Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.GameState == "CutScene4" && !cutSceneManager.isCutScene4) return;

            playerControl.gameObject.SetActive(false);
            mainCamera.GetComponent<Camera>().enabled = false;
            photoCamera.GetComponent<Camera>().enabled = true;
            uiManager.InGameUI.SetActive(false);
            minigamePanel.SetActive(true);
            is_minigame = true;
        }
    }

    public void ClearPhotoMode()
    {
        playerControl.gameObject.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = true;
        photoCamera.GetComponent<Camera>().enabled = false;
        minigamePanel.SetActive(false);
        is_minigame = false;
        StartRandom();
    }

    private void MiniGamePosition(float x_minValue, float x_maxValue, float y_minValue, float y_maxValue)
    {
        x_Axis.minValue = x_minValue;
        x_Axis.maxValue = x_maxValue;
        y_Axis.minValue = y_minValue;
        y_Axis.maxValue = y_maxValue;
    }

    private void UpdatePosition()
    {
        photoCamera.transform.position = new Vector3(x_Axis.value, y_Axis.value, -1f);
    }

    private void StartRandom()
    {
        x_Axis.value = Random.Range(x_Axis.minValue, x_Axis.maxValue);
        y_Axis.value = Random.Range(y_Axis.minValue, y_Axis.maxValue);
    }

    private void SliderControl(string direction)
    {
        switch (direction)
        {
            case "Up": y_Axis.value += SliderSpeed * Time.deltaTime; break;
            case "Down": y_Axis.value -= SliderSpeed * Time.deltaTime; break;
            case "Left": x_Axis.value -= SliderSpeed * Time.deltaTime; break;
            case "Right": x_Axis.value += SliderSpeed * Time.deltaTime; break;
        }
    }

    private void TakePhoto()
    {
        if (is_minigame)
        {
            if (Input.GetKey(KeyCode.UpArrow)) SliderControl("Up");
            if (Input.GetKey(KeyCode.DownArrow)) SliderControl("Down");
            if (Input.GetKey(KeyCode.LeftArrow)) SliderControl("Left");
            if (Input.GetKey(KeyCode.RightArrow)) SliderControl("Right");

            switch (GameManager.GameState)
            {
                case "튜토리얼": MiniGamePosition(-78.2f, -76.2f, 48f, 50f);
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

                case "창고": MiniGamePosition(-50.5f, -48f, 32f, 33.5f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= -49.15f && x_Axis.value <= -48.8f
                        && y_Axis.value >= 32.3f && y_Axis.value <= 32.5f)
                    {
                        UIManager.is_NPC = false;
                        is_take_photo = false;
                        is_minigame = false;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_2());
                    }
                    break;

                case "CutScene4": MiniGamePosition(-50.5f, -48f, 32f, 33.5f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= -49.15f && x_Axis.value <= -48.8f
                        && y_Axis.value >= 32.3f && y_Axis.value <= 32.5f)
                    {
                        UIManager.is_CutScene_4 = false;
                        is_take_photo = false;
                        is_minigame = false;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_5());
                    }
                    break;

                case "CutScene6": MiniGamePosition(-50.5f, -48f, 32f, 33.5f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= -49.15f && x_Axis.value <= -48.8f
                        && y_Axis.value >= 32.3f && y_Axis.value <= 32.5f)
                    {
                        UIManager.stage1 = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_7());
                    }
                    break;

                case "Chapter 2 연회장": MiniGamePosition(8f, 10f, -116f, -114f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value >= 9.08f && x_Axis.value <= 9.3f
                        && y_Axis.value >= -115.65f && y_Axis.value <= -115.3f)
                    {
                        UIManager.rabbit_Statue = false;
                        UIManager.cart = false;
                        UIManager.chicken = false;
                        UIManager.flower = false;
                        is_take_photo = false;
                        is_minigame = false;
                        isImageChange = true;
                        playerControl.isMove = false;
                        inventoryManager.Clean();
                        StartCoroutine(cutSceneManager.CutScene_9());
                    }
                    break;
            }
        }
    }

    void Update()
    {
        PhotoMode();
        TakePhoto();
        UpdatePosition();
    }

    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
        mainCamera = FindFirstObjectByType<MainCamera>();
        photoCamera = FindFirstObjectByType<MiniGame>();
        uiManager = FindFirstObjectByType<UIManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();

        StartRandom();
    }
}