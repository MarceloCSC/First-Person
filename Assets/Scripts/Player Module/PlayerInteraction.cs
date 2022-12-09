using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule.Interactive;
using System;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
{
    public class PlayerInteraction : MonoBehaviour
    {
        #region Delegates

        public event Action<bool, bool> OnItemExamined = delegate { };

        #endregion

        #region Fields

        [SerializeField] private float _distanceToExamine = 10.0f;
        [SerializeField] private float _distanceToInteract = 3.0f;
        [SerializeField] private LayerMask _visibleLayers;
        [SerializeField] private LayerMask _examinableLayers;
        [SerializeField] private LayerMask _reachableLayers;

        private Transform _examinedItem;
        private Transform _interactiveItem;
        private PlayerSurroundings _surroundings;

        #endregion

        #region Properties

        public Transform InteractiveItem => _interactiveItem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        #endregion

        #region Public Methods

        public void Examine()
        {
            if (CanExamine(out RaycastHit hit))
            {
                SetExamined(hit);

                bool canInteract = TrySetInteractive(hit);

                OnItemExamined(true, canInteract);
            }
            else if (_examinedItem)
            {
                ClearExamined();
            }
        }

        public bool TryGetInteractive(out IInteractive interactive)
        {
            interactive = null;

            if (!_interactiveItem) return false;

            _interactiveItem.TryGetComponent(out interactive);

            return true;
        }

        #endregion

        #region Private Methods

        private void SetExamined(RaycastHit hit)
        {
            if (hit.transform != _examinedItem)
            {
                _examinedItem = hit.transform;
            }
        }

        private void SetInteractive(RaycastHit hit)
        {
            if (hit.transform != _interactiveItem)
            {
                _interactiveItem = _examinedItem;
            }
        }

        private bool TrySetInteractive(RaycastHit hit)
        {
            if (CanInteract(hit))
            {
                SetInteractive(hit);

                return true;
            }
            else if (_interactiveItem)
            {
                _interactiveItem = null;
            }

            return false;
        }

        private void ClearExamined()
        {
            _examinedItem = null;
            _interactiveItem = null;

            OnItemExamined(false, false);
        }

        private bool CanExamine(out RaycastHit hit)
        {
            return Physics.Raycast(Player.CameraTransform.position,
                                   Player.CameraTransform.forward,
                                   out hit,
                                   _distanceToExamine,
                                   _visibleLayers) && hit.transform && IsExaminableLayer(ref hit);
        }

        private bool CanInteract(RaycastHit hit)
        {
            if (IsReachableLayer(ref hit)) return CanReach(hit);

            return Vector3.Distance(transform.position, hit.point) <= _distanceToInteract;
        }

        private bool IsExaminableLayer(ref RaycastHit hit) => _examinableLayers == (_examinableLayers | (1 << hit.transform.gameObject.layer));

        private bool IsReachableLayer(ref RaycastHit hit) => _reachableLayers == (_reachableLayers | (1 << hit.transform.gameObject.layer));

        private bool CanReach(RaycastHit hit)
        {
            return !Physics.Raycast(hit.transform.position, hit.normal, 1.0f, _visibleLayers) &&
                        Physics.Raycast(_surroundings.LowerBounds,
                                        transform.forward,
                                        out RaycastHit raycastHit,
                                        _distanceToInteract,
                                        _visibleLayers) && raycastHit.transform == hit.transform;
        }

        private void SetReferences()
        {
            _surroundings = GetComponent<PlayerSurroundings>();
        }

        #endregion
    }
}