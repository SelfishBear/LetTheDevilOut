using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IAssetProvider _assets;
        private readonly ISaveLoadService _saveLoadService;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        // public void CreateTutorialPanel()
        // {
        // 	WindowConfig config = _staticData.ForWindow(WindowId.TutorialPanel);
        // 	TutorialCanvas window = Object.Instantiate(config.Template, _uiRoot) as TutorialCanvas;
        // 	window?.Init(_stateMachine, _progressService, _loadingCurtain, _factory);
        // }

        public void CreateUIRoot()
        {
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
        }
    }
}