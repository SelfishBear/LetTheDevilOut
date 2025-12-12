using System;
using UnityEngine;

namespace CodeBase.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void Start()
        {
            PlayMusic();
        }

        public void PlayMusic()
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}