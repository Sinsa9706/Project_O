using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class MatSlot
{
    public ItemData item;
    public int quantity;
}

public class ResultSlot
{
    public ItemData item;
}

public class RecipeSlot
{
    public ItemData item;
}

public class CraftSystem : MonoBehaviour
{
    public RecipeSlot[] recipeSlots;
    public ResultSlot[] resultItemSlot;
    public MatSlot[] matItemSlots;

    public GameObject RecipeInfo;
    public GameObject RecipeSelect;

    [Header("ResultItem")]
    private RecipeSlot selectedRecipe;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemPrice;
    public TextMeshProUGUI selectedItemDescription;

    public static CraftSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void TotalItem(int id)
    {
        int total = 0;
        for(int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            if (Inventory.Instance.slots[i].item.Id == id)
            {
                total += Inventory.Instance.slots[i].quantity;
            }
        }
        return;
    }

    //public void CraftItem()
    //{
    //    RecipeDB recipeDB = new RecipeDB();
    //    Dictionary<int, RecipeData> _recipes = recipeDB.Get();
    //    for(int i = 0; i < _recipes.Count; i++) 
    //    {
    //        for(int j = 0; i < Invent)
    //    }
    //}



    public void MatItem(int id)
    {

    }

    public void SelectRecipe()
    {
        RecipeSelect.SetActive(false);
        RecipeInfo.SetActive(true);
    }

    // 해야될 것
    // 1. 레시피 데이터 가져오기
    // 2. 레시피 데이터와 인벤토리 슬롯 데이터 대조하기
    // 3. 대조했을 때, 인벤토리에 모두 존재 한다면, 레시피 데이터의 resultItem 생성하기
}
