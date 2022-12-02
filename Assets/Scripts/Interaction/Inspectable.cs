using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule
{
    public class Inspectable : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private ItemObject _item;

        #endregion

        #region Properties

        public ItemObject Item => _item;

        #endregion

        #region Public Methods

        public void StartInteraction()
        {
        }

        #endregion
    }
}