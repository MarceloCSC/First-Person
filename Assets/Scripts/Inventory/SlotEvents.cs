using An01malia.FirstPerson.UserInterfaceModule.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace An01malia.FirstPerson.InventoryModule
{
    public class SlotEvents : Slot, IPointerDownHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        #region Fields

        private static Slot _originalSlot;
        private bool _beingDragged;

        #endregion

        #region Unity Methods

        private void OnDisable()
        {
            OnDeselect(null);

            if (Tooltip.Instance != null) Tooltip.Instance.HideTooltip();

            if (_beingDragged)
            {
                OnEndDrag(null);
                _originalSlot = null;
            }
        }

        #endregion

        #region Public Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Item != null && eventData.button == PointerEventData.InputButton.Right)
            {
                ItemSelected = Item;
                IsSelected = false;
                Tooltip.Instance.ShowTooltip(Item);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                eventData.selectedObject = gameObject;
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (Item == null) return;

            ItemSelected = Item;
            IsSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            ItemSelected = null;

            if (Item == null) return;

            IsSelected = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            ItemSelected = null;
            _originalSlot = null;

            if (Item != null && eventData.button == PointerEventData.InputButton.Left)
            {
                _originalSlot = this;
                DragItem(true);
                IconToDrag.Instance.ChangeIcon(this);
                IsSelected = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_originalSlot == null) return;

            DragItem(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (_originalSlot == null) return;

            if (Item != null && Item.CanStack(_originalSlot.Item))
            {
                AddToStack();
            }
            else
            {
                SwapItems();
            }
        }

        #endregion

        #region Private Methods

        private void DragItem(bool isDragging)
        {
            IconToDrag.Instance.Activate(isDragging);

            _originalSlot.HideContents(isDragging);
            _beingDragged = isDragging;
        }

        private void AddToStack()
        {
            int amountLeft = Item.AmountToBeFilled;
            int amountToAdd = Mathf.Min(amountLeft, _originalSlot.Item.Amount);

            Item.Amount += amountToAdd;
            _originalSlot.Item.Amount -= amountToAdd;
        }

        private void SwapItems() => (Item, _originalSlot.Item) = (_originalSlot.Item, Item);

        #endregion
    }
}