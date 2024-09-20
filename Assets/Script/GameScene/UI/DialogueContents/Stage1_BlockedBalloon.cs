using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_BlockedBalloon : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue blockedBalloon;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && Input.GetKeyDown(KeyCode.Z) && dialogueManager.is_talking == false)
        {
            dialogueManager.ShowDialogue(blockedBalloon);
        }
    }
}
