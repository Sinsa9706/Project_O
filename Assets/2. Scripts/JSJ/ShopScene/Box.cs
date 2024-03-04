using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Box Item")]
    public List<ItemData> Items = new List<ItemData>();

    private NPC _NPC;

    public Dictionary<string, TMP_Text> FindText = new();

    private int clickItemId;


    private void Awake()
    {
        _NPC = GetComponent<NPC>();
    }

    private void Start()
    {
        BoxItemSet();
        BoxItemUISet();
    }

    private void BoxItemUISet()
    {
        FindText.Add("에메랄드 에센스", _NPC.ItemCount[0]);
        FindText.Add("루비 물약", _NPC.ItemCount[1]);
        FindText.Add("별가루 캔디", _NPC.ItemCount[2]);
        FindText.Add("핑크 프로틴 쉐이크", _NPC.ItemCount[3]);
        FindText.Add("오스틴-코기 포도잼", _NPC.ItemCount[4]);
        FindText.Add("민트 스톡", _NPC.ItemCount[5]);
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
    public void GetName(GameObject myObject)//인벤버튼에 연결
    {
        string name = myObject.name;
        //clickItemId = 
        PlusItemCount(name);
        MinusInventory();
    }

    private void PlusItemCount(string name)
    {
        if (FindText.TryGetValue(name, out var text))
        {
            int temp = int.Parse(FindText[name].text);
            temp++;
            FindText[name].text = temp.ToString();
        }
        else
        {
            Debug.Log("해당 키 없음");
            return;
        }    
    }

    private void MinusInventory()
    {
        Inventory.Instance.RemoveItem(clickItemId);
    }
}
