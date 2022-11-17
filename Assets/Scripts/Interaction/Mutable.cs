using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.Interaction
{
    public class Mutable : Interactive
    {
        #region Fields

        [Header("Position Change")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Transform _waypoint;

        private Vector3 _startingPosition;
        private Coroutine _changingPosition;

        private Animator _animator;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (TryGetComponent(out Animator animator))
            {
                _animator = animator;
            }
        }

        private void Start()
        {
            if (_animator) SetAnimation();
            _startingPosition = transform.position;
        }

        #endregion

        #region Public Methods

        public override void StartInteraction()
        {
            if (!_isLocked)
            {
                _isActivated = !_isActivated;

                if (_animator)
                {
                    _animator.SetBool("isActivated", _isActivated);
                }

                if (_waypoint)
                {
                    PrepareCoroutine();
                }
            }
        }

        #endregion

        #region Private Methods

        private void SetAnimation()
        {
            string layer = _isActivated ? "Transformed" : "Original";

            _animator.SetBool("isActivated", _isActivated);
            _animator.SetLayerWeight(_animator.GetLayerIndex(layer), 1.0f);
        }

        private void PrepareCoroutine()
        {
            if (_changingPosition != null)
            {
                StopCoroutine(_changingPosition);
            }

            Vector3 destination = _isActivated ? _waypoint.position : _startingPosition;
            _changingPosition = StartCoroutine(SetPosition(destination));
        }

        private IEnumerator SetPosition(Vector3 destination)
        {
            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, _movementSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            yield return null;
        }

        #endregion
    }
}