using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaBar;
    public static bool isPlayerRunning;

  
    void staminaChage()
    {
            if (isPlayerRunning== true)
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
        staminaBar.value -= Time.deltaTime;
    }
    void staminaUp()
    {
        staminaBar.value += 0.25f * Time.deltaTime ;
    }

    void Start()
    {
        staminaBar.value = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        staminaChage(); 
    }
}
