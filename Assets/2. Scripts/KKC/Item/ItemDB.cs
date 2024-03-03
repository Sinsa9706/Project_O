using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    private Dictionary<int, ItemData> _items = new();

    public ItemDB()
    {
        var res = Resources.Load<ItemDBSheet>("SO/ItemSO/ItemDBSheet");
        var itemSO = Object.Instantiate(res);
        var entities = itemSO.Items;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for(int i = 0; i < entityCount; i++)
        {
            var item = entities[i];

            if (_items.ContainsKey(item.Id))
                _items[item.Id] = item;
            else
                _items.Add(item.Id, item);
        }
    }

    public Dictionary<int, ItemData> Get()
    {
        return _items;
    }

    public ItemData Get(int id)
    {
        if(_items.ContainsKey(id))
            return _items[id];
        
        return null;
    }

    public IEnumerator DbEnumerator()
    {
        return _items.GetEnumerator();
    }
}
