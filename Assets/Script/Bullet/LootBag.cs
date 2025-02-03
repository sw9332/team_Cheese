using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LootBag : MonoBehaviour
{
    public GameObject[] droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems()    // item ������ ��� ����
    {
        int randomNum = Random.Range(1, 101);     // 1-100 �ۼ�Ʈ Ȯ�� ����
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNum <= item.dropChance)   // ���� �� ���� < ������ ��� Ȯ�� -> �������� �����. 
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
                for(int i=0; i < droppedItemPrefab.Length; i++)
                {
                    GameObject lootGameObject = Instantiate(droppedItemPrefab[i], spawnPosition, Quaternion.identity);

                    lootGameObject.tag = droppedItem.name;
                    lootGameObject.name = droppedItem.name;
                }
            }
        }
    }
}

    