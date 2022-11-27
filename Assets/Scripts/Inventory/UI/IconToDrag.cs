using An01malia.FirstPerson.PlayerModule;
using An01malia.FirstPerson.UIModule;
using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.InventoryModule
{
    public class IconToDrag : MonoBehaviour
    {
        private bool _beingDragged;

        private PlayerInput _playerInput;

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
            if (_beingDragged)
            {
                transform.position = _playerInput.CursorInputValues;
            }
        }

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
    }
}