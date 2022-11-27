using An01malia.FirstPerson.Inspection;
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

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Tooltip = _tooltip;
            IconToDrag = _iconToDrag;
            _canvas.worldCamera = _inspectionCamera;
            InspectItem.InspectionCamera = _inspectionCamera;
            InspectItem.ItemPlacement = _itemPlacement;
            PlayerItemInspection.LightSource = _lightSource;
        }

        #endregion
    }
}