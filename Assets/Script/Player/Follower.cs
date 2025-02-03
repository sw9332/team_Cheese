using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public PlayerControl player; // 플레이어 참조
    public float followSpeed = 2.5f;
    public float stopDistance = 1f; // 도착 시 멈출 거리

    void Update()
    {
        followSpeed = PlayerControl.speed - 0.05f;
        if (player.positionHistory.Count > 0)
        {
            Vector3 targetPosition = player.positionHistory.Peek(); // 가장 오래된 위치 가져오기

            float distance = Vector3.Distance(transform.position, targetPosition);

            // 목표 위치에 도달했으면 큐에서 제거
            if (distance < stopDistance)
            {
                player.positionHistory.Dequeue();
            }
            else
            {
                // 목표 위치로 이동
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }

            if (transform.position.y > player.transform.position.y + 1)
            {
                transform.GetComponent<SpriteRenderer>().sortingOrder = 9; // 뒤로
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().sortingOrder = 15; // 앞으로
            }
        }
    }
}
