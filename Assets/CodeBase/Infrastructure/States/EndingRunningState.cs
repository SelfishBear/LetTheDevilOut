using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class EndingRunningState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly LoadingCurtain _loadingCurtain;

        public EndingRunningState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services,
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