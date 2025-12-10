using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Randomizer;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.Killer;
using CodeBase.Logic.Map;
using CodeBase.PlayerLogic;
using CodeBase.UI.HUD;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _progressService;
        private readonly IRandomService _randomService;
        private readonly IGameStateMachine _stateMachine;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService,
            IPersistentProgressService progressService, IGameStateMachine stateMachine)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = progressService;
            _stateMachine = stateMachine;
        }

        public PlayerPrefab HeroPrefab { get; private set; }


        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public List<PlayerPrefab> SpawnedPlayers { get; } = new List<PlayerPrefab>();

        public HUDPrefab HUD { get; set; }


        public PlayerPrefab CreateHero(GameObject at)
        {
            HeroPrefab = InstantiateRegistered(AssetAddress.PlayerPath, at.transform.position)
                .GetComponent<PlayerPrefab>();

            HeroPrefab.GetComponent<PlayerFlashlight>().Construct(heroStaticData: _staticData.ForHero());

            return HeroPrefab;
        }

        public GameObject CreateKiller(GameObject at)
        {
            GameObject killer = InstantiateRegistered(AssetAddress.KillerPath, at.transform.position);
            return killer;
        }

        public HUDPrefab CreateHud()
        {
            HUD = InstantiateRegistered(AssetAddress.HudPath).GetComponent<HUDPrefab>();

            return HUD;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void Dispose()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}