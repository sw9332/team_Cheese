using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue; //대화내용

    private DialogueManager dialogueManager; //대화내용 관리

    //public GameObject player_thoughts_UI; //대화창
    //public GameObject Player_Image; //Player 이미지

    public GameObject Story_NextButton; //스토리 진행 다음 버튼
    public GameObject UI_Close_Button; //대화창 닫기 버튼

    //-------------------------------------------------------------------- 아래 단락 전체 내용을 dialogueManager로 통합. 확인 후 삭제 예정

    //public Text thoughts_Text; //Text
    //public static int Next_value = 0; //스토리 진행 값

    //void player_UI() //주인공 속마음
    //{
    //    Player_Image.SetActive(true);
    //    player_thoughts_UI.SetActive(true);

    //    Story_NextButton.SetActive(true);
    //    UI_Close_Button.SetActive(false);
    //}

    //void progress() //대화창
    //{
    //    Player_Image.SetActive(false);
    //    player_thoughts_UI.SetActive(true);

    //    Story_NextButton.SetActive(true);
    //    UI_Close_Button.SetActive(false);
    //}

    //void UI_Close() //UI 닫기
    //{
    //    player_thoughts_UI.SetActive(false);
    //}

    //--------------------------------------------------------------------

    public GameObject CameraUI;

    public static string Text_effect;

    public static bool Text_effect_isCheck = false;

    //스토리 진행    //마찬가지로 대화내용은 dialogue에서 관리, switch문의 역할은 dialoguemanager에서 할 예정.
                     //bool값 혹은 trigger 조정은 계속 고려할 예정 
    //void Stroy() 
    //{
    //    switch(Next_value)
    //    {
    //        case 0:
    //            Text_effect = "20XX년 #%월 %@일. 오늘은 내#$%......";
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;
    //            progress();
    //            break;

    //        case 1:
    //            Text_effect = "오늘은 #@#의 생일이다.";

    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            progress();
    //            break;

    //        case 2:
    //            Text_effect = "#&케이크를 찾아서 사진을 찍$%..한...다....";

    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            progress();
    //            break;

    //        case 3:
    //            Text_effect = "오늘은.....";

    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            player_UI();
    //            break;

    //        case 4:
    //            Text_effect = "드디어! 내 생일이야!!";

    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            player_UI();
    //            break;

    //        case 5:
    //            Text_effect = "친구들이 오기전에 파티준비를 해볼까?";

    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            player_UI();
    //            break;

    //        case 6:
    //            UI_Close();
    //            break;

    //        case 7:
    //            Text_effect = "어?! 저기 케이크다!";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            player_UI();
    //            break;

    //        case 8:
    //            Text_effect = "어.... 음.....";
                
    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            player_UI();
    //            break;

    //        case 9:
    //            Text_effect = "케이크를 주우려면 인형을 옮겨야겠어!";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            player_UI();
    //            break;

    //        case 10:
    //            Text_effect = "!@#$을....";
                
    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            progress();
    //            break;

    //        case 11:
    //            Text_effect = "인형을 옮기려면 스페이스바로 수집 후 인벤토리 창에서 꺼내 올려두세요.";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            progress();
    //            break;

    //        case 12:
    //            UI_Close();
    //            break;

    //        case 13: //인형을 인벤토리에 넣은 후.
    //            Text_effect = "와!!! 카메라다! 카메라로! 카메라로.....";
                
    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            player_UI();
    //            break;

    //        case 14:
    //            Text_effect = "빨리... ㅋ.... 케이크를 %#@$!!!";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            progress();
    //            break;

    //        case 15:
    //            Text_effect = "어?... 어... 아!";
                
    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            player_UI();
    //            break;

    //        case 16:
    //            Text_effect = "빨리 케이크를 옮기자!";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;

    //            player_UI();
    //            break;

    //        case 17:
    //            Text_effect = "카메라를 획득하였습니다.";
    //            CameraUI.SetActive(true);
                
    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            progress();
    //            break;

    //        case 18:
    //            UI_Close();
    //            break;


    //        case 19:
    //            Text_effect = "오!!!";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;


    //            player_UI();
    //            break;

    //        case 20:
    //            Text_effect = "카메라가 빛난다!";
                
    //            if(Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = false;

    //            player_UI();
    //            break;

    //        case 21:
    //            Text_effect = "이제 찍으면 되나?";
                
    //            if(!Text_effect_isCheck)
    //                StartCoroutine(typing());
    //                Text_effect_isCheck = true;


    //            MiniGame.is_take_photo = true;
    //            MiniGame.is_minigame = true;

    //            player_UI();
    //            break;

    //        case 22:
    //            UI_Close();
    //            Text_effect_isCheck = false;
    //            break;
    //    }
    //}

    //---------------------------------------------------------------------

    //대화창 타이핑 효과     // dialogueManager에 통합
    //public Text text_typing_effect;

    //IEnumerator typing()
    //{
    //    yield return new WaitForSeconds(0f);
    //    for(int i=0; i<=Text_effect.Length; i++)
    //    {
    //        text_typing_effect.text = Text_effect.Substring(0, i);
    //        yield return new WaitForSeconds(typingSpeed);
    //    }
    //}

    //오브젝트 상호작용 대화------------------------------------------------
    //각각의 오브젝트에 대화 스크립트를 추가하여 dialouge와 dialogueManager에서 관리할 예정

    //public GameObject Object_Interaction_UI;
    //public GameObject Interaction_Player_Image;
    //public static string Interaction_string;
    //public static string Interaction_string_isCheck = "";
    //public Text Interaction_Text;

    //void Object_Interaction()
    //{
    //    switch(Player.ObjectName)
    //    {
    //        case "달력":
    //            Interaction_string = "오늘이... 바로 내 생일!";

    //            if(Interaction_string_isCheck != "달력")
    //                StartCoroutine(typing2());
    //                Interaction_string_isCheck = "달력";

    //            Interaction_Player_Image.SetActive(true);
    //            Interaction_UI_open();
    //            break;

    //        case "인형":
    //            Interaction_string = "인형... 인형이다.";

    //            if(Interaction_string_isCheck != "인형")
    //                StartCoroutine(typing2());
    //                Interaction_string_isCheck = "인형";

    //            Interaction_Player_Image.SetActive(true);
    //            Interaction_UI_open();
    //            break;

    //        case "나가는 문":
    //            Interaction_string = "열리지 않는다.";

    //            if(Interaction_string_isCheck != "나가는 문")
    //                StartCoroutine(typing2());
    //                Interaction_string_isCheck = "나가는 문";

    //            Interaction_Player_Image.SetActive(false);
    //            Interaction_UI_open();
    //            break;
    //    }
    //}

    //---------------------------------------------------------------------

    //상호작용 텍스트 타이핑 효과 //dialogueManaer에 통합
    //public Text Interaction_typing_effect;

    //IEnumerator typing2()
    //{
    //    yield return new WaitForSeconds(0f);
    //    for(int i=0; i<=Interaction_string.Length; i++)
    //    {
    //        Interaction_typing_effect.text = Interaction_string.Substring(0, i);
    //        yield return new WaitForSeconds(typingSpeed);
    //    }
    //}

    //---------------------------------------------------------------------

    //상호작용UI 열기/닫기 //이후 수정
    //void Interaction_UI_open()
    //{
    //    Object_Interaction_UI.SetActive(true);
    //}

    //public void Interaction_UI_close() //닫기 버튼
    //{
    //    Player.ObjectName = "";
    //    Object_Interaction_UI.SetActive(false);
    //}

    //--------------------------------------------------------------------

    //대화 닫기/다음 버튼
    public void NextButton()
    {
        dialogueManager.count++;
    }

    //public void CloseButton() // 이후 수정
    //{
    //    UI_Close();
    //}

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

    public Image fadeImage;
    public float fadeDuration = 1f;

    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / fadeDuration);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / fadeDuration);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------

    public GameObject ExitUI;

    public void tutorial_Exit_OK()
    {
        StartCoroutine(FadeOut());
        Invoke("MainScene", 1f);
        Time.timeScale = 1;
    }

    public void tutorial_Exit_NO()
    {
        ExitUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    //---------------------------------------------------------------------

    public GameObject CameraAlbumUI;

    void AlbumUI_Open_Close()
    {
        if(Input.GetKeyDown(KeyCode.E) && CameraAlbumUI.activeSelf == false)
            CameraAlbumUI.SetActive(true);
        else if(Input.GetKeyDown(KeyCode.E) && CameraAlbumUI.activeSelf == true)
            CameraAlbumUI.SetActive(false);
    }

    //---------------------------------------------------------------------

    public float typingSpeed = 0.03f;

    public void StartFadeIn() // start fade in coroutine
    {
        StartCoroutine(FadeIn());
    
    }
    public void StopFadeIn() // stop fade in coroutine
    {
        StopCoroutine(FadeIn());
    }

    public void StartFadeOut() // start fade out coroutine
    {
        StartCoroutine(FadeOut());
    }
    public void StopFadeOut() // stop fade out coroutine
    {
        StopCoroutine(FadeIn());
    }



    void Start()
    {
       Time.timeScale = 1;
       //Next_value = 0; // 이후 수정
       //player_thoughts_UI.SetActive(true);
       fadeImage.gameObject.SetActive(true);
       Camera_setactive = false;
    }

    void Update()
    {
        //Stroy(); //스토리 진행
        //Object_Interaction(); //오브젝트 상호작용 대화
        Camera_effect_manager(); //카메라 UI 효과
        AlbumUI_Open_Close(); //카메라 앨범 열기 닫기

        //if(dialogueManager.count == 3)
        //{
        //    StartCoroutine(FadeIn());
        //}

        //else if(dialogueManager.count > 3)
        //{
        //    StopCoroutine(FadeIn());
        //}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(ExitUI.activeSelf == false)
            {
                ExitUI.SetActive(true);
                Time.timeScale = 0;
            }

            else if(ExitUI.activeSelf == true)
            {
                ExitUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
