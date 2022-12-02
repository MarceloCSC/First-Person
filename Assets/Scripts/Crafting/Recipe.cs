using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.Crafting
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/New Recipe")]
    public class Recipe : ScriptableObject
    {
        #region Fields

        [SerializeField] private Item _itemToCraft;
        [SerializeField] private Item[] _ingredients = new Item[4];

        [Space]
        [SerializeField] private bool _isAvailable;

        #endregion

        #region Properties

        public Item ItemToCraft => _itemToCraft;
        public Item[] Ingredients => _ingredients;

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

        public Item CraftedItem()
        {
            return new Item(_itemToCraft.Root, _itemToCraft.Amount);
        }

        #endregion
    }
}