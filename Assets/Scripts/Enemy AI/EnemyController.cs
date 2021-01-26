using System;
using UnityEngine;

namespace FirstPerson.EnemyAI
{

    public enum EnemyState
    {
        None,
        Idle,
        Patroling,
        Chasing,
        Attacking,
        Tracking,
        Suspicious
    }

    public enum AlertState
    {
        None,
        Distracted,
        Vigilant,
        HighAlert,
    }

    public class EnemyController : MonoBehaviour
    {

        public event Action<EnemyState> OnStateChanged = delegate { };
        public event Action<EnemyState> OnMovement = delegate { };
        public event Action<AlertState> OnAlert = delegate { };


        [SerializeField] EnemyState currentState = default;
        [SerializeField] AlertState alertness = default;


        #region Properties
        public EnemyState CurrentState
        {
            get => currentState;
            private set
            {
                currentState = value;
                OnMovement(currentState);
                OnStateChanged(currentState);
            }
        }

        public AlertState Alertness
        {
            get => alertness;
            private set
            {
                if (alertness == value) { return; }
                alertness = value;
                OnAlert(alertness);
            }
        }
        #endregion


        public void ChangeState(EnemyState newState)
        {
            if (currentState == EnemyState.Tracking || currentState != newState)
            {
                if (newState == EnemyState.Patroling || newState == EnemyState.Idle)
                {
                    Alertness = AlertState.Vigilant;
                }
                else
                {
                    Alertness = AlertState.HighAlert;
                }

                CurrentState = newState;
            }
        }

    }

}
