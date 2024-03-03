using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance = null;
    private Dictionary<int, ItemData> StorageItem = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StoreItem(int id)
    {
        if (StorageItem.ContainsKey(id))
        {
            ItemData itemData = StorageItem[id];
            itemData.Stack++;
            return;
        }
        ItemData findItemData = Database.Item.Get(id);
        StorageItem.Add(id, findItemData);
    }
}
