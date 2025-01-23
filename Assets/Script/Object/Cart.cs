using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cart Position 1") && gameObject.name == "Cart 1")
        {
            Debug.Log("Cart 1");
            UIManager.cart1 = true;
        }

        if (other.CompareTag("Cart Position 2") && gameObject.name == "Cart 2")
        {
            Debug.Log("Cart 2");
            UIManager.cart2 = true;
        }
    }
}
