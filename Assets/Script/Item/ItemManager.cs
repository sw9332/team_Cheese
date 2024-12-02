using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item BrownTeddyBear;
    public Item YellowTeddyBear;
    public Item PinkTeddyBear;
    public Item Cake;
    public Item NPC;

    public Item GetItem(string itemName)
    {
        switch(itemName)
        {
            case "DroppedBrownTeddyBear": return BrownTeddyBear;
            case "BrownTeddyBear": return BrownTeddyBear;
            case "YellowTeddyBear": return YellowTeddyBear;
            case "PinkTeddyBear": return PinkTeddyBear;
            case "Cake": return Cake;
            case "NPC": return NPC;
            default: return null;
        }
    }

    public Sprite GetItemSprite(string itemName)
    {
        Item item = GetItem(itemName);
        return item != null ? item.sprite : null;
    }
}