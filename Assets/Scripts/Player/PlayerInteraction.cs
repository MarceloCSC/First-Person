using An01malia.FirstPerson.InteractionModule.Environment;
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
        [SerializeField] private LayerMask _layersToExamine;

        private bool _hasToApproach;
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
                SetExamination(hit);

                bool canInteract = TrySetInteraction(hit);

                OnExamination(true, canInteract);
            }
            else if (_examinationObject)
            {
                ClearData();
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

        private void SetExamination(RaycastHit hit)
        {
            if (hit.transform != _examinationObject)
            {
                _examinationObject = hit.transform;
            }
        }

        private void SetInteraction(RaycastHit hit)
        {
            if (hit.transform != _interactionObject)
            {
                _interactionObject = _examinationObject;

                SetHasToApproach(_interactionObject);
            }
        }

        private bool TrySetInteraction(RaycastHit hit)
        {
            if (CanInteract(hit))
            {
                SetInteraction(hit);

                return true;
            }
            else if (_interactionObject)
            {
                _interactionObject = null;
            }

            return false;
        }

        private void ClearData()
        {
            _examinationObject = null;
            _interactionObject = null;
            _hasToApproach = false;

            OnExamination(false, false);
        }

        private bool CanExamine(out RaycastHit hit)
        {
            return Physics.Raycast(Player.Sight.position,
                                   Player.Sight.forward,
                                   out hit,
                                   _distanceToExamine,
                                   _layersToExamine,
                                   QueryTriggerInteraction.Collide) && hit.transform;
        }

        private bool CanInteract(RaycastHit hit)
        {
            if (_hasToApproach) return CanReach(hit);

            return Vector3.Distance(Player.Transform.position, hit.point) <= _distanceToInteract;
        }

        private void SetHasToApproach(Transform interaction)
        {
            _hasToApproach = interaction.TryGetComponent(out CrateToPush _) || interaction.TryGetComponent(out SurfaceToClimb _);
        }

        private bool CanReach(RaycastHit hit)
        {
            return !Physics.Raycast(hit.transform.position, hit.normal, 1.0f) &&
                        Physics.Raycast(_surroundings.LowerBounds,
                                        Player.Transform.forward,
                                        out RaycastHit raycastHit,
                                        _distanceToInteract) && raycastHit.transform == hit.transform;
        }

        private void SetReferences()
        {
            _surroundings = GetComponent<PlayerSurroundings>();
        }

        #endregion
    }
}