using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinSoldier : MonoBehaviour
{
    public float speed = 1;

    private Animator animator;

    public void Move(string direction)
    {
        animator.Play(direction);

        switch (direction)
        {
            case "Up": transform.Translate(Vector2.up * speed * Time.deltaTime); break;
            case "Down": transform.Translate(Vector2.down * speed * Time.deltaTime); break;
            case "Left": transform.Translate(Vector2.left * speed * Time.deltaTime); break;
            case "Right": transform.Translate(Vector2.right * speed * Time.deltaTime); break;
        }
    }

    public void Stop(string direction)
    {
        animator.Play(direction+" Stop");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}