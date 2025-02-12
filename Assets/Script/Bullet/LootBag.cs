using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems()    // item 여러개 드랍 가능
    {
        int randomNum = Random.Range(1, 101);     // 1-100 퍼센트 확률 설정
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNum <= item.dropChance)   // 랜덤 값 숫자 < 아이템 드랍 확률 -> 아이템이 드랍됨. 
            {
                possibleItems.Add(item);
            }
            // below code: only 1 item dropped fucntion 
            /*
            if (possibleItems.Count > 0)
            {
                Loot droppedItems = possibleItems[Random.Range(0, possibleItems.Count)];
                return droppeditem;
            }
            */
        }
        return possibleItems;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
       List<Loot> droppedItems = GetDroppedItems(); // Dropped Items
        if (droppedItems != null)    // If items are dropped
        {
            foreach (Loot droppedItem in droppedItems)
            {
                    GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
                    lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

                    lootGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

                    lootGameObject.tag = droppedItem.name;
                    lootGameObject.name = droppedItem.name;
            }
        }
    }
}

    