using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[System.Serializable]
public class ItemData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private int _stack;
    [SerializeField] private int _maxstack;
    [SerializeField] private float _price;
    [SerializeField] private string _spritePath;

    public int Id => _id;
    public string Name => _name;
    public string Desciption => _description;
    public ItemType Type => _type;
    public int Stack
    {
        get
        {
            return _stack;
        }
        set
        {
            _stack = value;
        }
    }
    public int MaxStack => _maxstack;
    public float Price => _price;
    public string SpritePath => _spritePath;

    private Sprite _sprite;
    public Sprite Sprite
    {
        get
        {
            Debug.Log("읽음");
            _sprite = Resources.Load<Sprite>(SpritePath);

            return _sprite;
        }
    }
}
