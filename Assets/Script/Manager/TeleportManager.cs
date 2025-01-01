using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour
{
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
        }
    }

    IEnumerator TeleportFade(string state, Collider2D other, float x, float y, bool print)
    {
        fadeManager.fadeDuration = 0.5f;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        GameManager.GameState = state;
        other.transform.position = new Vector3(x, y, 0);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));
        if (print) textManager.ShowMapNameText(GameManager.GameState, 1.5f);
        fadeManager.fadeDuration = 1f;
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();
        textManager = FindFirstObjectByType<TextManager>();
    }
}
