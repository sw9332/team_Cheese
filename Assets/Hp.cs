using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] hpObject;
    public Animator[] animator;

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
        for (int i = 0; i < hpObject.Length; i++)
        {
            hpObject[i].SetActive(i < hpValue);
        }
    }

    public void HpPlus(float value)
    {
        if (hpValue < hpObject.Length) hpValue += value;
        HpUpdate();
    }

    public void HpDecrease(float value)
    {
        if (hpValue > 0)
        {
            hpValue -= value;

            for (int i = 0; i < hpObject.Length; i++)
            {
                switch (animator[i].name)
                {
                    case "Hp 1": animator[1].Play("hp Left"); break;
                    case "Hp 2": animator[2].Play("hp Right"); break;
                    case "Hp 3": animator[3].Play("hp Left"); break;
                    case "Hp 4": animator[4].Play("hp Right"); break;
                    case "Hp 5": animator[5].Play("hp Left"); break;
                    case "Hp 6": animator[6].Play("hp Right"); break;
                    case "Hp 7": animator[7].Play("hp Left"); break;
                    case "Hp 8": animator[8].Play("hp Right"); break;
                    case "Hp 9": animator[9].Play("hp Left"); break;
                    case "Hp 10": animator[10].Play("hp Right"); break;
                }
            }
        }

        HpUpdate();
    }

    void Update()
    {
        if (hpValue < 1)
        {
            GameManager.GameEnd = true;
            StartCoroutine(gameManager.GameOver());
        }
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        HpUpdate();
    }
}