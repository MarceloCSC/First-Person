using An01malia.FirstPerson.UI;
using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.Inventory
{
    public class IconToDrag : MonoBehaviour
    {
        private bool beingDragged;

        private UIInputManager _inputManager;

        private void Awake()
        {
            // MUDAR!!
            _inputManager = FindObjectOfType<UIInputManager>();
        }

        private void Start()
        {
            Activate(false);
        }

        private void Update()
        {
            if (beingDragged)
            {
                transform.position = _inputManager.CursorInputValues;
            }
        }

        public void Activate(bool active)
        {
            if (active)
            {
                transform.SetAsLastSibling();
                GetComponent<Image>().enabled = true;
                beingDragged = true;
            }
            else
            {
                GetComponent<Image>().enabled = false;
                beingDragged = false;
            }
        }

        public void ChangeIcon(Slot slot)
        {
            GetComponent<Image>().sprite = slot.Item.Root.Icon;
        }
    }
}