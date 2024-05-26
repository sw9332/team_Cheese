using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player_thoughts_UI; //대화창
    public GameObject Player_Image; //Player 이미지

    public GameObject Story_NextButton; //스토리 진행 다음 버튼
    public GameObject UI_Close_Button; //대화창 닫기 버튼

    //--------------------------------------------------------------------

    public Text thoughts_Text; //Text
    public static int Next_value = 0; //스토리 진행 값

    void player_UI() //주인공 속마음
    {
        Player_Image.SetActive(true);
        player_thoughts_UI.SetActive(true);

        Story_NextButton.SetActive(true);
        UI_Close_Button.SetActive(false);
    }

    void progress() //대화창
    {
        Player_Image.SetActive(false);
        player_thoughts_UI.SetActive(true);

        Story_NextButton.SetActive(true);
        UI_Close_Button.SetActive(false);
    }

    void UI_Close() //UI 닫기
    {
        player_thoughts_UI.SetActive(false);
    }

    //--------------------------------------------------------------------

    public GameObject CameraUI;

    public static string Text_effect;

    public static bool Text_effect_isCheck = false;

    //스토리 진행
    void Stroy()
    {
        switch(Next_value)
        {
            case 0:
                Text_effect = "20XX년 #%월 %@일. 오늘은 내#$%......";
                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;
                progress();
                break;

            case 1:
                Text_effect = "오늘은 #@#의 생일이다.";

                if(Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = false;

                progress();
                break;

            case 2:
                Text_effect = "#&케이크를 찾아서 사진을 찍$%..한...다....";

                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;

                progress();
                break;

            case 3:
                Text_effect = "오늘은..... 드디어! 내 생일이다!";

                if(Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = false;

                player_UI();
                break;

            case 4:
                Text_effect = "친구들이 오기 전에 파티 준비를 해볼까?";

                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;

                player_UI();
                break;

            case 5:
                UI_Close();
                break;

            case 6:
                Text_effect = "어?! 저기 케이크다!";
                
                if(Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = false;

                player_UI();
                break;

            case 7:
                Text_effect = "음..... 케이크를 주우려면 인형을 옮겨야겠어!";
                
                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;

                player_UI();
                break;

            case 8:
                Text_effect = "!@#$을..... 인형을 옮기려면 스페이스바 키로 수집 후, 다시 인벤토리 창에서 꺼내 올려두세요.";
                
                if(Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = false;

                progress();
                break;

            case 9:
                UI_Close();
                break;

            case 10: //인형을 인벤토리에 넣은 후.
                Text_effect = "#@#은 카메라를 %#$%다!";
                
                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;

                progress();
                break;

            case 11:
                Text_effect = "빨리... 케이크를 찌...ㄱ..... #@$#@$%!!!";
                
                if(Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = false;

                progress();
                break;

            case 12:
                Text_effect = "와!!! 카메라다! 빨리, 케이크....";
                
                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;

                player_UI();
                break;

            case 13:
                Text_effect = "어?! 아! 케이크를 빨리 옮기자!";
                
                if(Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = false;

                player_UI();
                break;

            case 14:
                Text_effect = "카메라를 획득하였습니다.";
                CameraUI.SetActive(true);
                
                if(!Text_effect_isCheck)
                    StartCoroutine(typing());
                    Text_effect_isCheck = true;

                progress();
                break;

            case 15:
                UI_Close();
                break;

            case 16:
                UI_Close();
                break;
        }
    }

    //---------------------------------------------------------------------

    //대화창 타이핑 효과
    public Text text_typing_effect;

    IEnumerator typing()
    {
        yield return new WaitForSeconds(0f);
        for(int i=0; i<=Text_effect.Length; i++)
        {
            text_typing_effect.text = Text_effect.Substring(0, i);
            yield return new WaitForSeconds(0.03f);
        }
    }

    //오브젝트 상호작용 대화------------------------------------------------

    public GameObject Object_Interaction_UI;
    public Text Interaction_Text;
    public static string Interaction_string;
    public static string Interaction_string_isCheck = "";

    void Object_Interaction()
    {
        switch(Player.ObjectName)
        {
            case "달력":
                Interaction_string = "오늘이... 바로 내 생일!";

                if(Interaction_string_isCheck != "달력")
                    StartCoroutine(typing2());
                    Interaction_string_isCheck = "달력";

                Interaction_UI_open();
                break;

            case "인형":
                Interaction_string = "인형... 인형이다.";

                if(Interaction_string_isCheck != "인형")
                    StartCoroutine(typing2());
                    Interaction_string_isCheck = "인형";

                Interaction_UI_open();
                break;

            case "나가는 문":
                Interaction_string = "열리지 않는다.";

                if(Interaction_string_isCheck != "나가는 문")
                    StartCoroutine(typing2());
                    Interaction_string_isCheck = "나가는 문";

                Interaction_UI_open();
                break;
        }
    }

    //---------------------------------------------------------------------

    //상호작용 텍스트 타이핑 효과
    public Text Interaction_typing_effect;

    IEnumerator typing2()
    {
        yield return new WaitForSeconds(0f);
        for(int i=0; i<=Interaction_string.Length; i++)
        {
            Interaction_typing_effect.text = Interaction_string.Substring(0, i);
            yield return new WaitForSeconds(0.03f);
        }
    }

    //---------------------------------------------------------------------

    void Interaction_UI_open()
    {
        Object_Interaction_UI.SetActive(true);
    }

    public void Interaction_UI_close() //닫기 버튼
    {
        Player.ObjectName = "";
        Object_Interaction_UI.SetActive(false);
    }

    //--------------------------------------------------------------------

    //대화 닫기/다음 버튼
    public void NextButton()
    {
        Next_value++;
    }

    public void CloseButton()
    {
        UI_Close();
    }

    //--------------------------------------------------------------------

    //카메라 UI 효과
    public Animator Camera_Effect_Animation;
    public static bool Camera_setactive = false;

    void Camera_effect_manager()
    {
        switch(Camera_setactive)
        {
            case false:
                Camera_Effect_Animation.Play("Camera_Effect_false");
                break;

            case true:
                Camera_Effect_Animation.Play("Camera_Effect_true");
                break;
        }
    }

    //--------------------------------------------------------------------

    //페이드 인 효과
    public Image fadeImage;
    public GameObject fade;
    public float fadeDuration = 1.0f;
    
    private bool isFading = false;
    private float fadeTimer = 0.0f;
    
    void fadein_Start()
    {
        fade.SetActive(true);
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        StartFade();
    }

    void Fadein()
    {
        if(isFading && Next_value >= 3)
        {
            fadeTimer += Time.deltaTime;
            float alpha = 1.0f - Mathf.Clamp01(fadeTimer / fadeDuration);
            
            fadeImage.canvasRenderer.SetAlpha(alpha);

            if(fadeTimer >= fadeDuration)
            {
                isFading = false;
                FadeImage_false();
            }
        }
    }

    void StartFade()
    {
        isFading = true;
        fadeTimer = 0.0f;
    }

    void FadeImage_false()
    {
        fade.SetActive(false);
    }

    //--------------------------------------------------------------------

    void Start()
    {
        player_thoughts_UI.SetActive(true);
        fadein_Start();
    }

    void Update()
    {
        Stroy(); //스토리 진행
        Object_Interaction(); //오브젝트 상호작용 대화
        Camera_effect_manager(); //카메라 UI 효과
        Fadein();
    }
}
