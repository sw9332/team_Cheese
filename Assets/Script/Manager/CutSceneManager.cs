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
    private NPCItem npcItem;
    private N_Player n_Player;
    private Stage1_BlockedWay stage1_BlockedWay;
    private AlbumManager albumManager;
    private GameManager gameManager;
    private SaveManager saveManager;
    private TextManager textManager;
    private TutorialManager tutorialManager;

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

    [Header("Chapter 1 Save")]
    public GameObject Chapter1_Save1;
    public GameObject Chapter1_Save2;

    [Header("Check")]
    public bool Move = true;
    public bool isCutScene = false;
    public bool isCutScene4 = false;

    public IEnumerator Prologue()
    {
        GameManager.GameState = "∆©≈‰∏ÆæÛ";
        fadeManager.fadeImage.gameObject.SetActive(true);
        isCutScene = true;
        dialogueManager.ShowDialogue(dialogueContentManager.d_prologue);
        while (dialogueManager.count < 3) yield return null;
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));

        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene = false;
        tutorialManager.TutorialType(1);
        tutorialManager.TutorialUI.SetActive(true);
    }

    public IEnumerator Tutorial()
    {
        while (!inventoryManager.miniGameCamera) yield return null;
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
        Effect.SetActive(true);
        textManager.ShowDateText("XX.10.10", 3f);
        playerControl.transform.position = new Vector3(-1.5f, -1.5f, 0);
        mainCamera.transform.position = new Vector3(-1.5f, -1.5f, -10);
        miniGame.ClearPhotoMode();
        GameManager.GameState = "∆©≈‰∏ÆæÛ ƒ∆æ¿";
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        playerControl.isMove = true;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_1);
        while (dialogueManager.dialogue_continue) yield return null;
        Effect.SetActive(false);
        yield return StartCoroutine(fadeManager.ChangeStateFade("∆ƒ∆º∑Î"));
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
        Effect.SetActive(true);
        playerControl.transform.position = new Vector3(-49f, 29.5f, 0);
        npcItem = FindFirstObjectByType<NPCItem>();
        npcItem.ItemRemove();
        miniGame.ClearPhotoMode();
        playerControl.isMove = false;
        playerControl.Direction = "Up";
        NPC.SetActive(true);
        textManager.ShowDateText("XX.10.01", 3f);
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));

        // NPC HP : 5
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 4
        while (npcEnemy.HP > 4) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_2);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 3
        while (npcEnemy.HP > 3) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_3);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 2
        while (npcEnemy.HP > 2) yield return null;
        GameManager.GameState = "CutScene2";
        npcEnemy.CtrlKey.SetActive(false);

        while (GameManager.GameState != "CutScene2") yield return null;

        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        Effect.SetActive(false);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
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
        StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(0.1f);

        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos2);
        StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(0.1f);

        Vector2 targetPosition = new Vector2(playerControl.transform.position.x, BigTeddyBearBos.transform.position.y);
        float moveSpeed = 13f;

        while ((Vector2)BigTeddyBearBos.transform.position != targetPosition)
        {
            BigTeddyBearBos.transform.position = Vector2.MoveTowards(BigTeddyBearBos.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        BigTeddyBearBos.transform.position = new Vector2(-45f, -59f);
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
        yield return StartCoroutine(fadeManager.FadeIn(WhiteBackground, Color.white, false));
        WhiteBackground.color = Color.white;

        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_1);
        dialogueManager.is_ChoiceExpected = true;
        while (dialogueManager.dialogue_continue) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_2);
        dialogueManager.ShowChoiceDialogue(true, "≥≠¿Ô¿Ã", "¿Œ«¸");
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

        playerControl.transform.position = new Vector2(-49, 29f);
        playerControl.Direction = "Up";

        // NPC HP2 : 4
        while (npcEnemy.HP2 > 4) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_3);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : 3
        while (npcEnemy.HP2 > 3) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_4);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : 2
        while (npcEnemy.HP2 > 2) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_5);
        while (dialogueManager.dialogue_continue) yield return null;
        npcEnemy.CtrlKey.SetActive(false);
        UIManager.stage1 = true;
    }

    public IEnumerator CutScene_7()
    {
        GameManager.GameState = "Chapter 2";
        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        yield return new WaitForSeconds(1f);
        miniGame.ClearPhotoMode();
        PlayerImage.gameObject.SetActive(true);
        yield return StartCoroutine(fadeManager.FadeOut(PlayerImage, Color.white));
        yield return new WaitForSeconds(2f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_7_1);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return StartCoroutine(fadeManager.FadeIn(PlayerImage, Color.white, false));
        fadeManager.fadeImage.gameObject.SetActive(false);
        WhiteBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_7_2);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        WhiteBackground.gameObject.SetActive(false);
        playerControl.transform.position = new Vector2(-49.5f, -106.6f);
        playerControl.Direction = "Left";
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        StartCoroutine(CutScene_8());
    }

    public IEnumerator CutScene_8()
    {
        n_Player.isFollow = true;
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_1);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_2);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_3);
        while (dialogueManager.dialogue_continue) yield return null;
        E_Key.SetActive(true);
        while (E_Key.activeSelf) yield return null;
        while (albumManager.Album.activeSelf) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_4);
        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene = false;
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
        n_Player = FindFirstObjectByType<N_Player>();
        stage1_BlockedWay = FindFirstObjectByType<Stage1_BlockedWay>();
        albumManager = FindFirstObjectByType<AlbumManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        saveManager = FindFirstObjectByType<SaveManager>();
        textManager = FindFirstObjectByType<TextManager>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();

        switch (GameManager.Load)
        {
            case true: saveManager.Load(); break;
            case false: StartCoroutine(Prologue()); StartCoroutine(Tutorial()); break;
        }
    }
}