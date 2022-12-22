using System.Text;
using An01malia.FirstPerson.Core.References;
using An01malia.FirstPerson.EnemyModule;
using UnityEngine;
using UnityEngine.UI;

namespace An01malia.FirstPerson.UserInterfaceModule.Enemy
{
    public class UIEnemyDisplay : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _enemyStateText;
        [SerializeField] private Text _alertStateText;

        private EnemyController _enemy;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            _enemy.OnStateChanged += UpdateEnemyState;
            _enemy.OnAlert += UpdateAlertState;
        }

        private void LateUpdate()
        {
            transform.forward = Player.CameraTransform.forward;
        }

        private void OnDisable()
        {
            _enemy.OnStateChanged -= UpdateEnemyState;
            _enemy.OnAlert -= UpdateAlertState;
        }

        #endregion

        #region Private Methods

        private void UpdateEnemyState(EnemyState newState)
        {
            StringBuilder stringBuilder = new();

            _enemyStateText.text = stringBuilder.Append(newState).ToString();
        }

        private void UpdateAlertState(AlertState newState)
        {
            StringBuilder stringBuilder = new();

            _alertStateText.text = stringBuilder.Append(newState).ToString();
        }

        private void SetReferences()
        {
            _enemy = GetComponentInParent<EnemyController>();
        }

        #endregion
    }
}