using UnityEngine;
using UnityEngine.UI;

namespace FirstPerson.Inventory
{

    public class IconToDrag : MonoBehaviour
    {

        private bool beingDragged;


        private void Start()
        {
            Activate(false);
        }

        private void Update()
        {
            if (beingDragged)
            {
                transform.position = Input.mousePosition;
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
