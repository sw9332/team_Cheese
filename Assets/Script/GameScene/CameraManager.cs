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

        if(GameManager.GameState == "Tutorial")
        {
            //튜토리얼 진행일 때 카메라
            if (player.position.y > -3.5f && player.position.y < 0f)
            {
                Camera_Pos.y = player.position.y + offset.y;
            }
        }

        else if (GameManager.GameState == "InGame")
        {
            //인게임 시 카메라
            Camera_Pos.x = player.position.x + offset.x;
            Camera_Pos.y = player.position.y + offset.y;
        }

        transform.position = Camera_Pos;
    }
}
