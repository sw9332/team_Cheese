using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private Player player;

    public Vector2 PlayerPosition;
    public float Time;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPosition.x = player.transform.position.x;
            PlayerPosition.y = player.transform.position.y;
        }
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
}
