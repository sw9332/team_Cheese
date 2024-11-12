using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationEvent : MonoBehaviour
{
    private MainCamera mainCamera;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(mainCamera.VibrationEffect(3f, 0.1f));
        }
    }

    void Start()
    {
        mainCamera = FindFirstObjectByType<MainCamera>();
    }
}
