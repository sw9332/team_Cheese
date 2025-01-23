using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cart Position") && gameObject.name == "Cart 1")
        {
            Debug.Log("Cart");
            UIManager.cart = true;
        }
    }
}