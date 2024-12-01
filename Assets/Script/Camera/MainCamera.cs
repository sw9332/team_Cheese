using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            case "∆©≈‰∏ÆæÛ":
                pos.x = -77f;
                pos.y = Mathf.Clamp(player.transform.position.y, 47.5f, 49f);
                break;

            case "∆©≈‰∏ÆæÛ ƒ∆æ¿":
                pos.x = -51.9f;
                pos.y = 47.5f;
                break;

            case "∆ƒ∆º∑Î":
                pos.x = -1.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, -1f, 1f);
                break;

            case "∫πµµ #F":
                if (player.transform.position.y >= -7.8f)
                {
                    pos.x = -17.5f;
                    pos.y = player.transform.position.y;
                }
                    
                else
                {
                    pos.x = Mathf.Clamp(player.transform.position.x, -45.5f, 0f);
                    pos.y = -13.15f;
                }
                break;

            case "ø¨»∏¿Â ¿‘±∏":
                pos.x = -17.5f;
                pos.y = Mathf.Clamp(player.transform.position.y, 7.5f, 18.8f);
                break;

            case "ø¨»∏¿Â":
                pos.x = Mathf.Clamp(player.transform.position.x, 4f, 12f);
                pos.y = Mathf.Clamp(player.transform.position.y, 16f, 18.7f);
                break;

            case "√¢∞Ì ¿‘±∏":
                pos.x = -49.44f;
                pos.y = Mathf.Clamp(player.transform.position.y, -1.3f, 7f);
                break;

            case "√¢∞Ì":
                pos.x = Mathf.Clamp(player.transform.position.x, -50f, -49f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19f, 28f);
                break;

            case "∫∏Ω∫":
                pos.x = player.transform.position.x;
                pos.y = Mathf.Clamp(player.transform.position.y, -60f, -58.3f);
                break;

            case "CutScene2":
                pos.x = Mathf.Clamp(player.transform.position.x, -50f, -49f);
                pos.y = Mathf.Clamp(player.transform.position.y, 19f, 28f);
                break;

            default:
                pos.x = player.transform.position.x;
                pos.y = player.transform.position.y;
                break;
        }

        if (isGameStateChanged || GameManager.GameState == "∆©≈‰∏ÆæÛ ƒ∆æ¿") transform.position = pos;
        else transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 0.15f);
        lastGameState = GameManager.GameState;
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerControl = FindFirstObjectByType<PlayerControl>();
    }
}