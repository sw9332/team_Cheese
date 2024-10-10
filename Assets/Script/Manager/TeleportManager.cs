using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour
{
    public string locationName;
    public Text MapNameText;
    public float fadeDuration = 1f;
    private Coroutine fadeCoroutine;

    public void teleport(string locationName, Collider2D other)
    {
        switch (locationName)
        {
            case "파티룸 (입구)": GameManager.GameState = "파티룸";
                other.transform.position = new Vector3(57.5f, -1.8f, 0);
                ShowMapNameText(GameManager.GameState);
                break;
            case "파티룸 (출구)": GameManager.GameState = "복도 #F";
                other.transform.position = new Vector3(other.transform.position.x, -11.3f, 0);
                ShowMapNameText(GameManager.GameState);
                break;

            case "연회장 입구 (입구)": GameManager.GameState = "연회장 입구";
                other.transform.position = new Vector3(other.transform.position.x, 7.5f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;
            case "연회장 입구 (출구)": GameManager.GameState = "복도 #F";
                other.transform.position = new Vector3(other.transform.position.x, -2.33f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;

            case "연회장 (입구)": GameManager.GameState = "연회장";
                other.transform.position = new Vector3(59f, 19.67f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;
            case "연회장 (출구)": GameManager.GameState = "연회장 입구";
                other.transform.position = new Vector3(46.61f, 19.67f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;

            case "창고 입구 (입구)": GameManager.GameState = "창고 입구";
                other.transform.position = new Vector3(other.transform.position.x, -1.7f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;
            case "창고 입구 (출구)": GameManager.GameState = "복도 #F";
                other.transform.position = new Vector3(other.transform.position.x, -11.6f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;

            case "창고 (입구)": GameManager.GameState = "창고";
                other.transform.position = new Vector3(other.transform.position.x, 18.5f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;
            case "창고 (출구)": GameManager.GameState = "창고 입구";
                other.transform.position = new Vector3(other.transform.position.x, 8.6f, 0f);
                ShowMapNameText(GameManager.GameState);
                break;

            case "RoomE Go": GameManager.GameState = "-";
                other.transform.position = new Vector3(27.76f, -49.45f, 0f); ShowMapNameText(GameManager.GameState); break;
            case "RoomE Exit": other.transform.position = new Vector3(41.15f, -42.31f, 0f); ShowMapNameText(GameManager.GameState); break;

            case "RoomF Go": GameManager.GameState = "-";
                other.transform.position = new Vector3(40.97f, -58.25f, 0f); ShowMapNameText(GameManager.GameState); break;
            case "RoomF Exit": GameManager.GameState = "-";
                other.transform.position = new Vector3(28.05f, -63f, 0f); ShowMapNameText(GameManager.GameState); break;
        }
    }

    public void ShowMapNameText(string MapName)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            MapNameText.color = Color.clear;
        }

        fadeCoroutine = StartCoroutine(ShowMapNameTextCoroutine(MapName));
    }

    private IEnumerator ShowMapNameTextCoroutine(string MapName)
    {
        MapNameText.text = MapName;
        yield return StartCoroutine(FadeInText());
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FadeOutText());
    }

    public IEnumerator FadeInText()
    {
        MapNameText.gameObject.SetActive(true);
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            MapNameText.color = Color.Lerp(Color.clear, Color.white, timer / fadeDuration);
            yield return null;
        }
    }

    public IEnumerator FadeOutText()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            MapNameText.color = Color.Lerp(Color.white, Color.clear, timer / fadeDuration);
            yield return null;
        }

        MapNameText.gameObject.SetActive(false);
    }
}
