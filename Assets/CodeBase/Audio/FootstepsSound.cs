using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Audio
{
    public class FootstepsSound : SoundPlayer
    {
        [SerializeField] private AudioClip[] _stepSounds;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerSprint _playerSprint;
        [SerializeField] private float _stepInterval = 0.5f;
        [SerializeField] private float _sprintStepInterval = 0.3f;
        
        private float _stepTimer;

        private void Update()
        {
            PLayStepSound();
        }

        private void PLayStepSound()
        {
            bool isWalking = _playerMovement != null && _playerMovement.IsWalking();

            if (isWalking)
            {
                _stepTimer += Time.deltaTime;

                bool isSprinting = _playerSprint != null && _playerSprint.IsSprinting();
                float currentStepInterval = isSprinting ? _sprintStepInterval : _stepInterval;

                if (_stepTimer >= currentStepInterval)
                {
                    AudioClip audioClip = _stepSounds[Random.Range(0, _stepSounds.Length)];
                    PlayOneShot(audioClip);
                    _stepTimer = 0f;
                }
            }
            else
            {
                _stepTimer = 0f;
            }
        }
    }
}