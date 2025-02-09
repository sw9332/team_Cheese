using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinSoldier : MonoBehaviour
{
    private static TinSoldier instance = null;

    public static TinSoldier Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);
    }

    public float speed = 3;

    private Animator animator;

    public void Move(string direction)
    {
        animator.Play(direction);
    }

    public void Stop(string direction)
    {
        animator.Play(direction+" Stop");
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        Stop("Left");
    }
}