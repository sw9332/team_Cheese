using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Animator animator;

    public void BosMode(bool bosMode)
    {
        switch(bosMode)
        {
            case true: animator.Play("transformation"); break;
            case false: animator.Play("NPC"); break;
        }
    }

    void Start()
    {
        animator.Play("NPC");
    }
}