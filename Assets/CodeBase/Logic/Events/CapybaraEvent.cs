using System;
using CodeBase.Audio;
using UnityEngine;

namespace CodeBase.Logic.Events
{
    public class CapybaraEvent : TriggerEvent
    {
        [SerializeField] private Door _door;
        [SerializeField] private CapybaraSound _sound;

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
            _sound.PlayCapybaraSound();
            Debug.Log("Capybara Event Triggered");
        }
    }
}