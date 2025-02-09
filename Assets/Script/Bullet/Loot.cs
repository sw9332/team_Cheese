using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    // 드롭 가능한 아이템 - 시스템 이름이 Loot, Scriptable obj로 만들어서 관리
    public string lootName;
    public int dropChance;  // 드롭 확률

    public Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
        
}
