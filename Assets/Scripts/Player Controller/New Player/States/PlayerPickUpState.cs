using UnityEngine;

namespace FirstPerson.Player.States
{
    public class PlayerPickUpState : PlayerBaseState
    {
        #region Fields

        [SerializeField] private Transform _hand;

        private bool _isItemRemoved;
        private Transform _itemInHand;

        #endregion

        #region Overriden Methods

        public override void EnterState()
        {
            _itemInHand = _context.InteractiveItem;
            _itemInHand.GetComponent<Rigidbody>().isKinematic = true;
            _itemInHand.GetComponent<Collider>().enabled = false;
            _itemInHand.parent = _hand;
            _itemInHand.localPosition = Vector3.zero;
            _itemInHand.eulerAngles = Vector3.zero;
            _isItemRemoved = false;
        }

        public override void ExitState()
        {
            if (_isItemRemoved)
            {
                Destroy(_itemInHand.gameObject);
                _itemInHand = null;
            }
            else
            {
                _itemInHand.GetComponent<Rigidbody>().isKinematic = false;
                _itemInHand.GetComponent<Collider>().enabled = true;
                _itemInHand.parent = null;
                _itemInHand = null;
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        public override void CheckSwitchState()
        {
        }

        public override bool TrySwitchState(ActionType action)
        {
            if (action == ActionType.Interact)
            {
                // do something
                return true;
            }
            else if (action == ActionType.Climb)
            {
                RemoveSubState();
                return true;
            }
            else if (action == ActionType.Push)
            {
                RemoveSubState();
                return true;
            }
            else if (action == ActionType.PickUp)
            {
                RemoveSubState();
                return true;
            }
            else if (action == ActionType.None)
            {
                RemoveSubState();
                return true;
            }

            return false;
        }

        #endregion
    }
}