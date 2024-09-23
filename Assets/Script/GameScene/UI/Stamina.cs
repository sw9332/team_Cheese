using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider playerStaminaBar;
    public static bool isPlayerRunning;
    private PlayerControl playerControl;

    void staminaChage()
    {
            if (isPlayerRunning== true && playerControl.isPush != true)
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
        playerControl = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        staminaChage(); 
    }
}
