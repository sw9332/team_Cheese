using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;

    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode screenMode;

    int resolutionNum;
    int optionNum = 0;

    void InitUI()
    {
        for(int i=0; i<Screen.resolutions.Length; i++)
        {
            if(Screen.resolutions[i].refreshRate == StartSceneManager.Screen_Frame)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        resolutionDropdown.options.Clear();

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "Hz";
            resolutionDropdown.options.Add(option);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }

        resolutionDropdown.RefreshShownValue();
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropdownOptionChange(int x)
    {
        resolutionNum = x;
        OkBtnClick();
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        OkBtnClick();
    }

    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

    public Slider background_sound;
    public Slider effect_sound;

    public Text background_sound_text;
    public Text effect_sound_text;

    void Start()
    {
        InitUI();
        background_sound.value = 100;
        effect_sound.value = 100;
    }

    void Update()
    {
        background_sound_text.text = "배경음 : "+background_sound.value.ToString("F1")+"%";
        effect_sound_text.text = "효과음 : "+effect_sound.value.ToString("F1")+"%";
    }
}
