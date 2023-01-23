using An01malia.FirstPerson.Core;
using An01malia.FirstPerson.PlayerModule;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule.Items
{
    public class ItemToInspect : MonoBehaviour, IItem
    {
        #region Fields

        [SerializeField] private ItemObject _item;

        private MeshRenderer _renderer;
        private Collider _collider;
        private GameObject _prefab;

        #endregion

        #region Properties

        public ItemObject Root => _item;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        #endregion

        #region Public Methods

        public GameObject GetItemPrefab()
        {
            if (!ItemPooler.Instance.ItemsToExamine.TryGetValue(_item.Id, out _prefab)) return null;

            return _prefab;
        }

        public void PrepareInspection()
        {
            _renderer.enabled = false;
            _collider.enabled = false;

            _prefab.SetActive(true);
            _prefab.transform.position = Player.InspectionSpot.position;
            _prefab.transform.LookAt(Player.FirstPersonCamera.transform);
            _prefab.layer = LayerMask.NameToLayer(Layer.ToInspect);
        }

        public void FinishInspection()
        {
            _renderer.enabled = true;
            _collider.enabled = true;

            _prefab.SetActive(false);
            _prefab = null;
        }

        #endregion

        #region Private Methods

        private void SetReferences()
        {
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
        }

        #endregion
    }
}