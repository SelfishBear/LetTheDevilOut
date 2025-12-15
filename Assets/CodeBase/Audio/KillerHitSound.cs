using UnityEngine;

namespace CodeBase.Audio
{
    public class KillerHitSound : SoundPlayer
    {
        public void PlayKillerHitSound()
        {
            PlayOneShot();
        }
    }
}