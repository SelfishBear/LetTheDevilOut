using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Audio
{
    public class SFXAudioSources : MonoBehaviour
    {
        public List<AudioSource> AudioSources;

        private void Start()
        {
            SoundPlayer[] sources = FindObjectsByType<SoundPlayer>(FindObjectsSortMode.None);
            foreach (SoundPlayer source in sources)
                AudioSources.Add(source.AudioSource);
        }
        
        public void SetVolume(float volume)
        {
            foreach (AudioSource source in AudioSources)
                source.volume = volume;
        }
    }
}