using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour
{
    private FadeManager fadeManager;
    private MapNameManager mapNameManager;

    public void Teleport(string stateName, Collider2D other)
    {
        switch (stateName)
        {
            case "파티룸 (입구)": StartCoroutine(TeleportFade("파티룸", other, -4f, -3.7f)); break;
            case "파티룸 (출구)": StartCoroutine(TeleportFade("복도 #F", other, other.transform.position.x, -11.8f)); break;

            case "연회장 입구 (입구)": StartCoroutine(TeleportFade("연회장 입구", other, other.transform.position.x, 5.5f)); break;
            case "연회장 입구 (출구)": StartCoroutine(TeleportFade("복도 #F", other, other.transform.position.x, -2.85f)); break;

            case "연회장 (입구)": StartCoroutine(TeleportFade("연회장", other, -3.5f, other.transform.position.y)); break;
            case "연회장 (출구)": StartCoroutine(TeleportFade("연회장 입구", other, -13.3f, other.transform.position.y)); break;

            case "창고 입구 (입구)": StartCoroutine(TeleportFade("창고 입구", other, other.transform.position.x, -3.8f)); break;
            case "창고 입구 (출구)": StartCoroutine(TeleportFade("복도 #F", other, other.transform.position.x, -12f)); break;

            case "창고 (입구)": StartCoroutine(TeleportFade("창고", other, other.transform.position.x, 16.5f)); break;
            case "창고 (출구)": StartCoroutine(TeleportFade("창고 입구", other, other.transform.position.x,8.1f)); break;

            case "Stage1 (입구)": StartCoroutine(TeleportFade("Stage1", other, -63.45f, -31f)); break;

            case "보스 (입구)": StartCoroutine(TeleportFade("보스", other, -63f, -56.8f)); break;

            case "RoomE Go": StartCoroutine(TeleportFade("", other, 27.76f, -49.45f)); break;
            case "RoomE Exit": StartCoroutine(TeleportFade("", other, 41.15f, -42.31f)); break;

            case "RoomF Go": StartCoroutine(TeleportFade("", other, 40.97f, -58.25f)); break;
            case "RoomF Exit": StartCoroutine(TeleportFade("", other, 28.05f, -63f)); break;
        }
    }

    IEnumerator TeleportFade(string state, Collider2D other, float x, float y)
    {
        fadeManager.fadeDuration = 0.5f;
        yield return StartCoroutine(fadeManager.FadeOut(fadeManager.fadeImage, Color.black));
        GameManager.GameState = state;
        other.transform.position = new Vector3(x, y, 0);
        yield return StartCoroutine(fadeManager.FadeIn(fadeManager.fadeImage, Color.black));
        mapNameManager.ShowMapNameText(GameManager.GameState);
        fadeManager.fadeDuration = 1f;
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();
        mapNameManager = FindFirstObjectByType<MapNameManager>();
    }
}
