using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.GameplayServices;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.PlayerLogic;
using CodeBase.StaticData.Windows;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class SettingsWindow : WindowBase, ISavedProgress
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _closeGameButton;

        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private CanvasGroup _settingsPanelCanvasGroup;

        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        
        private PlayerPrefab _playerPrefab;

        private IInputService _inputService;
        private ITimeService _timeService;
        private InputSystem_Actions _inputActions;
        private ICursorService _cursorService;
        private PlayerProgress _playerProgress;
        private ISaveLoadService _saveLoad;

        protected override void Initialize()
        {
            _playerPrefab = AllServices.Container.Single<IGameFactory>().HeroPrefab;
            
            _cursorService = AllServices.Container.Single<ICursorService>();
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();
            
            _playerProgress = AllServices.Container.Single<IPersistentProgressService>().Progress;

            _saveLoad = AllServices.Container.Single<ISaveLoadService>();
            
            _timeService = AllServices.Container.Single<ITimeService>();
            
            _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            _sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
            
            ApplyVolumeSettings();
        }
        
        private void ApplyVolumeSettings()
        {
            if (_playerPrefab != null && _playerProgress != null)
            {
                _playerPrefab.MusicAudioSource.SetVolume(_playerProgress.Settings.MusicVolume);
                _playerPrefab.SfxAudioSources.SetVolume(_playerProgress.Settings.SfxVolume);
            }
        }

        protected override void SubscribeUpdates()
        {
            _inputActions.Player.ToggleSettings.performed += OnClosePressed;
            _settingsButton.onClick.AddListener(OpenSettingsPanel);
            _closeGameButton.onClick.AddListener(CloseGame);
        }

        protected override void Cleanup()
        {
            _inputActions.Player.ToggleSettings.performed -= OnClosePressed;
            _settingsButton.onClick.RemoveListener(OpenSettingsPanel);
            _closeGameButton.onClick.RemoveListener(CloseGame);
        }

        private void OnClosePressed(InputAction.CallbackContext obj)
        {
            if (IsCanvasVisible(_settingsPanelCanvasGroup))
            {
                HideCanvas(_settingsPanelCanvasGroup);
                return;
            }

            if (IsCanvasVisible(_mainCanvasGroup))
            {
                HideCanvas(_mainCanvasGroup);
                return;
            }

            ShowCanvas(_mainCanvasGroup);
        }

        private bool IsCanvasVisible(CanvasGroup canvas) => canvas.alpha > 0;

        private void ShowCanvas(CanvasGroup canvas)
        {
            _timeService.PauseGame();
            _cursorService.ChangeCursorState(true, false);

            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
        }

        private void HideCanvas(CanvasGroup canvas)
        {
            _timeService.ResumeGame();
            _cursorService.ChangeCursorState(false, true);
            
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
        }

        private void OpenSettingsPanel()
        {
            _cursorService.ChangeCursorState(true, false);
            ShowCanvas(_settingsPanelCanvasGroup);
        }

        private void SetMusicVolume(float volume)
        {
            _playerPrefab.MusicAudioSource.SetVolume(volume);
            _saveLoad.SaveProgress();
        }

        private void SetSfxVolume(float volume)
        {
            _playerPrefab.SfxAudioSources.SetVolume(volume);
            _saveLoad.SaveProgress();
        }

        private void CloseGame()
        {
            Application.Quit();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerProgress = progress;
            
            _musicVolumeSlider.value = progress.Settings.MusicVolume;
            _sfxVolumeSlider.value = progress.Settings.SfxVolume;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Settings.MusicVolume = _musicVolumeSlider.value;
            progress.Settings.SfxVolume = _sfxVolumeSlider.value;
        }
    }
}