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
        animator[animator.Length].Play("Hp Left Stop");
        animator[animator.Length].Play("Hp Right Stop");
        HpUpdate();
    }

    public void HpDecrease(float value)
    {
        if (hpValue > 0)
        {
            int number = Mathf.Max(0, Mathf.Min((int)hpValue - 1, animator.Length - 1));
            string name = (hpValue % 2 == 0) ? "Hp Right" : "Hp Left";
            animator[number].Play(name);

            hpValue -= value;
            StartCoroutine(WaitForAnimation());
        }
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        HpUpdate();
    }

    void Update()
    {
        if (hpValue < 1)
        {
            GameManager.GameEnd = true;
            StartCoroutine(gameManager.GameOver());
        }

        if (Input.GetKeyDown(KeyCode.D)) HpDecrease(1.0f);
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        HpUpdate();
    }
}