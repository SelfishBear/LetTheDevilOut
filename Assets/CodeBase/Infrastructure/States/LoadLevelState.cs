using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Killer;
using CodeBase.Logic.Map;
using CodeBase.PlayerLogic;
using CodeBase.StaticData;
using CodeBase.UI.HUD;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string InitialKillerPointTag = "KillerInitialPoint";
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPersistentProgressService _progressService;
        private readonly SceneLoader _sceneLoader;

        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory, IWindowService windowService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticDataService;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public void Enter(string name)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(name, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitSettingsWindow();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitSettingsWindow()
        {
            _windowService.Open(WindowId.SettingsWindow);
        }

        private void InitUIRoot()
        {
            _uiFactory.CreateUIRoot();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);

            foreach (ISavedProgressReader progressReader in _uiFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            PlayerPrefab player = InitHero();
            GameObject killer = InitKiller();
            HUDPrefab HUD = InitHud(player);
        }

        private PlayerPrefab InitHero()
        {
            GameObject spawnPoint = GameObject.FindWithTag(InitialPointTag);

            PlayerPrefab player = _gameFactory.CreateHero(spawnPoint);
            return player;
        }

        private GameObject InitKiller()
        {
            GameObject spawnPoint = GameObject.FindWithTag(InitialKillerPointTag);
            PatrolPoint[] patrolPoints = Object.FindObjectsByType<PatrolPoint>(FindObjectsSortMode.None);

            GameObject killer = _gameFactory.CreateKiller(spawnPoint);
            killer.GetComponent<EnemyAI>().Construct(patrolPoints);

            return killer;
        }

        private HUDPrefab InitHud(PlayerPrefab player)
        {
            HUDPrefab hud = GetHud();

            hud.SprintBarUI.Construct(player.PlayerSprint);
            player.Interactor.Construct(hud);


            return hud;
        }

        private HUDPrefab GetHud()
        {
            return _gameFactory.HUD ?? _gameFactory.CreateHud();
        }
    }
}