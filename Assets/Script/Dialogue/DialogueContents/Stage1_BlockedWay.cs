using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_BlockedWay : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Dialogue gnome;

    public bool is_open = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && dialogueManager.is_talking == false)
        {
            dialogueManager.ShowDialogue(gnome);
        }
    }

    void Update()
    {
        if (is_open)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Awake()
    {
        is_open = false;
    }
}
