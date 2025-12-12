using System;
using CodeBase.PlayerLogic;
using UnityEngine;

namespace CodeBase.Logic.Events
{
    public abstract class TriggerEvent : MonoBehaviour
    {
        [SerializeField] protected Collider _triggerCollider;
        [SerializeField] protected bool _isSelfInvoked;

        private void OnTriggerEnter(Collider other)
        {
            if (!_isSelfInvoked) return;
            if (other.TryGetComponent(out PlayerPrefab playerPrefab))
            {
                Execute();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPrefab playerPrefab))
            {
                OnExit();
            }
        }

        protected virtual void OnExit() { }
        protected abstract void Execute();
    }
}