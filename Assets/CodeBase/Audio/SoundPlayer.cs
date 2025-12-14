using UnityEngine;

namespace CodeBase.Audio
{
    public abstract class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public AudioSource AudioSource => _audioSource;

        public virtual void PlayLoop()
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public virtual void StopLoop()
        {
            _audioSource.loop = false;
            _audioSource.Stop();
        }

        public virtual void PlayOneShot()
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        public virtual void Play()
        {
            _audioSource.Play();
        }

        public virtual void PlayOneShot(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public virtual void SetVolume(float volume)
        {
            _audioSource.volume = Mathf.Clamp01(volume);
        }

        public virtual void SetPitch(float pitch)
        {
            _audioSource.pitch = pitch;
        }

        public bool IsPlaying => _audioSource.isPlaying;
    }
}