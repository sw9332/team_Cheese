using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerControl player;

    public Vector2 PlayerPosition;
    public string GameSate;
    public float Time;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPosition.x = player.transform.position.x;
            PlayerPosition.y = player.transform.position.y;
            GameSate = GameManager.GameState;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = PlayerPosition;
            GameManager.GameState = GameSate;
        }
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerControl>();
    }
}
