using UnityEngine;
using An01malia.FirstPerson.Inventory;

namespace An01malia.FirstPerson.Crafting
{

    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/New Recipe")]
    public class Recipe : ScriptableObject
    {

        [SerializeField] Item itemToCraft = null;
        [SerializeField] Item[] ingredients = new Item[4];
        [Space]
        [SerializeField] bool isAvailable = false;


        #region Properties
        public Item ItemToCraft => itemToCraft;
        public Item[] Ingredients => ingredients;
        public bool HasBeenLearned
        {
            get => isAvailable;
            set
            {
                isAvailable = value;
            }
        }
        #endregion


        public Item CraftedItem()
        {
            return new Item(itemToCraft.Root, itemToCraft.Amount);
        }

    }

}
