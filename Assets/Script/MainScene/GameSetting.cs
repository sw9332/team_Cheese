using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameSetting : MonoBehaviour
{
    //해상도 설정 부분-------------------------------------------------------------------------

    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;

    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode screenMode;

    int resolutionNum;
    int optionNum = 0;

    void InitUI()
    {
        var sortedResolutions = Screen.resolutions.Where(res => res.refreshRate == StartSceneManager.Screen_Frame).OrderByDescending(res => res.width * res.height).ToList();
        resolutions = sortedResolutions;
        resolutionDropdown.options.Clear();
        optionNum = 0;

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

    //그래픽 품질 설정 부분---------------------------------------------------------------------------------------

    public static string Graphics = "Ultra";

    public GameObject U_Check;
    public GameObject H_Check;
    public GameObject M_Check;
    public GameObject L_Check;

    public void GraphicsSettings_Ultra()
    {
        Graphics = "Ultra";
    }

    public void GraphicsSettings_High()
    {
        Graphics = "High";
    }

    public void GraphicsSettings_Medium()
    {
        Graphics = "Medium";
    }

    public void GraphicsSettings_Low()
    {
        Graphics = "Low";
    }

    void Graphic()
    {
        if(Graphics == "Ultra")
        {
            QualitySettings.SetQualityLevel(5);
            U_Check.SetActive(true);
            H_Check.SetActive(false);
            M_Check.SetActive(false);
            L_Check.SetActive(false);
        }

        else if(Graphics == "High")
        {
            QualitySettings.SetQualityLevel(3);
            U_Check.SetActive(false);
            H_Check.SetActive(true);
            M_Check.SetActive(false);
            L_Check.SetActive(false);
        }

        else if(Graphics == "Medium")
        {
            QualitySettings.SetQualityLevel(2);
            U_Check.SetActive(false);
            H_Check.SetActive(false);
            M_Check.SetActive(true);
            L_Check.SetActive(false);
        }

        else if(Graphics == "Low")
        {
            QualitySettings.SetQualityLevel(1);
            U_Check.SetActive(false);
            H_Check.SetActive(false);
            M_Check.SetActive(false);
            L_Check.SetActive(true);
        }
    }

    //배경음, 효과음 설정 부분------------------------------------------------------------------------------------

    public Slider background_sound;
    public Slider effect_sound;

    public Text background_sound_text;
    public Text effect_sound_text;

    void Background_sound_Setting()
    {
        background_sound_text.text = "배경음 : "+background_sound.value.ToString("F1")+"%";
    }

    void Effect_sound_Setting()
    {
        effect_sound_text.text = "효과음 : "+effect_sound.value.ToString("F1")+"%";
    }

    public void OK_Button()
    {
        PlayerPrefs.SetFloat("Background Sound", background_sound.value);
        PlayerPrefs.SetFloat("Effect Sound", effect_sound.value);
        gameObject.SetActive(false);
    }

    void Start()
    {
        InitUI();

        background_sound.value = PlayerPrefs.GetFloat("Background Sound");
        effect_sound.value = PlayerPrefs.GetFloat("Effect Sound");
    }

    void Update()
    {
        Background_sound_Setting();
        Effect_sound_Setting();   
        Graphic();
    }
}
