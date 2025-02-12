using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] hpObject;
    public Animator[] animator;

    public float hpValue = 6;

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

    void HpStart()
    {
        for (int i = 0; i < hpObject.Length; i++)
        {
            if (i >= hpValue) hpObject[i].SetActive(false);
        }
    }

    void HpUpdate()
    {
        for (int i = 0; i < hpObject.Length; i++)
        {
            if (i < hpValue)
            {
                if (i < hpValue) hpObject[i].SetActive(true);
                if (hpObject[i].name == "Hp Left") animator[i].Play("Hp Left Stop");
                else if (hpObject[i].name == "Hp Right") animator[i].Play("Hp Right Stop");
            }

            else
            {
                if (!hpObject[i].activeSelf) return;
                if (hpObject[i].name == "Hp Left") animator[i].Play("Hp Left");
                else if (hpObject[i].name == "Hp Right") animator[i].Play("Hp Right");
            }
        }
    }

    public void HpPlus(float value)
    {
        if (hpValue < hpObject.Length) hpValue += value;
        if(hpValue > hpObject.Length) hpValue = hpObject.Length;
        HpUpdate();
    }

    public void HpDecrease(float value)
    {
        if (hpValue > 0)
        {
            hpValue -= value;
            HpUpdate();
        }
    }

    void Update()
    {
        if (hpValue < 1)
        {
            GameManager.GameEnd = true;
            StartCoroutine(gameManager.GameOver());
        }

        if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Damage.Instance.ChangeToDamaged(1.0f));
        if (Input.GetKeyDown(KeyCode.F)) HpPlus(2.0f);
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        HpStart();
    }
}