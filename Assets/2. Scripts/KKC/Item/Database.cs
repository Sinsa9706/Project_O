using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static ItemDB _item;
    private static RecipeDB _recipe;

    public static ItemDB Item
    {
        get
        {
            if (_item == null)
                _item = new ItemDB();

            return _item;
        }
    }

    public static RecipeDB Recipe
    {
        get
        {
            if(_recipe == null)
                _recipe = new RecipeDB();

            return _recipe;
        }
    }
}
