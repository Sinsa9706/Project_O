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
    [SerializeField] private float _price;
    [SerializeField] private string _spritePath;

    public int id => _id;
    public string name => _name;
    public string desciption => _description;
    public ItemType type => _type;
    public int stack => _stack;
    public float price => _price;
    public string spritePath => _spritePath;


}
