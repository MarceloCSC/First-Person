using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.Crafting
{

    [CreateAssetMenu(fileName = "NewRecipeDatabase", menuName = "Crafting/New Recipe Database")]
    public class RecipeDatabase : ScriptableObject
    {

        [SerializeField] GameObject recipeSlotsPrefab = null;

        public Recipe[] allRecipes;
        public List<Recipe> availableRecipes = new List<Recipe>();


        public void UpdateDatabase()
        {
            foreach (Recipe recipe in allRecipes)
            {
                if (recipe.HasBeenLearned && !availableRecipes.Contains(recipe))
                {
                    availableRecipes.Add(recipe);
                    CreateRecipe(recipe);
                }
            }
        }

        public void CreateRecipe(Recipe recipe)
        {
            GameObject recipeSlots = Instantiate(recipeSlotsPrefab, CraftItems.RecipesGrid);

            CraftingSlot[] slots = recipeSlots.GetComponentsInChildren<CraftingSlot>();

            slots[0].Item = recipe.ItemToCraft;

            for (int i = 1; i < slots.Length; i++)
            {
                if (recipe.Ingredients[i - 1].Amount > 0)
                {
                    slots[i].Item = recipe.Ingredients[i - 1];
                }
            }
        }

    }

}
