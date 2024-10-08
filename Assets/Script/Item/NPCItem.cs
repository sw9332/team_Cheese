using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Room D floor")) UIManager.is_NPC = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Room D floor")) UIManager.is_NPC = false;
    }
}