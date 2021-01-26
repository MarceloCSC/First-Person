using UnityEngine;
using FirstPerson.Examine;

namespace FirstPerson
{

    public class References : MonoBehaviour
    {

        [Header("Core")]
        [SerializeField] Camera firstPersonCamera = null;
        [SerializeField] GameObject player = null;
        [SerializeField] Transform playerSight = null;
        [SerializeField] Transform leaningAxis = null;


        public static GameObject Player { get; private set; }
        public static Transform PlayerTransform { get; private set; }
        public static Transform PlayerSight { get; private set; }
        public static Camera FirstPersonCamera { get; private set; }
        public static Transform CameraTransform { get; private set; }
        public static Transform LeaningAxis { get; private set; }


        [Header("Inventory System")]
        [SerializeField] GameObject tooltip = null;
        [SerializeField] GameObject iconToDrag = null;


        public static GameObject Tooltip { get; private set; }
        public static GameObject IconToDrag { get; private set; }


        [Header("Examine System")]
        [SerializeField] Canvas canvas = null;
        [SerializeField] Camera renderCamera = null;
        [SerializeField] Transform itemPlacement = default;
        [SerializeField] GameObject lightSource = null;


        private void Awake()
        {
            Player = player;
            PlayerTransform = player.transform;
            PlayerSight = playerSight;
            FirstPersonCamera = firstPersonCamera;
            CameraTransform = firstPersonCamera.transform;
            LeaningAxis = leaningAxis;
            Tooltip = tooltip;
            IconToDrag = iconToDrag;
            canvas.worldCamera = renderCamera;
            ExamineItem.ExamineCamera = renderCamera;
            ExamineItem.ExamineSpot = itemPlacement;
            PlayerExamine.LightSource = lightSource;
        }

    }

}
