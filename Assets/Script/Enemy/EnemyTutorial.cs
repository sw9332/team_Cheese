using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    private TutorialManager tutorialManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.TutorialUI.SetActive(true);
            tutorialManager.TutorialType(6);
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        tutorialManager = FindFirstObjectByType<TutorialManager>();
    }
}