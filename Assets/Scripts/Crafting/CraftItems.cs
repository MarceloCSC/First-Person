using UnityEngine;
using An01malia.FirstPerson.Inventory;

namespace An01malia.FirstPerson.Crafting
{

    public class CraftItems : MonoBehaviour
    {

        [SerializeField] RecipeDatabase recipeDatabase = null;

        public static GameObject Panel;
        public static Transform RecipesGrid;


        #region Cached references
        private PlayerInventory playerInventory;
        #endregion


        private void Start()
        {
            SetReferences();
        }

        public void CheckRecipes()
        {
            recipeDatabase.UpdateDatabase();
        }

        public void Craft(Recipe recipe)
        {
            if (HasIngredients(recipe))
            {
                foreach (Item ingredient in recipe.Ingredients)
                {
                    playerInventory.RemoveItem(ingredient);
                }

                playerInventory.AddItems(recipe.CraftedItem());
            }
            else
            {
                print("Not enough ingredients!");
            }
        }

        private bool HasIngredients(Recipe recipe)
        {
            foreach (Item ingredient in recipe.Ingredients)
            {
                if (playerInventory.CountItems(ingredient) < ingredient.Amount)
                {
                    return false;
                }
            }

            return true;
        }

        private void SetReferences()
        {
            RecipesGrid = Panel.transform.GetChild(0);
            playerInventory = References.Player.GetComponent<PlayerInventory>();
        }

    }

}
