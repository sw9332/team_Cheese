using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContentManager : MonoBehaviour
{
    [Header("튜토리얼")]
    [SerializeField]
    public Dialogue d_prologue;
    [SerializeField]
    public Dialogue d_cake;
    [SerializeField]
    public Dialogue d_not_a_cake;
    [SerializeField]
    public Dialogue d_photo;
    [SerializeField]
    public Dialogue d_camera;

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
}