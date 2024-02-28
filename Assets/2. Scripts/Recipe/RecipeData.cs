using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeData
{
    [SerializeField] private int _id;
    [SerializeField] private int _mat1;
    [SerializeField] private int _mat2;
    [SerializeField] private int _mat3;
    [SerializeField] private int _resultItem;

    public int ID => _id;
    public int Mat1 => _mat1;
    public int Mat2 => _mat2;
    public int Mat3 => _mat3;
    public int ResultItem => _resultItem;
}
