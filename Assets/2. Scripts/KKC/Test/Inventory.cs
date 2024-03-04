using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Start()
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
        if(callbackContext.phase == InputActionPhase.Started)
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
            // ���콺 Ŀ�� ������
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            // ���콺 Ŀ�� ������
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    // �������� ��� �޼���
    // ���͸� ��ȣ�ۿ� ���� ��, ���� ������ �� �޼��带 ����ϸ� �κ��丮�� �������� ������.
    public void AddItem(int id)
    {
        if (HaveItem.ContainsKey(id))
        {
            ItemData itemData = HaveItem[id];
            itemData.Stack++;
            UpdateUI();
            return;
            // Slot���� MaxStack Ȱ���ؼ� 20���� �Һн�Ű��
        }
        ItemData findItemData = Database.Item.Get(id);
        HaveItem.Add(id, findItemData);

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = HaveItem[id];
            emptySlot.quantity = HaveItem[id].Stack;
            UpdateUI();
            return;
        }
        return;
    }

    // �������� ������� �޼��� (������� ��)
    public void RemoveItem(int id)
    {
        HaveItem.Remove(id);
    }

    private void RemoveSelectedItem()
    {

    }

    public ItemData GetItem(int id)
    {
        return HaveItem[id];
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
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.Name;
        selectedItemPrice.text = selectedItem.item.Price.ToString();
        selectedItemDescription.text = selectedItem.item.Desciption;

        //for(int i = 0; i < selectedItem.item.)

        itemInfo.SetActive(true);
    }

    private void ClearSelectItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemPrice.text = string.Empty;
        selectedItemDescription.text = string.Empty;
    }
}
