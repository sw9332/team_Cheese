using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameSetting : MonoBehaviour
{
    private static GameSetting instance;

    public static GameSetting Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public static int ScreenFrame = 60;

    public GameObject ui;
    public GameObject GraphicTab;
    public GameObject AudioTab;
    public GameObject KeyTab;

    public void Tab(int tabValue)
    {
        switch(tabValue)
        {
            case 1:
                GraphicTab.SetActive(true);
                AudioTab.SetActive(false);
                KeyTab.SetActive(false);
                break;
            case 2:
                GraphicTab.SetActive(false);
                AudioTab.SetActive(true);
                KeyTab.SetActive(false);
                break;
            case 3:
                GraphicTab.SetActive(false);
                AudioTab.SetActive(false);
                KeyTab.SetActive(true);
                break;

        }
    }

    //해상도 설정 부분-------------------------------------------------------------------------

    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;

    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode screenMode;

    int resolutionNum;
    int optionNum = 0;

    //해상도 리스트
    int[,] allowedResolutions = new int[,]
    { 
        {1920, 1080},
        {1600, 900},
        {1366, 768},
        {1280, 720},
    };

    void InitUI()
    {
        var filteredResolutions = Screen.resolutions
            .Where(res => IsAllowedResolution(res.width, res.height))
            .GroupBy(res => new { res.width, res.height })
            .Select(g => g.First())
            .OrderByDescending(res => res.width * res.height)
            .ToList();

        resolutions = filteredResolutions;
        resolutionDropdown.options.Clear();
        optionNum = 0;

        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " ";
            resolutionDropdown.options.Add(option);
            if (item.width == Screen.width && item.height == Screen.height) resolutionDropdown.value = optionNum;
            optionNum++;
        }

        resolutionDropdown.RefreshShownValue();
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    bool IsAllowedResolution(int width, int height)
    {
        for (int i = 0; i < allowedResolutions.GetLength(0); i++) if (allowedResolutions[i, 0] == width && allowedResolutions[i, 1] == height) return true;
        return false;
    }

    public void DropdownOptionChange(int x)
    {
        resolutionNum = x;
        ScreenApply();
    }

    public void FullScreen(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        ScreenApply();
    }

    void ScreenApply()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

    //프레임 설정 부분---------------------------------------------------------------------------------------

    public GameObject Check_30;
    public GameObject Check_60;
    public GameObject Check_144;

    private int SaveFPS;
    private int LoadFPS;

    public void Frame(int fps)
    {
        SaveFPS = fps;

        switch (fps)
        {
            case 30:
                ScreenFrame = 30;
                Check_30.SetActive(true);
                Check_60.SetActive(false);
                Check_144.SetActive(false);
                break;

            case 60:
                ScreenFrame = 60;
                Check_30.SetActive(false);
                Check_60.SetActive(true);
                Check_144.SetActive(false);
                break;

            case 144:
                ScreenFrame = 144;
                Check_30.SetActive(false);
                Check_60.SetActive(false);
                Check_144.SetActive(true);
                break;
        }

        PlayerPrefs.SetInt("Frame", SaveFPS);
    }

    void FrameLoad()
    {
        LoadFPS = PlayerPrefs.GetInt("Frame", 60);
        Frame(LoadFPS);
    }

    //그래픽 품질 설정 부분---------------------------------------------------------------------------------------

    public GameObject U_Check;
    public GameObject H_Check;
    public GameObject M_Check;
    public GameObject L_Check;

    private string SaveGraphics;
    private string LoadGraphics;

    public void Graphic(string graphics)
    {
        SaveGraphics = graphics;

        switch(graphics)
        {
            case "Ultra": QualitySettings.SetQualityLevel(5);
                U_Check.SetActive(true);
                H_Check.SetActive(false);
                M_Check.SetActive(false);
                L_Check.SetActive(false);
                break;

            case "High": QualitySettings.SetQualityLevel(3);
                U_Check.SetActive(false);
                H_Check.SetActive(true);
                M_Check.SetActive(false);
                L_Check.SetActive(false);
                break;

            case "Medium": QualitySettings.SetQualityLevel(2);
                U_Check.SetActive(false);
                H_Check.SetActive(false);
                M_Check.SetActive(true);
                L_Check.SetActive(false);
                break;

            case "Low": QualitySettings.SetQualityLevel(1);
                U_Check.SetActive(false);
                H_Check.SetActive(false);
                M_Check.SetActive(false);
                L_Check.SetActive(true);
                break;
        }

        PlayerPrefs.SetString("Graphic", SaveGraphics);
    }

    void GraphicLoad()
    {
        LoadGraphics = PlayerPrefs.GetString("Graphic", "Ultra");
        Graphic(LoadGraphics);
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

    void SoundLoad()
    {
        background_sound.value = PlayerPrefs.GetFloat("Background Sound");
        effect_sound.value = PlayerPrefs.GetFloat("Effect Sound");
    }

    //--------------------------------------------------------------------------------------------------------------

    public void OK_Button()
    {
        PlayerPrefs.SetFloat("Background Sound", background_sound.value);
        PlayerPrefs.SetFloat("Effect Sound", effect_sound.value);
        gameObject.SetActive(false);
    }

    void Update()
    {
        Background_sound_Setting();
        Effect_sound_Setting();

        Application.targetFrameRate = ScreenFrame;
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)) OK_Button();
    }

    void Start()
    {
        InitUI();
        FrameLoad();
        GraphicLoad();
        SoundLoad();
    }
}