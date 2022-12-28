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
            Physics.Raycast(Player.Transform.position - _lowerBounds, Player.Transform.forward, _rayLength, _layersToGrab) &&
                !Physics.Raycast(Player.Transform.position + _upperBounds, Player.Transform.forward, _rayLength, _layersToGrab);

        public Vector3 UpperBounds => Player.Transform.position + _upperBounds;
        public Vector3 LowerBounds => Player.Transform.position - _lowerBounds;

        #endregion

        #region Unity Methods

        private void OnDrawGizmos()
        {
            if (_toggleGizmos)
            {
                Gizmos.color = Color.green;

                Gizmos.DrawRay(Player.Transform.position + _upperBounds, Player.Transform.forward * _rayLength);
                Gizmos.DrawRay(Player.Transform.position - _lowerBounds, Player.Transform.forward * _rayLength);
            }
        }

        #endregion
    }
}