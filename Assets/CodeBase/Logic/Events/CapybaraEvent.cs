using System;
using UnityEngine;

namespace CodeBase.Logic.Events
{
    public class CapybaraEvent : TriggerEvent
    {
        [SerializeField] private Door _door;

        private void Start()
        {
            _door.OnDoorInteracted += Execute;
        }

        private void OnDestroy()
        {
            _door.OnDoorInteracted -= Execute;
        }

        protected override void Execute()
        {
            Debug.Log("Capybara Event Triggered");
        }
    }
}