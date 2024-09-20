using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Gnome : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue gnome;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            dialogueManager.ShowDialogue(gnome);
            this.gameObject.SetActive(false);
        }
    }
}
