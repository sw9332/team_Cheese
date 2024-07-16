using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_being_pushed : MonoBehaviour
{
    public Rigidbody2D rb;
    void Update()
    {
        if (Player.Velocity == 5)
            rb.constraints = RigidbodyConstraints2D.None;
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
