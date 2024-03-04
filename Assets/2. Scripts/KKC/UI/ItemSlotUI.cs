using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private ItemSlot curSlot;
    private Outline outline;

    public int index;

    public void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = Resources.Load<Sprite>(slot.item.SpritePath);
        quantityText.text = slot.quantity >= 1 ? slot.quantity.ToString() : string.Empty;
        // Stack이 MaxStack을 넘어가면 새로운 슬롯에 표시
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.Instance.SelectItem(index);
    }
}
