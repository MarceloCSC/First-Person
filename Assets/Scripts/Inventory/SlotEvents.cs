using UnityEngine;
using UnityEngine.EventSystems;

namespace An01malia.FirstPerson.Inventory
{

    public class SlotEvents : Slot, IPointerDownHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {

        private static Slot originalSlot;

        private bool beingDragged;

        #region Cached references
        private Tooltip tooltip;
        private IconToDrag iconToDrag;
        #endregion


        protected override void Awake()
        {
            base.Awake();
            SetReferences();
        }

        private void DragItem(bool isDragging)
        {
            originalSlot.HideContents(isDragging);
            iconToDrag.Activate(isDragging);
            beingDragged = isDragging;
        }

        private void AddToStack()
        {
            int amountLeft = Item.AmountToBeFilled;
            int amountToAdd = Mathf.Min(amountLeft, originalSlot.Item.Amount);

            Item.Amount += amountToAdd;
            originalSlot.Item.Amount -= amountToAdd;
        }

        private void SwapItems()
        {
            Item itemToDrop = originalSlot.Item;

            originalSlot.Item = Item;
            Item = itemToDrop;
        }

        #region EventSystem Methods
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Item != null && eventData.button == PointerEventData.InputButton.Right)
            {
                ItemSelected = Item;
                IsSelected = false;
                tooltip.ShowTooltip(Item);
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
            if (Item != null)
            {
                ItemSelected = Item;
                IsSelected = true;
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            ItemSelected = null;

            if (Item != null)
            {
                IsSelected = false;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            ItemSelected = null;
            originalSlot = null;

            if (Item != null && eventData.button == PointerEventData.InputButton.Left)
            {
                originalSlot = this;
                DragItem(true);
                iconToDrag.ChangeIcon(this);
                IsSelected = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (originalSlot != null)
            {
                DragItem(false);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (originalSlot != null)
            {
                if (Item != null && Item.CanStack(originalSlot.Item))
                {
                    AddToStack();
                }
                else
                {
                    SwapItems();
                }
            }
        }
        #endregion

        private void OnDisable()
        {
            OnDeselect(null);

            if (tooltip != null)
            {
                tooltip.HideTooltip();
            }

            if (beingDragged)
            {
                OnEndDrag(null);
                originalSlot = null;
            }
        }

        private void SetReferences()
        {
            tooltip = References.Tooltip.GetComponent<Tooltip>();
            iconToDrag = References.IconToDrag.GetComponent<IconToDrag>();
        }

    }

}
