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

    // EŰ ī�޶�ٹ� �̹����� � �̹����� ���� �� �� �Ǵ��ϴ� ������
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
            if (Input.GetKey(KeyCode.UpArrow)) photoCamera.transform.Translate(0f, 1f * 1.5f * Time.deltaTime, 0f);
            if (Input.GetKey(KeyCode.DownArrow)) photoCamera.transform.Translate(0f, -1f * 1.5f * Time.deltaTime, 0f);
            if (Input.GetKey(KeyCode.LeftArrow)) photoCamera.transform.Translate(-1f * 1.5f * Time.deltaTime, 0f, 0f);
            if (Input.GetKey(KeyCode.RightArrow)) photoCamera.transform.Translate(1f * 1.5f * Time.deltaTime, 0f, 0f);

            switch (GameManager.GameState)
            {
                case "Ʃ�丮��": ControlPhotoMode(-78.2f, -76.2f, 48f, 50f);
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

                case "â��": ControlPhotoMode(-49.5f, -48.5f, 31f, 32f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value <= -48.86f && x_Axis.value >= -48.95f
                        && y_Axis.value <= 31.3f && y_Axis.value >= 31.16f)
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

                case "CutScene4": ControlPhotoMode(-49.5f, -48.5f, 29f, 30f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value <= -48.8f && x_Axis.value >= -49.2f
                        && y_Axis.value <= 29.3f && y_Axis.value >= 29.2f)
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

                case "CutScene6":
                    ControlPhotoMode(-49.5f, -48.5f, 31f, 32f);
                    if (Input.GetKey(KeyCode.Tab)
                        && x_Axis.value <= -48.86f && x_Axis.value >= -48.95f
                        && y_Axis.value <= 31.3f && y_Axis.value >= 31.16f)
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