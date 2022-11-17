using System;
using UnityEngine;

namespace An01malia.FirstPerson.Player
{
    public class PlayerExamine : MonoBehaviour
    {
        #region Delegates

        public event Action<bool, bool> OnItemExamined = delegate { };

        #endregion

        #region Fields

        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _distanceToExamine = 10.0f;
        [SerializeField] private float _distanceToInteract = 3.0f;
        [SerializeField] private Vector3 _lowerBounds = new Vector3(0.0f, 0.9f, 0.0f);
        [SerializeField] private LayerMask _visibleLayers;
        [SerializeField] private LayerMask _examinableLayers;
        [SerializeField] private LayerMask _reachableLayers;

        private bool _canInteract;
        private Transform _examinedItem;

        #endregion

        #region Properties

        public bool CanInteract => _canInteract;
        public Transform ExaminedItem => _examinedItem;

        #endregion

        #region Public Methods

        public void Examine()
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _distanceToExamine, _visibleLayers) &&
                hit.transform && _examinableLayers == (_examinableLayers | (1 << hit.transform.gameObject.layer)))
            {
                if (hit.transform != _examinedItem)
                {
                    _examinedItem = hit.transform;
                }

                _canInteract = CheckCanInteract(hit);

                OnItemExamined(true, _canInteract);
            }
            else if (_examinedItem)
            {
                _examinedItem = null;
                _canInteract = false;

                OnItemExamined(false, false);
            }
        }

        #endregion

        #region Private Methods

        private bool CheckCanInteract(RaycastHit hit)
        {
            if (_reachableLayers == (_reachableLayers | (1 << hit.transform.gameObject.layer)))
            {
                return !Physics.Raycast(hit.transform.position, hit.normal, 1.0f, _visibleLayers) && 
                        Physics.Raycast(transform.position - _lowerBounds, transform.forward, out RaycastHit raycastHit, _distanceToInteract, _visibleLayers) &&
                        raycastHit.transform == hit.transform;
            }

            return Vector3.Distance(transform.position, hit.point) <= _distanceToInteract;
        }

        #endregion
    }
}