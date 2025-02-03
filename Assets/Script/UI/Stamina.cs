using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider playerStaminaBar;
    public bool isPlayerRunning;

    void Start()
    {
        playerStaminaBar.value = 1f;
    }

    void Update()
    {
        if (isPlayerRunning && !PlayerControl.isPush) playerStaminaBar.value -= 0.5f * Time.deltaTime;
        else playerStaminaBar.value += 0.25f * Time.deltaTime;
    }
}