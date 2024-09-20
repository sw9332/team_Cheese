using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Calender : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue calender;

    public bool is_checked = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player" && Input.GetKeyDown(KeyCode.Z))
        {
            dialogueManager.ShowDialogue(calender);
            is_checked = true;
        }
    }
}

