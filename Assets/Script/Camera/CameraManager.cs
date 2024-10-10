using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform player;

    public Vector3 pos;
    public Vector3 offset;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "Æ©Åä¸®¾ó":
                if (player.position.y > 46.5f && player.position.y < 50.9f)
                    pos.y = player.position.y + offset.y;
                break;

            case "Æ©Åä¸®¾ó ÄÆ¾À":
                pos.x = -51.9f;
                pos.y = 47f;
                break;

            case "Ã¢°í":
                pos.y = player.position.y + offset.y;
                break;

            default:
                pos.x = player.position.x + offset.x;
                pos.y = player.position.y + offset.y;
                break;
        }

        transform.position = pos;
    }
}
