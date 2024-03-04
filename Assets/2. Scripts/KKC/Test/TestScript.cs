using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        Inventory.Instance.AddItem(10010001);
        for(int i = 0; i < 20; i++)
        {
            Inventory.Instance.AddItem(10010001);
        }
        Inventory.Instance.AddItem(10010002);
        Inventory.Instance.AddItem(10030004);
        Debug.Log(Inventory.Instance.GetItem(0).item.Name);
    }
}
