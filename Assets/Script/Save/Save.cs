using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private SaveManager saveManager;

    public bool trigger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !trigger)
        {
            trigger = true;
            saveManager.Save();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
        }
    }

    void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
    }
}