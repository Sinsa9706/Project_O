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

    // 아이템을 얻는 메서드
    // 몬스터를 상호작용 했을 때, 다음 순서에 이 메서드를 사용하면 인벤토리에 아이템이 들어가진다.
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

    // 아이템이 사라지는 메서드 (사용했을 때)
    public void RemoveItem(int id)
    {
        HaveItem.Remove(id);
    }

    public ItemData GetItem(int id)
    {
        return HaveItem[id];
    }
}
