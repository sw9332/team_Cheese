using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContentManager : MonoBehaviour
{
    [Header("Chapter 1 Event")]
    public Dialogue d_event1;
    public Dialogue d_event2;
    public Dialogue d_event3;
    public Dialogue d_event4;

    [Header("Chapter 2 Event")]
    public Dialogue chapter2_event1;
    public Dialogue chapter2_event2;
    public Dialogue chapter2_event3;
    public Dialogue chapter2_event4;
    public Dialogue chapter2_event5;
    public Dialogue chapter2_event6;

    [Header("튜토리얼")]
    [SerializeField]
    public Dialogue d_prologue;
    [SerializeField]
    public Dialogue d_cake;
    [SerializeField]
    public Dialogue d_not_camera;
    [SerializeField]
    public Dialogue d_not_a_cake;
    [SerializeField]
    public Dialogue d_photo;
    [SerializeField]
    public Dialogue d_camera;
    [SerializeField]
    public Dialogue d_album1;
    [SerializeField]
    public Dialogue d_album2;

    [Header("CutScene_1 (튜토리얼 종료)")]
    [SerializeField]
    public Dialogue cutScene_1_1;
    [SerializeField]
    public Dialogue cutScene_1_2;

    [Header("CutScene_2 (창고 NPC 1)")]
    [SerializeField]
    public Dialogue d_Demo_1;
    [SerializeField]
    public Dialogue d_Demo_2;
    [SerializeField]
    public Dialogue d_Demo_3;

    [SerializeField]
    public Dialogue d_Stage_1;
    [SerializeField]
    public Dialogue d_Stage_2;
    [SerializeField]
    public Dialogue d_Stage_3;

    [Header("CutScene_3 (보스)")]
    [SerializeField]
    public Dialogue d_Bos1;
    [SerializeField]
    public Dialogue d_Bos2;

    [Header("CutScene_4 (보스와 충돌 후 대화)")]
    [SerializeField]
    public Dialogue cutScene_4_1;
    [SerializeField]
    public Dialogue cutScene_4_2;
    [SerializeField]
    public Dialogue cutScene_4_3;

    [Header("CutScene_5 (창고 NPC 2)")]
    [SerializeField]
    public Dialogue cutScene_5_1;
    [SerializeField]
    public Dialogue cutScene_5_2;
    [SerializeField]
    public Dialogue cutScene_5_3;

    [Header("CutScene_6 (보스전 후)")]
    public Dialogue cutScene_6_1;
    public Dialogue cutScene_6_2;
    public Dialogue cutScene_6_3;
    public Dialogue cutScene_6_4;
    public Dialogue cutScene_6_5;

    [Header("CutScene_7")]
    public Dialogue cutScene_7_1;
    public Dialogue cutScene_7_2;

    [Header("CutScene_8")]
    public Dialogue cutScene_8_1;
    public Dialogue cutScene_8_2;
    public Dialogue cutScene_8_3;
    public Dialogue cutScene_8_4;

    [Header("CutScene_10")]
    public Dialogue cutScene_10_1;
    public Dialogue cutScene_10_2;
    public Dialogue cutScene_10_3;
    public Dialogue cutScene_10_4;
    public Dialogue cutScene_10_5;
    public Dialogue cutScene_10_6;
}