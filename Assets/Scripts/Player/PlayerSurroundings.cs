using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
{
    public class PlayerSurroundings : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _rayLength = 0.75f;
        [SerializeField] private Vector3 _upperBounds = new(0.0f, 0.45f, 0.0f);
        [SerializeField] private Vector3 _lowerBounds = new(0.0f, 0.9f, 0.0f);
        [SerializeField] private LayerMask _layersToGrab;

        [Space]
        [SerializeField] private bool _toggleGizmos;

        #endregion

        #region Properties

        public bool CanGrabLedge =>
            Physics.Raycast(transform.position - _lowerBounds, transform.forward, _rayLength, _layersToGrab) &&
                !Physics.Raycast(transform.position + _upperBounds, transform.forward, _rayLength, _layersToGrab);

        public Vector3 UpperBounds => transform.position + _upperBounds;
        public Vector3 LowerBounds => transform.position - _lowerBounds;

        #endregion

        #region Unity Methods

        private void OnDrawGizmos()
        {
            if (_toggleGizmos)
            {
                Gizmos.color = Color.green;

                Gizmos.DrawRay(transform.position + _upperBounds, transform.forward * _rayLength);
                Gizmos.DrawRay(transform.position - _lowerBounds, transform.forward * _rayLength);
            }
        }

        #endregion
    }
}