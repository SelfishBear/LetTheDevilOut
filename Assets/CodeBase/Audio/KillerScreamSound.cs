namespace CodeBase.Audio
{
    public class KillerScreamSound : SoundPlayer
    {
        public void PlayScreamSound()
        {
            PlayLoop();
        }
        public void StopScreamSound()
        {
            StopLoop();
        }
    }
}