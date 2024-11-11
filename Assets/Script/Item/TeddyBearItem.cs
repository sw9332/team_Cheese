using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBearItem : MonoBehaviour
{
    public bool isInstalled = false;
    Vector3 Scale;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if(other.CompareTag("Chair"))
        {
            switch(other.transform.localScale.x)
            {
                case -1:
                    Scale = transform.localScale;
                    Scale.x = -1;
                    transform.localScale = Scale;
                    break;

                case 1:
                    Scale = transform.localScale;
                    Scale.x = 1;
                    transform.localScale = Scale;
                    break;
            }
        }
    }
}