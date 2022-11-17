using System.Text;
using UnityEngine;
using UnityEngine.UI;
using An01malia.FirstPerson.Enemy;

namespace An01malia.FirstPerson.UI
{

    public class UIEnemyDisplay : MonoBehaviour
    {

        [SerializeField] Text enemyStateText = null;
        [SerializeField] Text alertStateText = null;


        #region Cached references
        private EnemyController enemy;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void OnEnable()
        {
            enemy.OnStateChanged += UpdateEnemyState;
            enemy.OnAlert += UpdateAlertState;
        }

        private void LateUpdate()
        {
            transform.forward = References.CameraTransform.forward;
        }

        private void UpdateEnemyState(EnemyState newState)
        {
            StringBuilder stringBuilder = new StringBuilder();
            enemyStateText.text = stringBuilder.Append(newState).ToString();
        }

        private void UpdateAlertState(AlertState newState)
        {
            StringBuilder stringBuilder = new StringBuilder();
            alertStateText.text = stringBuilder.Append(newState).ToString();
        }

        private void OnDisable()
        {
            enemy.OnStateChanged -= UpdateEnemyState;
            enemy.OnAlert -= UpdateAlertState;
        }

        private void SetReferences()
        {
            enemy = GetComponentInParent<EnemyController>();
        }

    }

}
