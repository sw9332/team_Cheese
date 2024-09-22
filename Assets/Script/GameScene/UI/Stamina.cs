using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider playerStaminaBar;
    public static bool isPlayerRunning;

    void staminaChage()
    {
            if (isPlayerRunning== true && PlayerControl.is_Push != true)
            {
                staminaDown();
            }

            else
            {
                staminaUp();
            }
    }
    void staminaDown()
    {
        playerStaminaBar.value -= Time.deltaTime;
    }
    void staminaUp()
    {
        playerStaminaBar.value += 0.25f * Time.deltaTime ;
    }

    void Start()
    {
        playerStaminaBar.value = 1f;
    }

    void Update()
    {
        staminaChage(); 
    }
}
