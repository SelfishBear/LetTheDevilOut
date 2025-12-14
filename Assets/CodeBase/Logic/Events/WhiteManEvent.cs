using System.Collections;
using System.ComponentModel;
using CodeBase.Audio;
using UnityEngine;

namespace CodeBase.Logic.Events
{
    public class WhiteManEvent : TriggerEvent
    {
        [SerializeField] private Door _door;
        [SerializeField] private WhiteManSound _sound;
        [SerializeField] private SkinnedMeshRenderer _skin;

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
            _sound.PlayWhiteManSound();
            StartCoroutine(DisableAfterSeconds(5f));
        }
        
        private IEnumerator DisableAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _skin.enabled = false;
        }
    }
}