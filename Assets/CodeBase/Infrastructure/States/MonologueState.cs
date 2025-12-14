using CodeBase.Infrastructure.Services;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.States
{
    public class MonologueState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly LoadingCurtain _loadingCurtain;

        public MonologueState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services,
            LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
        }
    }
}