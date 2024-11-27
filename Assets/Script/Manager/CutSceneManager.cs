using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    private PlayerControl playerControl;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;
    private MainCamera mainCamera;
    private TutorialManager tutorialManager;
    private MiniGame miniGame;
    private Inventory inventory;

    public GameObject Effect;
    public GameObject Blocking;
    public GameObject NPC;

    public GameObject Stage1;
    public GameObject BlockedWay;

    public GameObject VibrationEvent;

    public GameObject BigTeddyBearBos;
    public Animator BigTeddyBearBosAnimation;

    public GameObject BlackBackground;

    public bool isCutScene = false;

    public IEnumerator TutorialCutScene()
    {
        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut());
        playerControl.transform.position = new Vector3(-1.5f, -1.5f, 0);
        mainCamera.transform.position = new Vector3(-1.5f, -1.5f, -10);
        miniGame.ClearPhotoMode();
        GameManager.GameState = "Æ©Åä¸®¾ó ÄÆ¾À";
        yield return StartCoroutine(fadeManager.FadeIn());
        playerControl.isMove = true;
        dialogueManager.ShowDialogue(dialogueContentManager.d_cutScene);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return StartCoroutine(fadeManager.ChangeStateFade("ÆÄÆ¼·ë"));
        dialogueManager.ShowDialogue(dialogueContentManager.d_calender);
        isCutScene = false;
    }

    public IEnumerator NpcCutScene()
    {
        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut());
        playerControl.transform.position = new Vector3(-49f, 27, 0);
        Destroy(NPCItem.Instance.gameObject);
        miniGame.ClearPhotoMode();
        NPC.SetActive(true);
        playerControl.isMove = false;
        inventory.Clean();
        yield return StartCoroutine(fadeManager.FadeIn());
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
    }

    public IEnumerator CutScene_Stage1()
    {
        yield return StartCoroutine(fadeManager.FadeOut());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn());
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
        Blocking.SetActive(true);
        BlockedWay.SetActive(false);
        Stage1.SetActive(true);
        VibrationEvent.SetActive(true);
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

    public IEnumerator Bos()
    {
        isCutScene = true;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos1);
        yield return StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;

        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos2);
        yield return StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;

        Vector2 targetPosition = new Vector2(playerControl.transform.position.x, BigTeddyBearBos.transform.position.y);
        float moveSpeed = 8f;
        BigTeddyBearBosAnimation.speed = 2f;
        BigTeddyBearBosAnimation.Play("BigTeddyBearMove");

        while ((Vector2)BigTeddyBearBos.transform.position != targetPosition)
        {
            BigTeddyBearBos.transform.position = Vector2.MoveTowards(
                BigTeddyBearBos.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        BigTeddyBearBosAnimation.Play("BigTeddyBearStop");
        BlackBackground.SetActive(true);
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
        inventory = FindFirstObjectByType<Inventory>();
    }
}