using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDB : MonoBehaviour
{
    private Dictionary<int, RecipeData> _recipes = new();

    public RecipeDB()
    {
        var res = Resources.Load<RecipeDBSheet>("SO/ItemSO/RecipeDBSheet");
        var recipeSO = Object.Instantiate(res);
        var recipes = recipeSO.Recipes;

        if (recipes == null || recipes.Count <= 0)
            return;

        var recipesCount = recipes.Count;
        for (int i = 0; i < recipesCount; i++)
        {
            var recipe = recipes[i];

            if (_recipes.ContainsKey(recipe.Id))
                _recipes[recipe.Id] = recipe;
            else
                _recipes.Add(recipe.Id, recipe);

        }
    }
}
