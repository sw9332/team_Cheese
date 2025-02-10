using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerControl playerControl;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    public bool isChangingSprite = false;

    public IEnumerator ChangeToDamaged(float value)
    {
        isChangingSprite = true;
        Hp.Instance.HpDecrease(value);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.05f);
        }

        spriteRenderer.color = Color.white;
        isChangingSprite = false;
    }

    void DamageControl()
    {
        if (playerControl.isMove && Damage.Instance.isChangingSprite)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerControl.Direction = "Up";
                playerControl.animator.Play("PlayerDamagedUp");

                PlayerControl.MoveX = false;
                PlayerControl.MoveY = true;

                transform.Translate(Vector3.up * PlayerControl.speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerControl.Direction = "Down";
                playerControl.animator.Play("PlayerDamagedDown");

                PlayerControl.MoveX = false;
                PlayerControl.MoveY = true;

                transform.Translate(Vector3.down * PlayerControl.speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerControl.Direction = "Left";
                playerControl.animator.Play("PlayerDamagedLeft");

                PlayerControl.MoveX = true;
                PlayerControl.MoveY = false;

                transform.Translate(Vector3.left * PlayerControl.speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerControl.Direction = "Right";
                playerControl.animator.Play("PlayerDamagedRight");

                PlayerControl.MoveX = true;
                PlayerControl.MoveY = false;

                transform.Translate(Vector3.right * PlayerControl.speed * Time.deltaTime);
            }
        }
    }

    private static Damage instance = null;

    public static Damage Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(instance);
    }

    void Update()
    {
        DamageControl();
    }

    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        gameManager = FindFirstObjectByType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}