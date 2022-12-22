using UnityEngine;

namespace An01malia.FirstPerson.ItemModule.Items
{
    public class ItemToCarry : MonoBehaviour, IItem
    {
        #region Fields

        [SerializeField] private ItemObject _item;

        #endregion

        #region Properties

        public ItemObject Root => _item;

        #endregion
    }
}