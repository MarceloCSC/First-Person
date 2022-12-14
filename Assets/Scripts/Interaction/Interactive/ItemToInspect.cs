using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
{
    public class ItemToInspect : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private ItemObject _item;

        #endregion

        #region Properties

        public ItemObject Root => _item;

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
        }

        #endregion
    }
}