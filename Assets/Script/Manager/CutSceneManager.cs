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
    private MiniGame miniGame;
    private InventoryManager inventoryManager;
    private UIManager uiManager;
    private NPCEnemy npcEnemy;
    private NPC npc;
    private Stage1_BlockedWay stage1_BlockedWay;
    private AlbumManager albumManager;
    private GameManager gameManager;

    [Header("Image")]
    public Image BlackBackground;
    public Image WhiteBackground;
    public Image PlayerImage;

    [Header("Effect")]
    public GameObject Effect;
    public Text BosUI;

    [Header("Object")]
    public GameObject E_Key;
    public GameObject NPC;
    public GameObject BigTeddyBearBos;

    [Header("Blocking")]
    public GameObject Blocking_1;
    public GameObject Blocking_2;

    [Header("Event")]
    public GameObject VibrationEvent;
    public GameObject NPC_Boss_Event1;
    public GameObject NPC_Boss_Event2;
    public GameObject NPC_Boss_Event3;

    [Header("Animation")]
    public Animator BigTeddyBearBosAnimation1;
    public Animator BigTeddyBearBosAnimation2;
    public Animator BigTeddyBearBosAnimation3;

    [Header("Check")]
    public bool Move = true;
    public bool isCutScene = false;
    public bool isCutScene4 = false;

    public IEnumerator Tutorial()
    {
        while (!inventoryManager.Camera) yield return null;
        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene = true;
        E_Key.SetActive(true);
        while (!albumManager.Album.activeSelf) yield return null;
        while (albumManager.Album.activeSelf) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_album2);
        isCutScene = false;
    }

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
        while (dialogueManager.dialogue_continue) yield return null;
        E_Key.SetActive(true);
        while (E_Key.activeSelf) yield return null;
        while (albumManager.Album.activeSelf) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_album1);
        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene = false;
    }

    public IEnumerator CutScene_2()
    {
        Move = false;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        playerControl.transform.position = new Vector3(-49f, 29.5f, 0);
        Destroy(NPCItem.Instance.gameObject);
        miniGame.ClearPhotoMode();
        playerControl.isMove = false;
        playerControl.Direction = "Up";
        NPC.SetActive(true);
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));

        // NPC HP : 5
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 3
        while (npcEnemy.HP > 3) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_2);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 1
        while (npcEnemy.HP > 1) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_3);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : -1
        while (npcEnemy.HP > -1) yield return null;
        GameManager.GameState = "CutScene2";
        npcEnemy.CtrlKey.SetActive(false);

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
        BlackBackground.gameObject.SetActive(true);
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
        NPC.SetActive(false);
        miniGame.ClearPhotoMode();
        GameManager.GameState = "CutScene5";
        WhiteBackground.gameObject.SetActive(true);
        playerControl.Direction = "Up";
        playerControl.transform.position = new Vector3(-49f, 22.5f, 0);
        npc.transform.position = new Vector3(-49f, 26.5f, 0);
        Effect.SetActive(false);
        BlackBackground.gameObject.SetActive(false);
        yield return StartCoroutine(fadeManager.FadeIn(WhiteBackground, Color.white));
        WhiteBackground.color = Color.white;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_1);
        while (dialogueManager.dialogue_continue) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_2);
        dialogueManager.ChoiceButton(true, "³­ÀïÀÌ", "ÀÎÇü");
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(false);
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

    public IEnumerator CutScene_6()
    {
        GameManager.GameState = "CutScene6";
        isCutScene = true;
        BlackBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.gameObject.SetActive(false);
        NPC.gameObject.transform.position = npc.transform.position;
        npc.transform.position = new Vector3(-68, 26.5f, 0);
        NPC.SetActive(true);
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        NPC_Boss_Event1.SetActive(true);
        NPC_Boss_Event2.SetActive(true);
        NPC_Boss_Event3.SetActive(true);
        isCutScene = false;

        while (!npcEnemy.event2) yield return null;
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : 3
        while (npcEnemy.HP2 > 3) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_3);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : 1
        while (npcEnemy.HP2 > 1) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_4);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : -1
        while (npcEnemy.HP2 > -1) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_5);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(false);
        GameManager.Demo = true;
    }

    public IEnumerator CutScene_7()
    {
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        yield return new WaitForSeconds(1f);
        PlayerImage.gameObject.SetActive(true);
        yield return StartCoroutine(fadeManager.FadeOut(PlayerImage, Color.white));
        yield return new WaitForSeconds(2f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_7_1);
        while (dialogueManager.dialogue_continue) yield return null;
        fadeManager.fadeImage.gameObject.SetActive(false);
        PlayerImage.gameObject.SetActive(false);
        WhiteBackground.gameObject.SetActive(true);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_7_2);
        while (dialogueManager.dialogue_continue) yield return null;
        StartCoroutine(gameManager.DemoClear());
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && E_Key.activeSelf) E_Key.SetActive(false);
    }
    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        mainCamera = FindFirstObjectByType<MainCamera>();
        miniGame = FindFirstObjectByType<MiniGame>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        uiManager = FindFirstObjectByType<UIManager>();
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        npc = FindFirstObjectByType<NPC>();
        stage1_BlockedWay = FindFirstObjectByType<Stage1_BlockedWay>();
        albumManager = FindFirstObjectByType<AlbumManager>();
        gameManager = FindFirstObjectByType<GameManager>();

        StartCoroutine(Tutorial());
    }
}