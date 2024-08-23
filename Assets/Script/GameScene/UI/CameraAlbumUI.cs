using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAlbumUI : MonoBehaviour
{
    public GameObject Album;
    public Image cakeImage;
    public static bool cakeImageAvailable = false;
    public GameObject player;

    void AlbumUI_Open_Close()
    {
        if (Input.GetKeyDown(KeyCode.E) && Album.activeSelf == false)
        {
            Album.SetActive(true);
            player.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E) && Album.activeSelf == true)
        {
            Album.SetActive(false);
            player.SetActive(true);
        }

    }

    void ShowCakeImage()
    {
        if (cakeImageAvailable)
            cakeImage.enabled =  true;
    }
     
    void Update()
    {
        AlbumUI_Open_Close();
        ShowCakeImage();
    }
}
