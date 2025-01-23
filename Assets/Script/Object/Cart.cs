using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cart Position") && gameObject.name == "Cart 1") UIManager.cart = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cart Position") && gameObject.name == "Cart 1") UIManager.cart = false;
    }
}