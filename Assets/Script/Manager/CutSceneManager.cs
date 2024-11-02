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

    public GameObject NPC;

    public IEnumerator TutorialCutScene()
    {
        yield return StartCoroutine(fadeManager.FadeOut());
        playerControl.transform.position = new Vector3(60, 0, 0);
        mainCamera.transform.position = new Vector3(60, 0, -10);
        miniGame.ClearPhotoMode();
        GameManager.GameState = "Æ©Åä¸®¾ó ÄÆ¾À";
        yield return StartCoroutine(fadeManager.FadeIn());
        playerControl.isMove = true;
        dialogueManager.ShowDialogue(dialogueContentManager.d_cutScene);
        while (dialogueManager.dialogue_continue) yield return null;
        StartCoroutine(fadeManager.ChangeStateFade("ÆÄÆ¼·ë"));
    }

    public IEnumerator NpcCutScene()
    {
        yield return StartCoroutine(fadeManager.FadeOut());
        playerControl.transform.position = new Vector3(12.5f, 28, 0);
        Destroy(NPCItem.Instance.gameObject);
        miniGame.ClearPhotoMode();
        NPC.SetActive(true);
        playerControl.isMove = false;
        inventory.Clean();
        tutorialManager.TutorialType(6);
        yield return StartCoroutine(fadeManager.FadeIn());
        yield return StartCoroutine(tutorialManager.ShowTutorialUI(true, dialogueContentManager.d_Demo_1));
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