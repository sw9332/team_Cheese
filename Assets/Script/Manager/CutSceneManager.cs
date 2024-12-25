using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    private PlayerControl playerControl;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;
    private MainCamera mainCamera;
    private TutorialManager tutorialManager;
    private MiniGame miniGame;
    private InventoryManager inventoryManager;
    private UIManager uiManager;
    private NPCEnemy npcEnemy;
    private NPC npc;
    private Stage1_BlockedWay stage1_BlockedWay;

    [Header("Effect")]
    public GameObject Effect;
    public GameObject BlackBackground;
    public GameObject WhiteBackground;
    public Text BosUI;

    [Header("Object")]
    public GameObject NPC;
    public GameObject BigTeddyBearBos;

    [Header("Blocking")]
    public GameObject Blocking_1;
    public GameObject Blocking_2;

    [Header("Event")]
    public GameObject VibrationEvent;

    [Header("Animation")]
    public Animator BigTeddyBearBosAnimation1;
    public Animator BigTeddyBearBosAnimation2;
    public Animator BigTeddyBearBosAnimation3;

    [Header("Check")]
    public bool Move = true;
    public bool isCutScene = false;
    public bool isCutScene4 = false;

    public IEnumerator CutScene_1()
    {
        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        playerControl.transform.position = new Vector3(-1.5f, -1.5f, 0);
        mainCamera.transform.position = new Vector3(-1.5f, -1.5f, -10);
        miniGame.ClearPhotoMode();
        GameManager.GameState = "Æ©Åä¸®¾ó ÄÆ¾À";
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));
        playerControl.isMove = true;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_1);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return StartCoroutine(fadeManager.ChangeStateFade("ÆÄÆ¼·ë"));
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_2);
        isCutScene = false;
    }

    public IEnumerator CutScene_2()
    {
        Move = false;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        playerControl.transform.position = new Vector3(-49f, 29.5f, 0);
        Destroy(NPCItem.Instance.gameObject);
        miniGame.ClearPhotoMode();
        NPC.SetActive(true);
        playerControl.isMove = false;
        playerControl.Direction = "Up";
        inventoryManager.Clean();
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
        while (GameManager.GameState != "CutScene2") yield return null;

        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));
        yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_1);
        while (dialogueManager.dialogue_continue) yield return null;
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_2);
        while (dialogueManager.dialogue_continue) yield return null;
        Blocking_1.SetActive(true);
        stage1_BlockedWay.is_open = true;
        VibrationEvent.SetActive(true);
        isCutScene = false;
        Move = true;
    }

    public IEnumerator CutScene_3()
    {
        isCutScene = true;
        Move = false;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos1);
        yield return StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;

        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos2);
        yield return StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;

        Vector2 targetPosition = new Vector2(playerControl.transform.position.x, BigTeddyBearBos.transform.position.y);
        float moveSpeed = 13f;
        BigTeddyBearBosAnimation1.speed = 2f;
        BigTeddyBearBosAnimation1.Play("BigTeddyBearMove");
        BigTeddyBearBosAnimation2.speed = 2f;
        BigTeddyBearBosAnimation2.Play("BigTeddyBearMove2");
        BigTeddyBearBosAnimation3.speed = 2f;
        BigTeddyBearBosAnimation3.Play("BigTeddyBearMove3");

        while ((Vector2)BigTeddyBearBos.transform.position != targetPosition)
        {
            BigTeddyBearBos.transform.position = Vector2.MoveTowards(
                BigTeddyBearBos.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        BigTeddyBearBosAnimation1.Play("BigTeddyBearStop");
        BigTeddyBearBosAnimation2.Play("BigTeddyBearStop");
        BigTeddyBearBosAnimation3.Play("BigTeddyBearStop");
        BlackBackground.SetActive(true);
        StartCoroutine(CutScene_4());
    }

    public IEnumerator CutScene_4()
    {
        GameManager.GameState = "CutScene4";
        UIManager.is_CutScene_4 = true;
        yield return new WaitForSeconds(3);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_1);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(2);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_2);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(2);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_3);
        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene4 = true;
    }

    public IEnumerator CutScene_5()
    {
        miniGame.ClearPhotoMode();
        GameManager.GameState = "CutScene5";
        WhiteBackground.SetActive(true);
        playerControl.Direction = "Up";
        playerControl.transform.position = new Vector3(-49f, 22.5f, 0);
        npc.transform.position = new Vector3(-49f, 26.5f, 0);
        yield return new WaitForSeconds(2);
        Effect.SetActive(false);
        BlackBackground.SetActive(false);
        UnityEngine.UI.Image image = WhiteBackground.GetComponent<UnityEngine.UI.Image>();
        yield return StartCoroutine(FadeOutImage(image, 1f));
        yield return new WaitForSeconds(1);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_1);
        while (dialogueManager.dialogue_continue) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_2);
        dialogueManager.ChoiceButton(true, "³­ÀïÀÌ", "ÀÎÇü");
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(false);
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(false);
        StartCoroutine(mainCamera.VibrationEffect(4, 0.1f));
        npc.Transformation(true);
        yield return new WaitForSeconds(2f);
        BosUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        BosUI.gameObject.SetActive(false);
        yield return null;
        isCutScene = false;
        npc.Hp.gameObject.SetActive(true);
        Blocking_2.SetActive(true);
        Move = true;
        StartCoroutine(npc.Boss_Pattern());
    }

    public IEnumerator isVibrationEvent()
    {
        isCutScene = true;
        yield return StartCoroutine(mainCamera.VibrationEffect(1, 0.1f));
        yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_3);
        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene = false;
    }

    IEnumerator FadeOutImage(UnityEngine.UI.Image image, float duration)
    {
        Color color = image.color;
        float startAlpha = color.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, t / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 0f);
        image.gameObject.SetActive(false);
    }


    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        mainCamera = FindFirstObjectByType<MainCamera>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        miniGame = FindFirstObjectByType<MiniGame>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        uiManager = FindFirstObjectByType<UIManager>();
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        npc = FindFirstObjectByType<NPC>();
        stage1_BlockedWay = FindFirstObjectByType<Stage1_BlockedWay>();
    }
}