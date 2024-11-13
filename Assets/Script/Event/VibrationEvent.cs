using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationEvent : MonoBehaviour
{
    private MainCamera mainCamera;
    private CutSceneManager cutSceneManager;

    private bool isChack = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isChack)
        {
            isChack = true;
            StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        }
    }

    void Start()
    {
        mainCamera = FindFirstObjectByType<MainCamera>();
        cutSceneManager = FindFirstObjectByType<CutSceneManager>();
    }
}
