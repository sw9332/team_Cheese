using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public PlayerControl player; // �÷��̾� ����
    public float followSpeed = 2.5f;
    public float stopDistance = 1f; // ���� �� ���� �Ÿ�

    void Update()
    {
        followSpeed = PlayerControl.speed - 0.05f;
        if (player.positionHistory.Count > 0)
        {
            Vector3 targetPosition = player.positionHistory.Peek(); // ���� ������ ��ġ ��������

            float distance = Vector3.Distance(transform.position, targetPosition);

            // ��ǥ ��ġ�� ���������� ť���� ����
            if (distance < stopDistance)
            {
                player.positionHistory.Dequeue();
            }
            else
            {
                // ��ǥ ��ġ�� �̵�
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }

            if (transform.position.y > player.transform.position.y + 1)
            {
                transform.GetComponent<SpriteRenderer>().sortingOrder = 9; // �ڷ�
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().sortingOrder = 15; // ������
            }
        }
    }
}
