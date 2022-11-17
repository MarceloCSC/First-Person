using UnityEngine;

namespace An01malia.FirstPerson.Interaction
{
    public class Pushable : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _movementSpeed = 5.0f;
        [SerializeField] private float _acceleration = 0.8f;
        [SerializeField] private bool _canMoveForward = true;
        [SerializeField] private bool _canMoveSideways = true;

        #endregion

        #region Properties

        public float MovementSpeed => _movementSpeed;
        public float Acceleration => _acceleration;
        public bool CanMoveForward => _canMoveForward;
        public bool CanMoveSideways => _canMoveSideways;

        #endregion
    }
}