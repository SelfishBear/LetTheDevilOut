namespace CodeBase.Infrastructure.Services.GameplayServices
{
    public interface ICursorService : IService
    {
        void ChangeCursorState(bool isVisible, bool isLocked);
    }
}