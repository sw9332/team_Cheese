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
            case "Tutorial Room": other.transform.position = new Vector3(57.5f, -1.8f, 0); ShowMapNameText("튜토리얼 방"); break;
            case "In front Tutorial Room": other.transform.position = new Vector3(other.transform.position.x, -11.3f, 0); ShowMapNameText("OO복도"); break;

            case "RoomA Go": other.transform.position = new Vector3(other.transform.position.x, 7.5f, 0f); break;
            case "RoomA Exit": other.transform.position = new Vector3(other.transform.position.x, -2.33f, 0f); break;

            case "RoomB Go": other.transform.position = new Vector3(59f, 19.67f, 0f); break;
            case "RoomB Exit": other.transform.position = new Vector3(46.61f, 19.67f, 0f); break;

            case "RoomC Go": other.transform.position = new Vector3(other.transform.position.x, -1.7f, 0f); break;
            case "RoomC Exit": other.transform.position = new Vector3(other.transform.position.x, -11.6f, 0f); break;

            case "RoomD Go": other.transform.position = new Vector3(other.transform.position.x, 18.5f, 0f); break;
            case "RoomD Exit": other.transform.position = new Vector3(other.transform.position.x, 8.6f, 0f); break;

            case "RoomE Go": other.transform.position = new Vector3(27.76f, -49.45f, 0f); break;
            case "RoomE Exit": other.transform.position = new Vector3(41.15f, -42.31f, 0f); break;

            case "RoomF Go": other.transform.position = new Vector3(40.97f, -58.25f, 0f); break;
            case "RoomF Exit": other.transform.position = new Vector3(28.05f, -63f, 0f); break;
        }
    }

    public void ShowMapNameText(string MapName)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
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
