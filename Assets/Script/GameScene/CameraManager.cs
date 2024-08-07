using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    Vector3 Camera_Pos;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        Camera_Pos = transform.position;

        if(GameManager.GameState == "tutorial")
        {
            if(player.position.x > -1f && player.position.x < 1f)
            {
                Camera_Pos.x = player.position.x + offset.x;
            }

            if(player.position.y > -1f && player.position.y < 1f)
            {
                Camera_Pos.y = player.position.y + offset.y;
            }
        }

        transform.position = Camera_Pos;
    }
}
