using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpriteChange : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Dictionary<int, ItemData> itemDatas;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemDatas = Database.Item.Get();
    }

    private void Start()
    {
        Debug.Log(itemDatas[0].Name);
        ItemData itemData = Database.Item.Get(10010001);
        spriteRenderer.sprite = Resources.Load<Sprite>(itemData.SpritePath);
    }
}
