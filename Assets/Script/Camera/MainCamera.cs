using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Player player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;
    private string lastGameState;

    void Update()
    {
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
    }
}