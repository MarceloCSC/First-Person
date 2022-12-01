using UnityEngine;

namespace An01malia.FirstPerson.Core
{
    public class GameOptions : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity = 15.0f;

        public static float MouseSensitivity { get; private set; }

        private void Awake()
        {
            MouseSensitivity = _mouseSensitivity;
        }
    }
}