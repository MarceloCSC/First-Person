﻿using An01malia.FirstPerson.ItemModule;
using An01malia.FirstPerson.ItemModule.Items;
using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Environment
{
    public class ItemSpot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _item;
        [SerializeField] private bool _isItemLocked;
        [SerializeField] private List<ItemObject> _itemsAllowed;

        private ItemToCarry _itemToCarry;

        #endregion

        #region Properties

        public Transform Item => _item ? _item.transform : null;

        public bool IsItemLocked => _isItemLocked;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (_item) _item.TryGetComponent(out _itemToCarry);
            if (_itemToCarry) _itemToCarry.OnCarrying += RemoveItem;
        }

        private void OnDisable()
        {
            if (_itemToCarry) _itemToCarry.OnCarrying -= RemoveItem;
        }

        #endregion

        #region Public Methods

        public virtual bool TryPlaceItem(Transform transform)
        {
            if (!CanPlaceItem(transform, out ItemToCarry item)) return false;

            Place(item);

            return true;
        }

        #endregion

        #region Protected Methods

        protected void Place(ItemToCarry item)
        {
            item.Place(transform.position, _isItemLocked);

            _item = item.gameObject;
            _itemToCarry = item;
            _itemToCarry.OnCarrying += RemoveItem;
        }

        protected virtual void RemoveItem()
        {
            _itemToCarry.OnCarrying -= RemoveItem;
            _itemToCarry = null;
            _item = null;
        }

        protected bool CanPlaceItem(Transform transform, out ItemToCarry item)
        {
            return transform.TryGetComponent(out item) && !_item && IsAllowed(item);
        }

        protected virtual bool IsAllowed(ItemToCarry item) => _itemsAllowed.Count == 0 || _itemsAllowed.Contains(item.Root);

        #endregion
    }
}