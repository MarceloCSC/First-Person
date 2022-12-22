using UnityEngine;

namespace An01malia.FirstPerson.ItemModule
{
    public class ItemSpot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Vector3 _position;

        private ItemObject _item;

        #endregion

        #region Public Methods

        public void PlaceItem(ItemObject item)
        {
            GameObject prefab = Instantiate(item.Prefab);

            prefab.transform.position = _position;

            _item = item;
        }

        #endregion
    }
}