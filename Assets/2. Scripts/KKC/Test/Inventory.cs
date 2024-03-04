using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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

    // �������� ��� �޼���
    // ���͸� ��ȣ�ۿ� ���� ��, ���� ������ �� �޼��带 ����ϸ� �κ��丮�� �������� ������.
    public void AddItem(int id)
    {
        if (HaveItem.ContainsKey(id))
        {
            ItemData itemData = HaveItem[id];
            itemData.Stack++;
            return;
            // Slot���� MaxStack Ȱ���ؼ� 20���� �Һн�Ű��
        }
        ItemData findItemData = Database.Item.Get(id);
        HaveItem.Add(id, findItemData);
    }

    // �������� ������� �޼��� (������� ��)
    public void RemoveItem(int id)
    {
        HaveItem.Remove(id);
    }

    public ItemData GetItem(int id)
    {
        return HaveItem[id];
    }
}
