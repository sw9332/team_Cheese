using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject[] hpObject;
    public float hpValue = 5;

    private static GameObject value = null;

    public static GameObject Instance
    {
        get
        {
            return value;
        }
    }

    void Awake()
    {
        if (value == null)
            value = this.gameObject;
        else
            Destroy(value);
    }

    void HpUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            hpObject[i].SetActive(i < hpValue);
        }
    }

    public void HpPlus(float value)
    {
        hpValue += value;
        HpUpdate();
    }

    public void HpDecrease(float value)
    {
        hpValue -= value;
        HpUpdate();
    }
}