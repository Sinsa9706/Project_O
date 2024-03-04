using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public GameObject itemInfo;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemPrice;
    public TextMeshProUGUI selectedItemDescription;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory Instance = null;
    private Dictionary<int, ItemData> HaveItem = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Init()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        ClearSelectItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == Database.Item.Get(id))
            {
                if (slots[i].quantity < Database.Item.Get(id).MaxStack)
                {
                    slots[i].quantity++;
                    UpdateUI();
                    Debug.Log("a" + i);
                    return;
                }
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = Database.Item.Get(id);
            emptySlot.quantity = 1;
            UpdateUI();
            Debug.Log("b" + id);

        }
    }

    public void RemoveItem(int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.Id == Database.Item.Get(id).Id)
            {
                slots[i].quantity--;
            }
        }
    }

    private void RemoveSelectedItem()
    {

    }

    public ItemSlot GetItem(int i)
    {
        Debug.Log(slots.Length);
        return slots[i];
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
        {
            itemInfo.SetActive(false);
            return;
        }
        else
        {
            selectedItem = slots[index];
            selectedItemIndex = index;

            selectedItemName.text = selectedItem.item.Name;
            selectedItemPrice.text = selectedItem.item.Price == 0 ? "-" : selectedItem.item.Price.ToString();
            selectedItemDescription.text = selectedItem.item.Desciption;

            //for(int i = 0; i < selectedItem.item.)

            itemInfo.SetActive(true);
        }

    }

    private void ClearSelectItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemPrice.text = string.Empty;
        selectedItemDescription.text = string.Empty;
    }
}
