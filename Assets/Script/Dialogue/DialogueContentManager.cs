using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContentManager : MonoBehaviour
{
    [Header("�̺�Ʈ")]
    public Dialogue d_event1;
    public Dialogue d_event2;
    public Dialogue d_event3;
    public Dialogue d_event4;

    [Header("Ʃ�丮��")]
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

    [Header("CutScene_1 (Ʃ�丮�� ����)")]
    [SerializeField]
    public Dialogue cutScene_1_1;
    [SerializeField]
    public Dialogue cutScene_1_2;

    [Header("CutScene_2 (â�� NPC 1)")]
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

    [Header("CutScene_3 (����)")]
    [SerializeField]
    public Dialogue d_Bos1;
    [SerializeField]
    public Dialogue d_Bos2;

    [Header("CutScene_4 (������ �浹 �� ��ȭ)")]
    [SerializeField]
    public Dialogue cutScene_4_1;
    [SerializeField]
    public Dialogue cutScene_4_2;
    [SerializeField]
    public Dialogue cutScene_4_3;

    [Header("CutScene_5 (â�� NPC 2)")]
    [SerializeField]
    public Dialogue cutScene_5_1;
    [SerializeField]
    public Dialogue cutScene_5_2;
    [SerializeField]
    public Dialogue cutScene_5_3;

    [Header("CutScene_6 (������ ��)")]
    public Dialogue cutScene_6_1;
    public Dialogue cutScene_6_2;
    public Dialogue cutScene_6_3;
    public Dialogue cutScene_6_4;
    public Dialogue cutScene_6_5;

    [Header("CutScene_7 (Demo ������)")]
    public Dialogue cutScene_7_1;
    public Dialogue cutScene_7_2;
}