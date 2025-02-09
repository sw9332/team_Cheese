using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_ToStage2Room : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue inRoom;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            dialogueManager.ShowDialogue(inRoom);
            this.gameObject.SetActive(false);
        }
    }
}
