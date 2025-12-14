namespace CodeBase.Infrastructure.Services
{
    public class TimeService : ITimeService
    {
        public void SetTimeScale(float timeScale)
        {
            UnityEngine.Time.timeScale = timeScale;
            UnityEngine.AudioListener.pause = timeScale == 0f;
        }
        
        public void PauseGame()
        {
            UnityEngine.Time.timeScale = 0f;
            UnityEngine.AudioListener.pause = true;
        }
        
        public void ResumeGame()
        {
            UnityEngine.Time.timeScale = 1f;
            UnityEngine.AudioListener.pause = false;
        }
    }
}