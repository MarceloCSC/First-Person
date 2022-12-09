using UnityEngine;

namespace An01malia.FirstPerson.Core.References
{
    public class Player : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _player;
        [SerializeField] private Camera _firstPersonCamera;
        [SerializeField] private Transform _playerSight;
        [SerializeField] private Transform _playerHand;
        [SerializeField] private Transform _leaningAxis;

        #endregion

        #region Properties

        public static Transform Transform { get; private set; }
        public static Transform CameraTransform { get; private set; }
        public static Transform Sight { get; private set; }
        public static Transform Hand { get; private set; }
        public static Camera Camera { get; private set; }
        public static Vector3 SightPosition { get; private set; }
        public static Vector3 CameraPosition { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Transform = _player.transform;
            Sight = _playerSight;
            Hand = _playerHand;
            SightPosition = _playerSight.localPosition;
            Camera = _firstPersonCamera;
            CameraTransform = _firstPersonCamera.transform;
            CameraPosition = _firstPersonCamera.transform.localPosition;
        }

        #endregion
    }
}