using UnityEngine;
using FirstPerson.Inventory;

namespace FirstPerson.Interaction
{

    public class PickUp : Interactable
    {

        [SerializeField] Item item = null;


        #region Properties
        public override string Name => item.Root.Name.ToLower();
        #endregion


        #region Cached references
        private PlayerInventory inventory;
        #endregion


        protected override void InteractWith()
        {
            if (IsSame())
            {
                AddToInventory(SingleItem());
            }
        }

        protected override void TakeAll()
        {
            if (IsSame())
            {
                AddToInventory(item);
            }
        }

        private void AddToInventory(Item item)
        {
            inventory.AddItems(item);

            if (this.item.Amount == 0)
            {
                gameObject.SetActive(false);
            }
        }

        private Item SingleItem()
        {
            Item singleItem = new Item(item.Root, 1);
            item.Amount--;

            return singleItem;
        }

        protected override void SetReferences()
        {
            base.SetReferences();
            inventory = References.Player.GetComponent<PlayerInventory>();
        }

    }

}
