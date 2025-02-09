using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosEvent : MonoBehaviour
{
    private CutSceneManager cutSceneManager;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(cutSceneManager.CutScene_3());
        }
    }

    private void Start()
    {
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
    }
}
