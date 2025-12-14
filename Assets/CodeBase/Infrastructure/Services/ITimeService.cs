namespace CodeBase.Infrastructure.Services
{
    public interface ITimeService : IService
    {
        void SetTimeScale(float timeScale);
        void PauseGame();
        void ResumeGame();
    }
}