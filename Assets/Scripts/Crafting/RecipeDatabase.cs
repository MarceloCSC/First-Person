using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.CraftingModule
{
    [CreateAssetMenu(fileName = "NewRecipeDatabase", menuName = "Crafting/New Recipe Database")]
    public class RecipeDatabase : ScriptableObject
    {
        #region Fields

        [SerializeField] private GameObject _recipeSlotsPrefab;

        public RecipeObject[] AllRecipes;
        public List<RecipeObject> AvailableRecipes = new();

        #endregion

        #region Public Methods

        public void UpdateDatabase()
        {
            foreach (RecipeObject recipe in AllRecipes)
            {
                if (recipe.HasBeenLearned && !AvailableRecipes.Contains(recipe))
                {
                    AvailableRecipes.Add(recipe);
                    CreateRecipe(recipe);
                }
            }
        }

        public void CreateRecipe(RecipeObject recipe)
        {
            GameObject recipeSlots = Instantiate(_recipeSlotsPrefab, CraftItems.RecipesGrid);

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

        #endregion
    }
}