using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Player player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "튜토리얼":
                pos.x = -77f;
                if (player.transform.position.y >= 47.5f && player.transform.position.y <= 49f) pos.y = player.transform.position.y;
                break;

            case "튜토리얼 컷씬":
                pos.x = -51.9f;
                pos.y = 47.5f;
                break;

            case "파티룸":
                pos.x = 60f;
                if (player.transform.position.y >= 0f && player.transform.position.y <= 1.3f) pos.y = player.transform.position.y;
                break;

            case "복도 #F":
                if (player.transform.position.y >= -7.5f)
                {
                    pos.x = 44f;
                    pos.y = player.transform.position.y;
                }
                    
                else
                {
                    pos.x = player.transform.position.x;
                    pos.y = -12.5f;
                }
                break;

            case "연회장 입구":
                pos.x = 44f;
                pos.y = player.transform.position.y;
                break;

            case "연회장":
                bool posX = (player.transform.position.x >= 58.3f && player.transform.position.x <= 73.8f);
                bool posY = (player.transform.position.y <= 21f && player.transform.position.y >= 16f);
                if (posX) pos.x = player.transform.position.x;
                if (posY) pos.y = player.transform.position.y;
                if (!posX && !posY) pos = transform.position;
                break;

            case "창고 입구":
                pos.x = 12f;
                pos.y = player.transform.position.y;
                break;

            case "창고":
                pos.y = player.transform.position.y;
                break;

            default:
                pos.x = player.transform.position.x;
                pos.y = player.transform.position.y;
                break;
        }

        transform.position = pos;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
}