using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance = null;
    private Dictionary<int, ItemData> HaveItem = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AddItem(int id)
    {
        if (HaveItem.ContainsKey(id))
        {
            ItemData itemData = HaveItem[id];
            itemData.Stack++;
            return;
            // Slot에서 MaxStack 활용해서 20개씩 소분시키기
        }
        ItemData findItemData = Database.Item.Get(id);
        HaveItem.Add(id, findItemData);
    }

    public void RemoveItem(int id)
    {
        HaveItem.Remove(id);
    }

    public ItemData GetItem(int id)
    {
        return HaveItem[id];
    }
}
