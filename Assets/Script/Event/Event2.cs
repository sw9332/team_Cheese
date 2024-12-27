using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private Event1 event1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (event1.triggerEvent1)
            {
                case true: dialogueManager.ShowDialogue(dialogueContentManager.d_event2); break;
                case false: dialogueManager.ShowDialogue(dialogueContentManager.d_event3); event1.triggerEvent1 = true; event1.gameObject.SetActive(false); break;
            }
            
        }
    }

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        event1 = FindFirstObjectByType<Event1>();
    }
}
