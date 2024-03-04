using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WorkTable : MonoBehaviour
{
    [Header("WorkTable Item")]
    public List<ItemData> SelectItems = new List<ItemData>();
    public List<RecipeData> ItemRecipe = new List<RecipeData>();
    public List<ItemData> SourceItems = new List<ItemData>();

    [Header("MakeUI")]
    public Image MakeItemImage;
    public TMP_Text Name;
    public TMP_Text Description;

    [Header("MakeUIItem")]
    public List<Image> UseItemImage;
    public List<TMP_Text> UseItemName;
    public List<TMP_Text> UseItemCountText;

    [Header("Finish")]
    public GameObject FinishUI;


    private int _selectItemIndex;
    private int _sourceId1;
    private int _sourceId2;
    private int _sourceId3;

    private UIManager _UIManager;

    private void Awake()
    {
        _UIManager = this.gameObject.GetComponent<UIManager>();
    }

    private void Start()
    {
        WorkTableItemSet();
        ItemRecipeSet();
        SourceItemSet();
    }

    private void WorkTableItemSet()
    {
        SelectItems.Add(Database.Item.Get(20010001));
        SelectItems.Add(Database.Item.Get(20010002));
        SelectItems.Add(Database.Item.Get(20010003));
        SelectItems.Add(Database.Item.Get(20010004));
        SelectItems.Add(Database.Item.Get(20010005));
        SelectItems.Add(Database.Item.Get(20010006));
    }

    private void ItemRecipeSet()
    {
        ItemRecipe.Add(Database.Recipe.Get(10000001));
        ItemRecipe.Add(Database.Recipe.Get(10000002));
        ItemRecipe.Add(Database.Recipe.Get(10000003));
        ItemRecipe.Add(Database.Recipe.Get(10000004));
        ItemRecipe.Add(Database.Recipe.Get(10000005));
        ItemRecipe.Add(Database.Recipe.Get(10000006));
    }

    private void SourceItemSet()
    {
        SourceItems.Add(Database.Item.Get(10010001));
        SourceItems.Add(Database.Item.Get(10010002));
        SourceItems.Add(Database.Item.Get(10010003));
        SourceItems.Add(Database.Item.Get(10010004));
        SourceItems.Add(Database.Item.Get(10010005));
        SourceItems.Add(Database.Item.Get(10010006));
        SourceItems.Add(Database.Item.Get(10020001));
        SourceItems.Add(Database.Item.Get(10020002));
        SourceItems.Add(Database.Item.Get(10020003));
        SourceItems.Add(Database.Item.Get(10020004));
        SourceItems.Add(Database.Item.Get(10020005));
        SourceItems.Add(Database.Item.Get(10020006));
        SourceItems.Add(Database.Item.Get(10030001));
        SourceItems.Add(Database.Item.Get(10030002));
        SourceItems.Add(Database.Item.Get(10030003));
        SourceItems.Add(Database.Item.Get(10030004));
    }


    public void SelectMakeItem(int index)
    {
        _selectItemIndex = index;

        MakeItemImage.overrideSprite = SelectItems[index].Sprite;
        Name.text = SelectItems[index].Name;
        Description.text = SelectItems[index].Desciption;

        //Debug.Log(ItemRecipe[index].Mat1);

        // if (slots[i].item == Database.Item.Get(id))

        _sourceId1 = ItemRecipe[index].Mat1;
        _sourceId2 = ItemRecipe[index].Mat2;
        _sourceId3 = ItemRecipe[index].Mat3;

        for (int i = 0; i < SourceItems.Count; ++i)
        {
            if (SourceItems[i] == Database.Item.Get(_sourceId1))
            {
                UseItemImage[0].overrideSprite = SourceItems[i].Sprite;
                UseItemImage[0].SetNativeSize();
                UseItemName[0].text = SourceItems[i].Name;
            }
        }
        for (int i = 0; i < SourceItems.Count; ++i)
        {
            if (SourceItems[i].Id == _sourceId2)
            {
                UseItemImage[1].overrideSprite = SourceItems[i].Sprite;
                UseItemImage[1].SetNativeSize();
                UseItemName[1].text = SourceItems[i].Name;
            }
        }
        for (int i = 0; i < SourceItems.Count; ++i)
        {
            if (SourceItems[i].Id == _sourceId3)
            {
                UseItemImage[2].overrideSprite = SourceItems[i].Sprite;
                UseItemImage[2].SetNativeSize();
                UseItemName[2].text = SourceItems[i].Name;
            }
        }

        WorkTableUIUpdate();
    }

    private void WorkTableUIUpdate()
    {
        ItemSlot slot = Inventory.Instance.ItemCheck(_sourceId1);
        if (slot != null)
            UseItemCountText[0].text = "1 /" + slot.quantity;
        else
            UseItemCountText[0].text = "1 /0";


        slot = Inventory.Instance.ItemCheck(_sourceId2);
        if (slot != null)
            UseItemCountText[1].text = "1 /" + slot.quantity;
        else
            UseItemCountText[1].text = "1 /0";


        slot = Inventory.Instance.ItemCheck(_sourceId3);
        if (slot != null)
            UseItemCountText[2].text = "1 /" + slot.quantity;
        else
            UseItemCountText[2].text = "1 /0";
    }

    public void MakeClick()
    {
        ItemSlot slot;

        slot = Inventory.Instance.ItemCheck(_sourceId1);
        if (slot == null) return;

        slot = Inventory.Instance.ItemCheck(_sourceId2);
        if (slot == null) return;

        slot = Inventory.Instance.ItemCheck(_sourceId3);
        if (slot == null) return;

        slot = Inventory.Instance.ItemCheck(_sourceId1);
        slot.quantity--;

        slot = Inventory.Instance.ItemCheck(_sourceId2);
        slot.quantity--;

        slot = Inventory.Instance.ItemCheck(_sourceId3);
        slot.quantity--;

        Inventory.Instance.AddItem(SelectItems[_selectItemIndex].Id);
        Inventory.Instance.UpdateUI();
        WorkTableUIUpdate();

        MakeFinish();
    }

    private void MakeFinish()
    {

    }
    //[Header("WorkTable Item")]
    //public List<ItemData> SelectItems = new List<ItemData>();
    //public List<RecipeData> ItemRecipe = new List<RecipeData>();
    //public List<ItemData> SourceItems = new List<ItemData>();

    //[Header("MakeUI")]
    //public Image MakeItemImage;
    //public TMP_Text Name;
    //public TMP_Text Description;

    //[Header("MakeUIItem")]
    //public List<Image> UseItemImage;
    //public List<TMP_Text> UseItemName;
    //public List<TMP_Text> UseItemCountText;


    //private int _selectItemIndex;
    //private int _sourceId1;
    //private int _sourceId2;
    //private int _sourceId3;
}
