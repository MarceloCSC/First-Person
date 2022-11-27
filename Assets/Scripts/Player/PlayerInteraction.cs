using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule;
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
        [SerializeField] private Vector3 _lowerBounds = new(0.0f, 0.9f, 0.0f);
        [SerializeField] private LayerMask _visibleLayers;
        [SerializeField] private LayerMask _examinableLayers;
        [SerializeField] private LayerMask _reachableLayers;

        private Transform _examinedItem;
        private Transform _interactiveItem;

        #endregion

        #region Properties

        public Transform InteractiveItem => _interactiveItem;

        #endregion

        #region Public Methods

        public void Examine()
        {
            if (CanExamine(out RaycastHit hit))
            {
                SetExamined(hit);
            }
            else if (_examinedItem)
            {
                ClearExamined();
            }
        }

        public bool TrySetInteractive(out IInteractive interactive)
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

            bool canInteract = TrySetInteractive(hit);

            OnItemExamined(true, canInteract);
        }

        private bool TrySetInteractive(RaycastHit hit)
        {
            bool canInteract = false;

            if (CanInteract(hit))
            {
                _interactiveItem = _examinedItem;
            }

            return canInteract;
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
                                   _visibleLayers) && hit.transform && IsExaminable(ref hit);
        }

        private bool CanInteract(RaycastHit hit)
        {
            if (IsReachable(ref hit))
            {
                return !Physics.Raycast(hit.transform.position,
                                        hit.normal,
                                        1.0f,
                                        _visibleLayers) &&

                        Physics.Raycast(transform.position - _lowerBounds,
                                        transform.forward,
                                        out RaycastHit raycastHit,
                                        _distanceToInteract,
                                        _visibleLayers) &&

                        raycastHit.transform == hit.transform;
            }

            return Vector3.Distance(transform.position, hit.point) <= _distanceToInteract;
        }

        private bool IsExaminable(ref RaycastHit hit) => _examinableLayers == (_examinableLayers | (1 << hit.transform.gameObject.layer));

        private bool IsReachable(ref RaycastHit hit) => _reachableLayers == (_reachableLayers | (1 << hit.transform.gameObject.layer));

        #endregion
    }
}