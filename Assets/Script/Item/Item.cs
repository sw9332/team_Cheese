using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { 일반, 회복, 사용 }

[System.Serializable]
public class Item
{
    public string id;
    public Type type;
    public GameObject prefab;
    public Sprite sprite;
}
