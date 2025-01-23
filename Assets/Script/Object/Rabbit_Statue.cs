using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Statue : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rabbit Statue Position"))
        {
            Debug.Log("Rabbit Statue");
            UIManager.rabbit_Statue = true;
        }
    }
}