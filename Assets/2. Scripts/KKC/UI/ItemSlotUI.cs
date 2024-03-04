using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        quantityText.text = slot.quantity >= slot.item.Stack ? slot.quantity.ToString() : string.Empty;
        // 비어있는 슬롯에 새롭게 표시
        if (slot.quantity >= slot.item.MaxStack)
        {
            index++;

            if(slot != null)
            {
                while(slot == null)
                {
                    index++;
                }
                return;
            }
        }
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {

    }
}
