using UnityEngine;

namespace An01malia.FirstPerson.Core.References
{
    public class UI : MonoBehaviour
    {
        #region Fields

        [Header("Inventory System")]
        [SerializeField] private GameObject _tooltip;
        [SerializeField] private GameObject _iconToDrag;

        [Header("Inspection System")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _inspectionCamera;
        [SerializeField] private Transform _itemPlacement;
        [SerializeField] private GameObject _lightSource;

        #endregion

        #region Properties

        public static GameObject Tooltip { get; private set; }
        public static GameObject IconToDrag { get; private set; }
        public static Camera InspectionCamera { get; private set; }
        public static Transform ItemPlacement { get; private set; }
        public static GameObject LightSource { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Tooltip = _tooltip;
            IconToDrag = _iconToDrag;
            InspectionCamera = _inspectionCamera;
            ItemPlacement = _itemPlacement;
            LightSource = _lightSource;
            _canvas.worldCamera = _inspectionCamera;
        }

        #endregion
    }
}