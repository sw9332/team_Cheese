using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject photoCamera;
    public Slider x_Axis;
    public Slider y_Axis;
    public GameObject minigamePanel;
    public GameObject ingameUIPanel;
    public GameObject player;

    public static bool is_take_photo;
    public bool is_next_stage;
    public static bool is_minigame;

    // Start is called before the first frame update
    void Start()
    {
        is_take_photo = false;
    }

    // Update is called once per frame
    void Update()
    {
        PhotoMode();
        TakePhoto();
        GoNextStage();
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
        if(is_minigame == true)
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

            float ClampX = Mathf.Clamp(photoCamera.transform.GetComponent<Transform>().position.x, -1.2f, 1.2f);
            float ClampY = Mathf.Clamp(photoCamera.transform.GetComponent<Transform>().position.y, -1f, 1f);

            photoCamera.transform.GetComponent<Transform>().position = new Vector3(ClampX, ClampY, -1f);

            x_Axis.GetComponent<Slider>().value = photoCamera.transform.position.x;
            y_Axis.GetComponent<Slider>().value = photoCamera.transform.position.y;

            if(Input.GetKey(KeyCode.F)
                && x_Axis.GetComponent<Slider>().value <= 0.2f && x_Axis.GetComponent<Slider>().value >= -0.2f
                && y_Axis.GetComponent<Slider>().value <= 0.2f && y_Axis.GetComponent<Slider>().value >= -0.2f)
            {
                is_next_stage = true;
            }
        }
    }

    private void GoNextStage()
    {
        if(is_next_stage == true)
        {
            player.transform.position = new Vector3(60, 0, 0);
            mainCamera.transform.position = new Vector3(60, 0, -10);
            player.SetActive(true);
            mainCamera.GetComponent<Camera>().enabled = true;
            photoCamera.GetComponent<Camera>().enabled = false;
            ingameUIPanel.SetActive(true);
            minigamePanel.SetActive(false);
            is_minigame = false;
            is_next_stage = false;

            UIManager.Camera_setactive = false;
        }
    }
}
