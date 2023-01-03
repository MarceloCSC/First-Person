using An01malia.FirstPerson.ItemModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.CraftingModule
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/New Recipe")]
    public class RecipeObject : ScriptableObject
    {
        #region Fields

        [SerializeField] private InventoryItem _itemToCraft;
        [SerializeField] private InventoryItem[] _ingredients = new InventoryItem[4];

        [Space]
        [SerializeField] private bool _isAvailable;

        #endregion

        #region Properties

        public InventoryItem ItemToCraft => _itemToCraft;
        public InventoryItem[] Ingredients => _ingredients;

        public bool HasBeenLearned
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
            }
        }

        #endregion

        #region Public Methods

        public InventoryItem CraftedItem()
        {
            return new InventoryItem(_itemToCraft.Root, _itemToCraft.Amount);
        }

        #endregion
    }
}