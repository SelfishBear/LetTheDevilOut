using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.GameplayServices;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.StaticData.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class EndGameUI : WindowBase  
    {
        [SerializeField] private Button _revengeButton;
        [SerializeField] private Button _escapeButton;

        [SerializeField] private CanvasGroup _canvasGroup;

        private IInputService _inputService;
        private InputSystem_Actions _inputActions;
        private ICursorService _cursorService;

        protected override void Initialize()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();

            _cursorService = AllServices.Container.Single<ICursorService>();

            _revengeButton.onClick.AddListener(TakeRevenge);
            _escapeButton.onClick.AddListener(Escape);
        }

        private void TakeRevenge()
        {
            //TODO: Implement revenge logic
        }

        private void Escape()
        {
            //TODO: Implement escape logic  
        }

        public void Show()
        {
            DisableInput();
            SetCursorState();
            ShowEndGame();
        }

        private void SetCursorState()
        {
            _cursorService.ChangeCursorState(true, false);
        }

        private void DisableInput()
        {
            _inputActions.Disable();
        }

        private void ShowEndGame()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}