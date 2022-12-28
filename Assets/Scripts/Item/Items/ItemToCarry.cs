using An01malia.FirstPerson.PlayerModule;
using System;
using System.Collections;
using UnityEngine;

namespace An01malia.FirstPerson.ItemModule.Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemToCarry : MonoBehaviour, IItem
    {
        #region Delegates

        public event Action OnCarrying = delegate { };

        #endregion

        #region Fields

        [SerializeField] private ItemObject _item;
        [SerializeField] private float _isKinematicTimeout = 10.0f;

        private Coroutine _coroutine;

        #endregion

        #region Properties

        public ItemObject Root => _item;

        #endregion

        #region Public Methods

        public void Carry()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<Collider>().enabled = false;
            transform.parent = Player.Hand;
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = Vector3.zero;

            OnCarrying();
        }

        public void Drop()
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetComponent<Collider>().enabled = true;
            transform.parent = null;

            if (_coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(SetIsKinematic());
        }

        #endregion

        #region Private Methods

        private IEnumerator SetIsKinematic()
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.down;

            float deadline = Time.time + _isKinematicTimeout;

            while (rigidbody.velocity != Vector3.zero && Time.time < deadline)
            {
                yield return new WaitForFixedUpdate();
            }

            rigidbody.isKinematic = true;

            _coroutine = null;

            yield return null;
        }

        #endregion
    }
}