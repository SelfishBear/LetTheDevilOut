using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        private const string Monologue = "Monologue";
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        private IGameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _gameStateMachine.Enter<MonologueState, string>(Monologue);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}