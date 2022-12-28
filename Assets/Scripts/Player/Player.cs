using UnityEngine;

namespace An01malia.FirstPerson.PlayerModule
{
    public class Player : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _firstPersonCamera;
        [SerializeField] private Camera _inspectionCamera;
        [SerializeField] private Transform _playerSight;
        [SerializeField] private Transform _playerHand;
        [SerializeField] private Transform _leaningAxis;
        [SerializeField] private Transform _itemPlacement;
        [SerializeField] private GameObject _lightSource;

        #endregion

        #region Properties

        public static Transform Transform { get; private set; }
        public static Transform Camera { get; private set; }
        public static Transform Sight { get; private set; }
        public static Transform Hand { get; private set; }
        public static Vector3 SightPosition { get; private set; }
        public static Camera InspectionCamera { get; private set; }
        public static Transform InspectionItemPlacement { get; private set; }
        public static GameObject InspectionLightSource { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Transform = transform;
            Camera = _firstPersonCamera.transform;
            Sight = _playerSight;
            Hand = _playerHand;
            SightPosition = _playerSight.localPosition;
            InspectionCamera = _inspectionCamera;
            InspectionItemPlacement = _itemPlacement;
            InspectionLightSource = _lightSource;
        }

        #endregion
    }
}