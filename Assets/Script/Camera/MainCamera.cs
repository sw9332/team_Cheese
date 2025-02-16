using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Player player;
    private PlayerControl playerControl;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;
    private string lastGameState;
    private bool isShaking = false;

    public IEnumerator VibrationEffect(float duration, float magnitude)
    {
        playerControl.isMove = false;
        isShaking = true;
        Vector3 originalPos = transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
        playerControl.isMove = true;
        isShaking = false;
    }

    void Update()
    {
        if (isShaking) return;

        pos = transform.position;
        bool isGameStateChanged = GameManager.GameState != lastGameState;

        switch (GameManager.GameState)
        {
            case "튜토리얼":
                pos.x = -77f;
                pos.y = 48.2f;
                break;

            case "튜토리얼 컷씬":
                pos.x = -51.9f;
                pos.y = 48.2f;
                break;

            case "파티룸":
                pos.x = -1.5f;
                pos.y = 0;
                break;

            case "복도":
                if (player.transform.position.y >= -7.8f)
                {
                    pos.x = -17.5f;
                    pos.y = Mathf.Clamp(player.transform.position.y, player.transform.position.y, -5.49f);
                }
                    
                else
                {
                    pos.x = Mathf.Clamp(player.transform.position.x, -42.99f, -0.15f);
                    pos.y = -13.3f;
                }
                break;

            case "연회장 입구":
                pos.x = -17.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, 8.8f, 17.5f);
                break;

            case "연회장":
                pos.x = Mathf.Clamp(player.transform.position.x, 7.02f, 10.05f);
                pos.y = Mathf.Clamp(player.transform.position.y, 17.15f, 17.506f);
                break;

            case "창고 입구":
                pos.x = -49.44f;
                pos.y = Mathf.Clamp(player.transform.position.y, -0.1f, 5.6f);
                break;

            case "창고":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19.8f, 29.5f);
                break;

            case "Stage1":
                pos.x = -63.45f;
                pos.y = Mathf.Clamp(player.transform.position.y, -45.2f, -33.5f);
                break;

            case "보스":
                pos.x = player.transform.position.x;
                pos.y = -59.2f;
                break;

            case "CutScene2":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19.8f, 29.5f);
                break;

            case "CutScene5":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19.8f, 29.5f);
                break;

            case "CutScene6":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19.8f, 29.5f);
                break;

            case "CutScene7":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19.8f, 29.5f);
                break;

            case "Chapter 2":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, -112.2f, -102.5f);
                break;

            case "Chapter 2 복도":
                if (player.transform.position.y >= -140)
                {
                    pos.x = -17.5f;
                    pos.y = Mathf.Clamp(player.transform.position.y, player.transform.position.y, -137.5f);
                }

                else if (player.transform.position.y <= -152)
                {
                    pos.x = Mathf.Clamp(player.transform.position.x, -11.98f, -1.7f);
                    pos.y = Mathf.Clamp(player.transform.position.y, -165.3f, -156.2f);
                }

                else
                {
                    pos.x = Mathf.Clamp(player.transform.position.x, -42.99f, 1.05f);
                    pos.y = -145.5f;
                }

                break;

            case "Chapter 2 연회장 입구":
                pos.x = -17.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -123.2f, -114.5f);
                break;

            case "Chapter 2 연회장":
                pos.x = Mathf.Clamp(player.transform.position.x, 7.02f, 10.05f);
                pos.y = Mathf.Clamp(player.transform.position.y, -114.8f, -114.5f);
                break;

            case "Chapter 2 창고 입구":
                pos.x = -49.44f;
                pos.y = Mathf.Clamp(player.transform.position.y, -132.2f, -126.5f);
                break;

            case "Chapter 2 창고":
                pos.x = Mathf.Clamp(player.transform.position.x, -52.99f, -45.98f);
                pos.y = Mathf.Clamp(player.transform.position.y, -114.81f, -102.5f);
                break;

            case "CutScene 9":
                pos.x = 9f;
                pos.y = -244f;
                break;

            case "CutScene 10":
                pos.x = Mathf.Clamp(player.transform.position.x, 7.02f, 10.05f);
                pos.y = Mathf.Clamp(player.transform.position.y, -245.8f, -245.5f);
                break;

            case "CutScene 10_1":
                pos.x = -17.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -254.3f, -245.5f);
                break;

            case "CutScene 10_2":
                pos.x = -17.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -254.3f, -245.5f);
                break;

            case "Chapter 2 하단 복도":
                pos.x = -36.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -192.82f, -182.5f);
                break;

            case "Chapter 2 Enemy Room":
                pos.x = -40;
                pos.y = Mathf.Clamp(player.transform.position.y, -168.2f, -163.5f);
                break;

            case "Chapter 2 왼쪽 하단 복도":
                pos.x = -63.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -177.2f, -165.5f);
                break;

            case "Chapter 2 CCTV Room":
                pos.x = -12.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -194.83f, -186.483f);
                break;

            case "Chapter 2 왼쪽 하단":
                pos.x = Mathf.Clamp(player.transform.position.x, -68.99f, -61.95f);
                pos.y = -191.2f;
                break;

            case "Chapter 2 왼쪽 하단 Enemy Room":
                pos.x = -79.75f;
                pos.y = Mathf.Clamp(player.transform.position.y, -175.2f, -173.5f);
                break;

            case "Event":
                pos.x = transform.position.x;
                pos.y = transform.position.y;
                break;

            default:
                pos.x = player.transform.position.x;
                pos.y = player.transform.position.y;
                break;
        }

        if (isGameStateChanged || GameManager.GameState == "튜토리얼 컷씬") transform.position = pos;
        else transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 0.15f);
        lastGameState = GameManager.GameState;
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerControl = FindFirstObjectByType<PlayerControl>();
    }
}