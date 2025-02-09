using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject[] hpObject;
    public float hpValue = 3;

    private static Hp value = null;

    public static Hp Instance
    {
        get
        {
            return value;
        }
    }

    void Awake()
    {
        if (value == null)
            value = this;
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
        if (hpValue < 5) hpValue += value;
        HpUpdate();
    }

    public void HpDecrease(float value)
    {
        if (hpValue > 0) hpValue -= value;
        HpUpdate();
    }

    void Start()
    {
        HpUpdate();
    }
}