using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemDB;

    private Dictionary<string, Item> itemDictionary;

    public Item GetItem(string id)
    {
        return itemDictionary.ContainsKey(id) ? itemDictionary[id] : null;
    }

    public Sprite GetItemSprite(string id)
    {
        Item item = GetItem(id);
        return item != null ? item.sprite : null;
    }

    void Awake()
    {
        itemDictionary = new Dictionary<string, Item>();
        foreach (Item item in itemDB)
        {
            itemDictionary[item.id] = item;
        }
    }
}