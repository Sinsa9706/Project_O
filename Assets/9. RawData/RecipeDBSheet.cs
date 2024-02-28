using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "6. SO/ItemSO", ExcelName = "RecipeDBSheet")]
public class RecipeDBSheet : ScriptableObject
{
    public List<RecipeData> Recipes; // Replace 'EntityType' to an actual type that is serializable.
}
