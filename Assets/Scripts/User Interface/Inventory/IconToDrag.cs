using An01malia.FirstPerson.InventoryModule;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UserInterfaceModule.Inventory
{
    public class IconToDrag : MonoBehaviour
    {
        #region Fields

        private bool _beingDragged;
        private PlayerInput _playerInput;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // MUDAR!!
            _playerInput = FindObjectOfType<PlayerInput>();
        }

        private void Start()
        {
            Activate(false);
        }

        private void Update()
        {
            if (!_beingDragged) return;

            transform.position = _playerInput.CursorInputValues;
        }

        #endregion

        #region Public Methods

        public void Activate(bool active)
        {
            if (active)
            {
                transform.SetAsLastSibling();

                GetComponent<Image>().enabled = true;

                _beingDragged = true;
            }
            else
            {
                GetComponent<Image>().enabled = false;

                _beingDragged = false;
            }
        }

        public void ChangeIcon(Slot slot)
        {
            GetComponent<Image>().sprite = slot.Item.Root.Icon;
        }

        #endregion
    }
}