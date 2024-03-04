using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class RecipeDB
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

    public Dictionary<int, RecipeData> Get()
    {
        return _recipes;
    }

    public RecipeData Get(int id)
    {
        if (_recipes.ContainsKey(id))
            return _recipes[id];

        return null;
    }

    public IEnumerator DbEnumerator()
    {
        return _recipes.GetEnumerator();
    }
}
