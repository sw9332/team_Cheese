using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    // ��� ������ ������ - �ý��� �̸��� Loot, Scriptable obj�� ���� ����
    public string lootName;
    public int dropChance;  // ��� Ȯ��

    public Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
        
}
