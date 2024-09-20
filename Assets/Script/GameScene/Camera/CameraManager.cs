using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public static Vector3 Camera_Pos;

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
            if (player.position.y > 46.5f && player.position.y < 50.9f)
            {
                Camera_Pos.y = player.position.y + offset.y;
            }
        }

        else if (GameManager.GameState == "Tutorial Cut Scene")
        {
            //케이크를 찍을 시 컷씬 카메라
            Camera_Pos.x = -51.8f;
            Camera_Pos.y = 47f;
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
