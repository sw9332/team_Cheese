using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private SaveManager saveManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) saveManager.Save();
    }

    void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
    }
}