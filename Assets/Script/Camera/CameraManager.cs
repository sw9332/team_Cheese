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

        if(GameManager.GameState == "Tutorial")
        {
            //튜토리얼 진행일 때 카메라
            if (player.position.y > 46.5f && player.position.y < 50.9f)
            {
                pos.y = player.position.y + offset.y;
            }
        }

        else if (GameManager.GameState == "Tutorial Cut Scene")
        {
            //케이크를 찍을 시 컷씬 카메라
            pos.x = -51.9f;
            pos.y = 47f;
        }

        else if (GameManager.GameState == "Stage1")
        {
            //Stage1 시 카메라
            pos.x = player.position.x + offset.x;
            pos.y = player.position.y + offset.y;
        }

        else if (GameManager.GameState == "Demo")
        {
            //Demo 시 카메라
            pos.x = player.position.x + offset.x;
            pos.y = player.position.y + offset.y;
        }

        transform.position = pos;
    }
}
