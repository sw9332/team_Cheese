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
            case "파티룸 (입구)": StartCoroutine(TeleportFade("파티룸", other, -4f, -3.7f, true)); break;
            case "파티룸 (출구)": StartCoroutine(TeleportFade("복도", other, other.transform.position.x, -11.8f, true)); break;

            case "연회장 입구 (입구)": StartCoroutine(TeleportFade("연회장 입구", other, other.transform.position.x, 5.5f, true)); break;
            case "연회장 입구 (출구)": StartCoroutine(TeleportFade("복도", other, other.transform.position.x, -2.85f, true)); break;

            case "연회장 (입구)": StartCoroutine(TeleportFade("연회장", other, -3.5f, other.transform.position.y, true)); break;
            case "연회장 (출구)": StartCoroutine(TeleportFade("연회장 입구", other, -13.3f, other.transform.position.y, true)); break;

            case "창고 입구 (입구)": StartCoroutine(TeleportFade("창고 입구", other, other.transform.position.x, -3.8f, true)); break;
            case "창고 입구 (출구)": StartCoroutine(TeleportFade("복도", other, other.transform.position.x, -12f, true)); break;

            case "창고 (입구)": StartCoroutine(TeleportFade("창고", other, other.transform.position.x, 16.5f, true)); break;
            case "창고 (출구)": StartCoroutine(TeleportFade("창고 입구", other, other.transform.position.x,8.1f, true)); break;

            case "Stage1 (입구)": StartCoroutine(TeleportFade("Stage1", other, -63.45f, -31f, false)); break;

            case "보스 (입구)": StartCoroutine(TeleportFade("보스", other, -63f, -56.8f, false)); break;

            case "RoomE Go": StartCoroutine(TeleportFade("", other, 27.76f, -49.45f, false)); break;
            case "RoomE Exit": StartCoroutine(TeleportFade("", other, 41.15f, -42.31f, false)); break;

            case "RoomF Go": StartCoroutine(TeleportFade("", other, 40.97f, -58.25f, false)); break;
            case "RoomF Exit": StartCoroutine(TeleportFade("", other, 28.05f, -63f, false)); break;

            case "Chapter 2 연회장 입구 (입구)": StartCoroutine(TeleportFade("Chapter 2 연회장 입구", other, other.transform.position.x, -127, false)); break;
            case "Chapter 2 연회장 입구 (출구)": StartCoroutine(TeleportFade("Chapter 2 복도", other, other.transform.position.x, -134.9f, false)); break;

            case "Chapter 2 연회장 (입구)": StartCoroutine(TeleportFade("Chapter 2 연회장", other, -3.8f, other.transform.position.y, false)); break;
            case "Chapter 2 연회장 (출구)": StartCoroutine(TeleportFade("Chapter 2 연회장 입구", other, -13.08f, other.transform.position.y, false)); break;

            case "Chapter 2 창고 입구 (입구)": StartCoroutine(TeleportFade("Chapter 2 창고 입구", other, other.transform.position.x, -136, false)); break;
            case "Chapter 2 창고 입구 (출구)": StartCoroutine(TeleportFade("Chapter 2 복도", other, other.transform.position.x, -143.9f, false)); break;

            case "Chapter 2 창고 (입구)": StartCoroutine(TeleportFade("Chapter 2 창고", other, other.transform.position.x, -116, false)); break;
            case "Chapter 2 창고 (출구)": StartCoroutine(TeleportFade("Chapter 2 창고 입구", other, other.transform.position.x, -124, false)); break;

            case "Chapter 2 하단 복도 (입구)": StartCoroutine(TeleportFade("Chapter 2 하단 복도", other, -32.3f, -182, false)); break;
            case "Chapter 2 하단 복도 (출구)": StartCoroutine(TeleportFade("Chapter 2 복도", other, -21.5f, -166, false)); break;

            case "Chapter 2 Enemy Room (입구)": StartCoroutine(TeleportFade("Chapter 2 Enemy Room", other, -44.42f, -172.5f, false)); break;
            case "Chapter 2 Enemy Room (출구)": StartCoroutine(TeleportFade("Chapter 2 하단 복도", other, -39, -180, false)); break;

            case "Chapter 2 왼쪽 하단 복도 (입구)": StartCoroutine(TeleportFade("Chapter 2 왼쪽 하단 복도", other, -63.5f, -163, false)); break;
            case "Chapter 2 왼쪽 하단 복도 (출구)": StartCoroutine(TeleportFade("Chapter 2 복도", other, -49.44f, -149.95f, false)); break;

            case "Chapter 2 CCTV Room (입구)": StartCoroutine(TeleportFade("Chapter 2 CCTV Room", other, -21.8f, -192, false)); break;
            case "Chapter 2 CCTV Room (출구)": StartCoroutine(TeleportFade("Chapter 2 하단 복도", other, -32.3f, -197, false)); break;

            case "Chapter 2 왼쪽 하단 (입구)": StartCoroutine(TeleportFade("Chapter 2 왼쪽 하단", other, -63, -188.8f, false)); break;
            case "Chapter 2 왼쪽 하단 (출구)": StartCoroutine(TeleportFade("Chapter 2 왼쪽 하단 복도", other, -63.97f, -180.9f, false)); break;

            case "Chapter 2 왼쪽 하단 Enemy Room (입구)": StartCoroutine(TeleportFade("Chapter 2 왼쪽 하단 Enemy Room", other, -79.972f, -179f, false)); break;
            case "Chapter 2 왼쪽 하단 Enemy Room (출구)": StartCoroutine(TeleportFade("Chapter 2 왼쪽 하단", other, -76, -188.8f, false)); break;
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
                case "Chapter 2 연회장 입구": n_Player.transform.position = new Vector2(player.transform.position.x - 1f, player.transform.position.y + 1f); break;
                case "Chapter 2 연회장": n_Player.transform.position = new Vector2(player.transform.position.x + 1f, player.transform.position.y - 1f); break;
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
