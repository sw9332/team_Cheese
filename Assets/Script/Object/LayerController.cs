using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    private Player player;
    private SpriteRenderer spriteRenderer;

    void Update()
    {
        if (player.transform.position.y > transform.position.y - 0.1f) spriteRenderer.sortingOrder = 15;
        else spriteRenderer.sortingOrder = 10;
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}