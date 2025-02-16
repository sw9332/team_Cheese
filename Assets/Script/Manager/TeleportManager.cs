using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour
{
    private Player player;
    private N_Player n_Player;
    private FadeManager fadeManager;
    private TextManager textManager;

    public void Teleport(string stateName, Collider2D other)
    {
        switch (stateName)
        {
            case "��Ƽ�� (�Ա�)": StartCoroutine(TeleportFade("��Ƽ��", other, -4f, -3.7f, true)); break;
            case "��Ƽ�� (�ⱸ)": StartCoroutine(TeleportFade("����", other, other.transform.position.x, -11.8f, true)); break;

            case "��ȸ�� �Ա� (�Ա�)": StartCoroutine(TeleportFade("��ȸ�� �Ա�", other, other.transform.position.x, 5.5f, true)); break;
            case "��ȸ�� �Ա� (�ⱸ)": StartCoroutine(TeleportFade("����", other, other.transform.position.x, -2.85f, true)); break;

            case "��ȸ�� (�Ա�)": StartCoroutine(TeleportFade("��ȸ��", other, -3.5f, other.transform.position.y, true)); break;
            case "��ȸ�� (�ⱸ)": StartCoroutine(TeleportFade("��ȸ�� �Ա�", other, -13.3f, other.transform.position.y, true)); break;

            case "â�� �Ա� (�Ա�)": StartCoroutine(TeleportFade("â�� �Ա�", other, other.transform.position.x, -3.8f, true)); break;
            case "â�� �Ա� (�ⱸ)": StartCoroutine(TeleportFade("����", other, other.transform.position.x, -12f, true)); break;

            case "â�� (�Ա�)": StartCoroutine(TeleportFade("â��", other, other.transform.position.x, 16.5f, true)); break;
            case "â�� (�ⱸ)": StartCoroutine(TeleportFade("â�� �Ա�", other, other.transform.position.x,8.1f, true)); break;

            case "Stage1 (�Ա�)": StartCoroutine(TeleportFade("Stage1", other, -63.45f, -31f, false)); break;

            case "���� (�Ա�)": StartCoroutine(TeleportFade("����", other, -63f, -56.8f, false)); break;

            case "RoomE Go": StartCoroutine(TeleportFade("", other, 27.76f, -49.45f, false)); break;
            case "RoomE Exit": StartCoroutine(TeleportFade("", other, 41.15f, -42.31f, false)); break;

            case "RoomF Go": StartCoroutine(TeleportFade("", other, 40.97f, -58.25f, false)); break;
            case "RoomF Exit": StartCoroutine(TeleportFade("", other, 28.05f, -63f, false)); break;

            case "Chapter 2 ��ȸ�� �Ա� (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 ��ȸ�� �Ա�", other, other.transform.position.x, -127, false)); break;
            case "Chapter 2 ��ȸ�� �Ա� (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 ����", other, other.transform.position.x, -134.9f, false)); break;

            case "Chapter 2 ��ȸ�� (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 ��ȸ��", other, -3.8f, other.transform.position.y, false)); break;
            case "Chapter 2 ��ȸ�� (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 ��ȸ�� �Ա�", other, -13.08f, other.transform.position.y, false)); break;

            case "Chapter 2 â�� �Ա� (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 â�� �Ա�", other, other.transform.position.x, -136, false)); break;
            case "Chapter 2 â�� �Ա� (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 ����", other, other.transform.position.x, -143.9f, false)); break;

            case "Chapter 2 â�� (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 â��", other, other.transform.position.x, -116, false)); break;
            case "Chapter 2 â�� (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 â�� �Ա�", other, other.transform.position.x, -124, false)); break;

            case "Chapter 2 �ϴ� ���� (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 �ϴ� ����", other, -32.3f, -182, false)); break;
            case "Chapter 2 �ϴ� ���� (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 ����", other, -21.5f, -166, false)); break;

            case "Chapter 2 Enemy Room (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 Enemy Room", other, -44.42f, -172.5f, false)); break;
            case "Chapter 2 Enemy Room (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 �ϴ� ����", other, -39, -180, false)); break;

            case "Chapter 2 CCTV Room (�Ա�)": StartCoroutine(TeleportFade("Chapter 2 CCTV Room", other, -21.8f, -192, false)); break;
            case "Chapter 2 CCTV Room (�ⱸ)": StartCoroutine(TeleportFade("Chapter 2 �ϴ� ����", other, -32.3f, -197, false)); break;
        }
    }

    IEnumerator TeleportFade(string state, Collider2D other, float x, float y, bool print)
    {
        fadeManager.fadeDuration = 0.5f;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        GameManager.GameState = state;
        other.transform.position = new Vector3(x, y, 0);

        if (n_Player.isFollow)
        {
            switch (state)
            {
                case "Chapter 2 ��ȸ�� �Ա�": n_Player.transform.position = new Vector2(player.transform.position.x - 1f, player.transform.position.y + 1f); break;
                case "Chapter 2 ��ȸ��": n_Player.transform.position = new Vector2(player.transform.position.x + 1f, player.transform.position.y - 1f); break;
                default: n_Player.transform.position = new Vector2(player.transform.position.x - 1f, player.transform.position.y); break;
            }
            
        }

        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black, false));
        if (print) textManager.ShowMapNameText(GameManager.GameState, 1.5f);
        fadeManager.fadeDuration = 1f;
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        n_Player = FindFirstObjectByType<N_Player>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        textManager = FindFirstObjectByType<TextManager>();
    }
}
