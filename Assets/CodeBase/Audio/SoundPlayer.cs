using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlayLoopSound()
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }
        
        public void StopSound()
        {
            _audioSource.loop = false;
            _audioSource.Stop();
        }
    }
}