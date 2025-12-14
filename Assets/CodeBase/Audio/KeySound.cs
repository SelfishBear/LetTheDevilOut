using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Audio
{
    public class KeySound : SoundPlayer
    {
        [SerializeField] private Key _key;

        private void Start()
        {
            _key.OnInteraction += PlayKeySound;
        }

        private void OnDestroy()
        {
            _key.OnInteraction -= PlayKeySound;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayKeySound();
            }
        }

        private void PlayKeySound()
        {
            if (AudioSource == null || AudioSource.clip == null)
            {
                Debug.LogError("AudioSource or AudioClip is NULL on KeySound!");
                return;
            }
            
            // Используем PlayClipAtPoint потому что объект Key уничтожается сразу после взаимодействия
            AudioSource.PlayClipAtPoint(AudioSource.clip, transform.position, AudioSource.volume);
        }
    }
}