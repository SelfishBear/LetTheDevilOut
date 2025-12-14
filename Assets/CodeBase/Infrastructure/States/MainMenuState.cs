using CodeBase.Infrastructure.Services;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly LoadingCurtain _loadingCurtain;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services,
            LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Hide();
            _sceneLoader.Load(payload, onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
          
        }

        public void Exit()
        {
        }
    }
}