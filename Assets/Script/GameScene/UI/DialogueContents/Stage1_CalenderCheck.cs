using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_CalenderCheck : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private Stage1_Calender Dcalender;

    public Dialogue Check;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        Dcalender= transform.GetComponentInParent<Stage1_Calender>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            dialogueManager.ShowDialogue(Check);
        }
    }

    private void Update()
    {
        if(Dcalender.is_checked == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
