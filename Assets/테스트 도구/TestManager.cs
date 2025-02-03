using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    private Player player;
    private DialogueManager dialogueManager;
    private InventoryManager inventoryManager;
    private UIManager uiManager;
    private FadeManager fadeManager;

    private Image image;
    private Animator animator;

    public InputField Player_Position_Input;
    public InputField Dialogue_Speed_Input;

    public Button TestButton;
    public Button Camera_ON_OFF_Button;

    public Text CameraText;

    public void Player_Position()
    {
        switch(Player_Position_Input.text)
        {
            case "����": player.transform.position = new Vector3(-5f, -13f, 0); break;
            case "��ȸ�� �Ա�": player.transform.position = new Vector3(-17f, 6f, 0); break;
            case "��ȸ��": player.transform.position = new Vector3(-3f, 18f, 0); break;
            case "â��": player.transform.position = new Vector3(-49f, 17f, 0); break;
            case "â�� �Ա�": player.transform.position = new Vector3(-49f, -3f, 0); break;
            case "���� �Ա�": player.transform.position = new Vector3(-63f, -48f, 0); break;
        }

        GameManager.GameState = Player_Position_Input.text;
    }

    public void Dialogue_Speed()
    {
        string Input = Dialogue_Speed_Input.text;
        if (float.TryParse(Input, out float delay)) dialogueManager.delay = delay;
    }

    public void Camera_ON_OFF()
    {
        if (inventoryManager.Camera == false)
        {
            CameraText.text = "ON";
            inventoryManager.Camera = true;
        }
            
        else
        {
            CameraText.text = "OFF";
            inventoryManager.Camera = false;
        }
    }

    public void Test_UI_True()
    {
        animator.SetBool("UI", true);
        TestButton.interactable = false;
        StartCoroutine(fadeManager.FadeOut(image, Color.gray));
    }

    public void Test_UI_False()
    {
        animator.SetBool("UI", false);
        TestButton.interactable = true;
        StartCoroutine(fadeManager.FadeIn(image, Color.gray, true));
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        uiManager = FindFirstObjectByType<UIManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();

        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        Player_Position_Input.text = "Ʃ�丮��";
        Dialogue_Speed_Input.text = dialogueManager.delay.ToString();
    }
}