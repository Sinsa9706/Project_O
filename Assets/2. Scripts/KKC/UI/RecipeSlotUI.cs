using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VolumeComponent;

public class RecipeSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI recipeName;
    private RecipeSlot recipeSlot;

    public int index;

    public void Set(RecipeSlot slot)
    {
        recipeSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = Resources.Load<Sprite>(slot.item.SpritePath);
        recipeName.text = slot.item.Name;
    }

    public void OnButtonClick()
    {
        CraftSystem.Instance.SelectRecipe();
    }

    // 버튼을 눌렀을 때, 필요한 데이터를 뿌려주는 것
}
