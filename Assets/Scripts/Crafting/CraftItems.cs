using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.ItemModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.CraftingModule
{
    public class CraftItems : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RecipeDatabase _recipeDatabase;

        public static GameObject Panel;
        public static Transform RecipesGrid;

        private PlayerInventory _playerInventory;

        #endregion

        #region Unity Methods

        private void Start()
        {
            SetReferences();
        }

        #endregion

        #region Public Methods

        public void CheckRecipes()
        {
            _recipeDatabase.UpdateDatabase();
        }

        public void Craft(Recipe recipe)
        {
            if (HasIngredients(recipe))
            {
                foreach (InventoryItem ingredient in recipe.Ingredients)
                {
                    _playerInventory.RemoveItem(ingredient);
                }

                _playerInventory.AddItems(recipe.CraftedItem());
            }
            else
            {
                print("Not enough ingredients!");
            }
        }

        #endregion

        #region Private Methods

        private bool HasIngredients(Recipe recipe)
        {
            foreach (InventoryItem ingredient in recipe.Ingredients)
            {
                if (_playerInventory.CountItems(ingredient) < ingredient.Amount)
                {
                    return false;
                }
            }

            return true;
        }

        private void SetReferences()
        {
            RecipesGrid = Panel.transform.GetChild(0);
            _playerInventory = Player.Transform.GetComponent<PlayerInventory>();
        }

        #endregion
    }
}