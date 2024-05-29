using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
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
        Vector3 Camera_Pos = transform.position;

        if(player.position.x > -4 && player.position.x < 4)
        {
            Camera_Pos.x = player.position.x + offset.x;
        }

        Camera_Pos.y = player.position.y + offset.y;
        transform.position = Camera_Pos;
    }
}
