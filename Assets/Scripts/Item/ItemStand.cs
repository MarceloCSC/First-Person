using An01malia.FirstPerson.ItemModule.Items;
using System.Collections.Generic;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule
{
    public class ItemStand : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _item;
        [SerializeField] private List<ItemObject> _itemsAllowed;

        private ItemToCarry _itemToCarry;

        #endregion

        #region Properties

        public Transform Item => _item ? _item.transform : null;

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

        public bool TryPlaceItem(Transform transform)
        {
            if (!transform.TryGetComponent(out ItemToCarry item)) return false;

            if (_item || !CanPlace(item)) return false;

            transform.localPosition = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            transform.position = base.transform.position;

            _item = item.gameObject;
            _itemToCarry = item;
            _itemToCarry.OnCarrying += RemoveItem;

            return true;
        }

        #endregion

        #region Private Methods

        private void RemoveItem()
        {
            _itemToCarry.OnCarrying -= RemoveItem;
            _itemToCarry = null;
            _item = null;
        }

        private bool CanPlace(ItemToCarry item) => _itemsAllowed.Count == 0 || _itemsAllowed.Contains(item.Root);

        #endregion
    }
}