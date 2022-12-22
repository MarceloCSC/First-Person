using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.InteractionModule.Interactive;
using An01malia.FirstPerson.ItemModule.Items;
using System;
using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
{
    public class PlayerInteraction : MonoBehaviour
    {
        #region Delegates

        public event Action<bool, bool> OnExamination = delegate { };

        #endregion

        #region Fields

        [SerializeField] private float _distanceToExamine = 10.0f;
        [SerializeField] private float _distanceToInteract = 3.0f;
        [SerializeField] private LayerMask _visibleLayers;
        [SerializeField] private LayerMask _examinableLayers;
        [SerializeField] private LayerMask _reachableLayers;

        private Transform _examinationObject;
        private Transform _interactionObject;
        private PlayerSurroundings _surroundings;

        #endregion

        #region Properties

        public Transform Interaction => _interactionObject;

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

                OnExamination(true, canInteract);
            }
            else if (_examinationObject)
            {
                ClearExamined();
            }
        }

        public bool TryGetInteractive(out IInteractive interactive)
        {
            interactive = null;

            if (!_interactionObject) return false;

            return _interactionObject.TryGetComponent(out interactive);
        }

        public bool TryGetItem(out IItem item)
        {
            item = null;

            if (!_interactionObject) return false;

            return _interactionObject.TryGetComponent(out item);
        }

        #endregion

        #region Private Methods

        private void SetExamined(RaycastHit hit)
        {
            if (hit.transform != _examinationObject)
            {
                _examinationObject = hit.transform;
            }
        }

        private void SetInteractive(RaycastHit hit)
        {
            if (hit.transform != _interactionObject)
            {
                _interactionObject = _examinationObject;
            }
        }

        private bool TrySetInteractive(RaycastHit hit)
        {
            if (CanInteract(hit))
            {
                SetInteractive(hit);

                return true;
            }
            else if (_interactionObject)
            {
                _interactionObject = null;
            }

            return false;
        }

        private void ClearExamined()
        {
            _examinationObject = null;
            _interactionObject = null;

            OnExamination(false, false);
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