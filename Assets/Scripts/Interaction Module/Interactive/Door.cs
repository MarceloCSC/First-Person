using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.InteractionModule.Interactive
{
    public class Door : MonoBehaviour, IInteractive
    {
        #region Fields

        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _isLocked;

        [Header("Automatic Doors")]
        [SerializeField] private bool _isOpenedByTrigger;
        [SerializeField] private bool _hasTimer;
        [SerializeField] private bool _staysOpen;
        [SerializeField] private float _timer = 0.0f;

        private float _timeLeft;
        private Coroutine _closingDoor;

        private Animator _animator;

        #endregion Fields

        #region Properties

        public bool IsLocked { get => _isLocked; set => _isLocked = value; }

        #endregion Properties

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            SetAnimation();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isOpenedByTrigger && other.CompareTag("Player"))
            {
                if (_isLocked) return;

                ChangeState(true);

                if (_closingDoor != null) StopCoroutine(_closingDoor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_isOpenedByTrigger && _isOpen && !_staysOpen && other.CompareTag("Player"))
            {
                PrepareCoroutine();
            }
        }

        #endregion Unity Methods

        #region Public Methods

        public void StartInteraction()
        {
            if (_isLocked) return;

            ChangeState(!_isOpen);

            if (_hasTimer && _isOpen) PrepareCoroutine();
        }

        #endregion Public Methods

        #region Private Methods

        private void ChangeState(bool isOpen)
        {
            _isOpen = isOpen;
            _animator.SetBool("isOpen", isOpen);
        }

        private void SetAnimation()
        {
            string layer = _isOpen ? "Opened" : "Closed";

            _animator.SetBool("isOpen", _isOpen);
            _animator.SetLayerWeight(_animator.GetLayerIndex(layer), 1.0f);
        }

        private void PrepareCoroutine()
        {
            if (_closingDoor != null) StopCoroutine(_closingDoor);

            _closingDoor = StartCoroutine(WaitToClose(_timer));
        }

        private IEnumerator WaitToClose(float timeToWait)
        {
            _timeLeft = timeToWait;

            while (_timeLeft > 0.0f)
            {
                yield return new WaitForSeconds(1.0f);
                _timeLeft -= 1.0f;
            }

            ChangeState(false);

            yield return null;
        }

        private void SetReferences()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion Private Methods
    }
}