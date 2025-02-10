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
    public Camera MainCamera;

    [Header("Blocking")]
    public GameObject Blocking_1;
    public GameObject Blocking_2;

    [Header("Event")]
    public GameObject VibrationEvent;
    public GameObject NPC_Boss_Event1;
    public GameObject NPC_Boss_Event2;
    public GameObject NPC_Boss_Event3;

    [Header("Animation")]
    public Animator rabbit;

    [Header("Chapter 1 Save")]
    public GameObject Chapter1_Save1;
    public GameObject Chapter1_Save2;

    [Header("Check")]
    public bool Move = true;
    public bool isCutScene = false;
    public bool isCutScene4 = false;

    public void ChangePosition(GameObject changeObject, float x, float y, float z)
    {
        changeObject.transform.position = new Vector3(x, y, z);
    }

    public IEnumerator WaitForDialogue()
    {
        while (dialogueManager.dialogue_continue) yield return null;
    }

    public IEnumerator MoveObject(GameObject Object, float x, float y, float speed)
    {
        Vector2 targetPosition = new Vector2(x, y);

        while ((Vector2)Object.transform.position != targetPosition)
        {
            Object.transform.position = Vector2.MoveTowards(Object.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator Prologue()
    {
        GameManager.GameState = "∆©≈‰∏ÆæÛ";
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;

        fadeManager.fadeImage.gameObject.SetActive(true);

        dialogueManager.ShowDialogue(dialogueContentManager.d_prologue);
        while (dialogueManager.count < 3) yield return null;
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));

        yield return StartCoroutine(WaitForDialogue());
        tutorialManager.TutorialType(1);
        tutorialManager.TutorialUI.SetActive(true);
        
        uiManager.InGameUI.SetActive(true);
        albumManager.album = false;
        isCutScene = false;
    }

    public IEnumerator Tutorial()
    {
        while (!inventoryManager.miniGameCamera) yield return null;
        yield return StartCoroutine(WaitForDialogue());

        uiManager.InGameUI.SetActive(false);     
        isCutScene = true;

        albumManager.album = true;
        E_Key.SetActive(true);

        while (!albumManager.Album.activeSelf) yield return null;
        while (albumManager.Album.activeSelf) yield return null;

        uiManager.InGameUI.SetActive(true);
        albumManager.album = false;

        dialogueManager.ShowDialogue(dialogueContentManager.d_album2);
        StartCoroutine(WaitForDialogue());
        isCutScene = false;
    }

    public IEnumerator CutScene_1()
    {
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        miniGame.ClearPhotoMode();

        Effect.SetActive(true);
        textManager.ShowDateText("XX.10.10", 2f);
        ChangePosition(playerControl.gameObject, -1.5f, -1.5f, 0);
        ChangePosition(mainCamera.gameObject, -1.5f, -1.5f, -10);
        GameManager.GameState = "∆©≈‰∏ÆæÛ ƒ∆æ¿";
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        playerControl.isMove = true;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_1);
        yield return StartCoroutine(WaitForDialogue());
        Effect.SetActive(false);
        yield return StartCoroutine(fadeManager.ChangeStateFade("∆ƒ∆º∑Î"));
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_2);
        yield return StartCoroutine(WaitForDialogue());
        albumManager.album = true;
        E_Key.SetActive(true);
        while (E_Key.activeSelf) yield return null;
        while (albumManager.Album.activeSelf) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_album1);
        yield return StartCoroutine(WaitForDialogue());

        uiManager.InGameUI.SetActive(true);
        albumManager.album = true;
        isCutScene = false;
    }

    public IEnumerator CutScene_2()
    {
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        uiManager.InGameUI.SetActive(true);
        albumManager.album = false;
        isCutScene = false;
        Move = false;
        miniGame.ClearPhotoMode();

        Effect.SetActive(true);
        ChangePosition(playerControl.gameObject, -49f, 29.5f, 0);
        npcItem = FindFirstObjectByType<NPCItem>();
        npcItem.ItemRemove();
        playerControl.isMove = false;
        playerControl.StopDirection("Up");
        NPC.SetActive(true);
        textManager.ShowDateText("XX.10.01", 2f);
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));

        // NPC HP : 5
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 4
        while (npcEnemy.HP > 4) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_2);
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 3
        while (npcEnemy.HP > 3) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_3);
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP : 2
        while (npcEnemy.HP > 2) yield return null;
        GameManager.GameState = "CutScene2";
        npcEnemy.CtrlKey.SetActive(false);

        while (GameManager.GameState != "CutScene2") yield return null;

        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        Effect.SetActive(false);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_1);
        yield return StartCoroutine(WaitForDialogue());
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_2);
        yield return StartCoroutine(WaitForDialogue());
        Blocking_1.SetActive(true);
        stage1_BlockedWay.is_open = true;
        VibrationEvent.SetActive(true);

        uiManager.InGameUI.SetActive(true);
        albumManager.album = true;
        isCutScene = false;
        Move = true;
    }

    public IEnumerator CutScene_3()
    {
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        Move = false;

        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos1);
        StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        yield return StartCoroutine(WaitForDialogue());
        yield return new WaitForSeconds(0.1f);

        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos2);
        StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        yield return StartCoroutine(WaitForDialogue());
        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(MoveObject(BigTeddyBearBos, playerControl.transform.position.x, BigTeddyBearBos.transform.position.y, 15f));
        BigTeddyBearBos.transform.position = new Vector2(-45f, -59f);
        BlackBackground.gameObject.SetActive(true);
        StartCoroutine(CutScene_4());
    }

    public IEnumerator CutScene_4()
    {
        GameManager.GameState = "CutScene4";
        UIManager.is_CutScene_4 = true;
        uiManager.InGameUI.SetActive(true);
        albumManager.album = false;
        isCutScene = false;
        yield return new WaitForSeconds(3);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_1);
        yield return StartCoroutine(WaitForDialogue());
        yield return new WaitForSeconds(2);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_2);
        yield return StartCoroutine(WaitForDialogue());
        yield return new WaitForSeconds(2);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_3);
        yield return StartCoroutine(WaitForDialogue());
        isCutScene4 = true;
    }

    public IEnumerator CutScene_5()
    {
        GameManager.GameState = "CutScene5";
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        miniGame.ClearPhotoMode();

        NPC.SetActive(false);
        WhiteBackground.gameObject.SetActive(true);
        playerControl.StopDirection("Up");
        ChangePosition(playerControl.gameObject, -49f, 22.5f, 0);
        ChangePosition(npc.gameObject, -49f, 26.5f, 0);
        Effect.SetActive(false);
        BlackBackground.gameObject.SetActive(false);
        yield return StartCoroutine(fadeManager.FadeIn(WhiteBackground, Color.white, false));
        WhiteBackground.color = Color.white;

        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_1);
        dialogueManager.is_ChoiceExpected = true;
        yield return StartCoroutine(WaitForDialogue());
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_2);
        dialogueManager.ShowChoiceDialogue(true, "≥≠¿Ô¿Ã", "¿Œ«¸");
        yield return StartCoroutine(WaitForDialogue());

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
        
        npc.Hp.gameObject.SetActive(true);
        Blocking_2.SetActive(true);

        uiManager.InGameUI.SetActive(true);
        albumManager.album = false;
        isCutScene = false;
        Move = true;

        StartCoroutine(npc.Boss_Pattern());
    }

    public IEnumerator CutScene_6()
    {
        GameManager.GameState = "CutScene6";
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        Move = false;
        
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

        uiManager.InGameUI.SetActive(true);
        albumManager.album = false;
        isCutScene = false;
        Move = true;

        while (!npcEnemy.event2) yield return null;
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(true);

        ChangePosition(playerControl.gameObject, -49f, 29f, 0);
        playerControl.StopDirection("Up");

        // NPC HP2 : 4
        while (npcEnemy.HP2 > 4) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_3);
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : 3
        while (npcEnemy.HP2 > 3) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_4);
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(true);

        // NPC HP2 : 2
        while (npcEnemy.HP2 > 2) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_6_5);
        yield return StartCoroutine(WaitForDialogue());
        npcEnemy.CtrlKey.SetActive(false);
        UIManager.stage1 = true;
    }

    public IEnumerator CutScene_7()
    {
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        miniGame.ClearPhotoMode();

        yield return new WaitForSeconds(1f);
        PlayerImage.gameObject.SetActive(true);
        yield return StartCoroutine(fadeManager.FadeOut(PlayerImage, Color.white));
        yield return new WaitForSeconds(2f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_7_1);
        yield return StartCoroutine(WaitForDialogue());
        yield return StartCoroutine(fadeManager.FadeIn(PlayerImage, Color.white, false));
        fadeManager.fadeImage.gameObject.SetActive(false);
        WhiteBackground.gameObject.SetActive(true);
        WhiteBackground.color = Color.white;
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_7_2);
        yield return StartCoroutine(WaitForDialogue());
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        WhiteBackground.gameObject.SetActive(false);
        ChangePosition(playerControl.gameObject, -49.5f, -107f, 0);
        playerControl.StopDirection("lying down");
        yield return new WaitForSeconds(1f);
        StartCoroutine(CutScene_8());
    }

    public IEnumerator CutScene_8()
    {
        GameManager.GameState = "Chapter 2";
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        n_Player.isFollow = true;

        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_1);
        yield return StartCoroutine(WaitForDialogue());
        yield return new WaitForSeconds(1f);
        playerControl.StopDirection("Wake Up");
        yield return new WaitForSeconds(1.3f);
        playerControl.StopDirection("Left");
        yield return new WaitForSeconds(0.5f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_2);
        yield return StartCoroutine(WaitForDialogue());
        yield return new WaitForSeconds(1f);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_3);
        yield return StartCoroutine(WaitForDialogue());
        albumManager.album = true;
        E_Key.SetActive(true);
        while (E_Key.activeSelf) yield return null;
        while (albumManager.Album.activeSelf) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_8_4);
        yield return StartCoroutine(WaitForDialogue());

        uiManager.InGameUI.SetActive(true);
        albumManager.album = true;
        isCutScene = false;
    }

    public IEnumerator CutScene_9()
    {
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        GameManager.GameState = "CutScene 9";
        uiManager.InGameUI.SetActive(false);
        albumManager.album = false;
        isCutScene = true;
        miniGame.ClearPhotoMode();

<<<<<<< HEAD
=======
        ChangePosition(TinSoldier.Instance.gameObject, 0.6f, -243.8f, 0);
        TinSoldier.Instance.gameObject.SetActive(true);
        rabbit.Play("rabbit");
>>>>>>> 62490b494e8cb7dac4fe007f5d7baf09911fa9b4
        MainCamera.orthographicSize = 4;
        Effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        yield return new WaitForSeconds(2f);

        TinSoldier.Instance.Move("Right");
        yield return StartCoroutine(MoveObject(TinSoldier.Instance.gameObject, 7.2f, TinSoldier.Instance.transform.position.y, TinSoldier.Instance.speed));

        TinSoldier.Instance.Stop("Right");
        yield return new WaitForSeconds(1f);

        TinSoldier.Instance.Move("Right Attack");
        yield return new WaitForSeconds(1f);
        rabbit.Play("rabbit transformation");

        TinSoldier.Instance.Stop("Right");
        yield return new WaitForSeconds(1.3f);

        TinSoldier.Instance.Move("Right");
        yield return StartCoroutine(MoveObject(TinSoldier.Instance.gameObject, 17.16f, TinSoldier.Instance.transform.position.y, TinSoldier.Instance.speed));

        TinSoldier.Instance.gameObject.SetActive(false);
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));

        GameManager.GameState = "Chapter 2 ø¨»∏¿Â";
        uiManager.InGameUI.SetActive(true);
        MainCamera.orthographicSize = 6;
        Effect.SetActive(false);

        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));

        albumManager.album = true;
        isCutScene = false;
    }

    public IEnumerator isVibrationEvent()
    {
        isCutScene = true;
        yield return StartCoroutine(mainCamera.VibrationEffect(1, 0.1f));
        yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_3);
        yield return StartCoroutine(WaitForDialogue());
        isCutScene = false;
        uiManager.InGameUI.SetActive(true);
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