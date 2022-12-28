using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UserInterfaceModule.Inventory
{
    public class IconToDrag : SimpleSingleton<IconToDrag>
    {
        #region Fields

        private bool _isBeingDragged;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Activate(false);
        }

        private void Update()
        {
            if (!_isBeingDragged) return;

            transform.position = PlayerInput.CursorInputValues;
        }

        #endregion

        #region Public Methods

        public void Activate(bool active)
        {
            if (active)
            {
                transform.SetAsLastSibling();

                GetComponent<Image>().enabled = true;

                _isBeingDragged = true;
            }
            else
            {
                GetComponent<Image>().enabled = false;

                _isBeingDragged = false;
            }
        }

        public void ChangeIcon(Slot slot)
        {
            GetComponent<Image>().sprite = slot.Item.Root.Icon;
        }

        #endregion
    }
}