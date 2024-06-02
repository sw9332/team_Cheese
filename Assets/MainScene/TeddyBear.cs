using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y <= -8f)
        {
            Destroy(gameObject);
        } 
    }
}
