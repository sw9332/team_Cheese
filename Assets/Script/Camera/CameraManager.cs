using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "∆©≈‰∏ÆæÛ":
                if(player.position.y >= 47 && player.position.y <= 50)
                    pos.y = player.position.y;
                break;

            case "∆©≈‰∏ÆæÛ ƒ∆æ¿":
                pos.x = -51.9f;
                pos.y = 47f;
                break;

            case "∆ƒ∆º∑Î":
                pos.x = 60f;
                pos.y = player.position.y;
                break;

            case "∫πµµ #F":
                if (player.position.y >= -7.5f)
                {
                    pos.x = player.position.x;
                    pos.y = player.position.y;
                }
                    
                else
                {
                    pos.x = player.position.x;
                    pos.y = -12.5f;
                }
                break;

            case "ø¨»∏¿Â ¿‘±∏":
                pos.x = 44f;
                pos.y = player.position.y;
                break;

            case "√¢∞Ì":
                pos.y = player.position.y;
                break;

            default:
                pos.x = player.position.x;
                pos.y = player.position.y;
                break;
        }

        transform.position = pos;
    }
}