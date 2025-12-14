using UnityEngine;

namespace CodeBase.Audio
{
    public class MusicAudioSource : MonoBehaviour
    {
        public AudioSource AudioSource;
        
        public void SetVolume(float volume)
        {
            AudioSource.volume = volume;
        }
    }
}