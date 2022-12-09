using An01malia.FirstPerson.InventoryModule.Items;
using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.InventoryModule
{
    public class SlotBaseClass : MonoBehaviour
    {
        [SerializeField] protected Image image = null;
        [SerializeField] protected Text amountText = null;
        [SerializeField] protected Item item;

        protected Color defaultColor = Color.white;
        protected Color invisible = new Color(1, 1, 1, 0);

        protected virtual void Awake()
        {
            SetComponents();
        }

        private void SetComponents()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }

            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
        }
    }
}