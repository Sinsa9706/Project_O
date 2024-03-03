using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        itemData = Database.Item.Get(10010001);
        Debug.Log(itemData.Name);

        Inventory.Instance.AddItem(10010001);
        Debug.Log(Inventory.Instance.GetItem(10010001).Name);
    }
}
