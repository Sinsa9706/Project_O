using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Box Item")]
    public List<ItemData> Items = new List<ItemData>();

    private NPC _NPC;

    private void Awake()
    {
        _NPC = GetComponent<NPC>();
    }

    private void Start()
    {
        BoxItemSet();
    }

    private void BoxItemSet()
    {
        Items.Add(Database.Item.Get(20010001));
        Items.Add(Database.Item.Get(20010002));
        Items.Add(Database.Item.Get(20010003));
        Items.Add(Database.Item.Get(20010004));
        Items.Add(Database.Item.Get(20010005));
        Items.Add(Database.Item.Get(20010006));
    }

    //public void PlusItemCount()
    //{
    //    switch()
    //
    //    int temp = int.Parse(_NPC.ItemCount[0].text);
    //    temp++;
    //
    //    _NPC.ItemCount[1].text = temp.ToString();
    //}

}
