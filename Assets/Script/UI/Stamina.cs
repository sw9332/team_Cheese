using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public bool isPlayerRunning;
    public float Hp = 6f;

    public GameObject[] stamina;

    void HpUpdate()
    {
        for (int i = 0; i < 6; i++)
        {
            stamina[i].SetActive(i < Hp);
        }
    }

    void HpDecrease()
    {
        if (Hp > -0.8)
        {
            Hp -= 1f * Time.deltaTime;
            HpUpdate();
        }
    }

    void HpPlus()
    {
        if (Hp < 6)
        {
            Hp += 1f * Time.deltaTime;
            HpUpdate();
        }
    }

    void Update()
    {
        if (isPlayerRunning && Input.GetKey(KeyCode.LeftShift)) HpDecrease();
        else if (!isPlayerRunning && !Input.GetKey(KeyCode.LeftShift)) HpPlus();
    }
}