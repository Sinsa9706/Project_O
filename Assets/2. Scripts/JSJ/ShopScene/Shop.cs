using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    [Header("Shop Item")]
    public List<ItemData> Items = new List<ItemData>();

    [Header("Shop UI")]
    public TMP_Text DescName;
    public TMP_Text Desc;
    public Image DescSprite;

    [Header("Sprite")]
    public List<Sprite> Sprites;

    public static int ItemGold;

    private int clickItemId;

    private void Start()
    {
        ShopItemSet();
    }

    private void ShopItemSet()
    {
        Items.Add(Database.Item.Get(10030001));
        Items.Add(Database.Item.Get(10030002));
        Items.Add(Database.Item.Get(10030003));
        Items.Add(Database.Item.Get(10030004));
    }

    public void ClickItemGoldCheck(int num)
    {
        ItemGold = (int)(Items[num].Price);
        ItemUIChange(num);
        clickItemId = Items[num].Id;
    }

    private void ItemUIChange(int num)
    {
        DescSprite.enabled = true;

        string name = Items[num].Name;
        string description = Items[num].Desciption;
        Sprite sprite = Sprites[num];

        DescName.text = name;
        Desc.text = description;
        DescSprite.sprite = sprite;
        DescSprite.SetNativeSize();
    }

    public void BuyItem()
    {
        for(int i = 0; i < UIManager.Count; ++i)
        {
            Inventory.Instance.AddItem(clickItemId);

        }
    }
}
