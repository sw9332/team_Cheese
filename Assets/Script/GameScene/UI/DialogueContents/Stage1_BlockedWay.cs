using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_BlockedWay : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue gnome;

    public bool is_open = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Awake()
    {
        is_open = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && Input.GetKey(KeyCode.Z) && dialogueManager.is_talking == false)
        {
            dialogueManager.ShowDialogue(gnome);
        }
    }

    private void Update()
    {
        if (is_open)
        {
            this.gameObject.SetActive(false);
        }
    }
}
