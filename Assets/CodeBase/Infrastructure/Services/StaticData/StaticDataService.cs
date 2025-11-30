using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelsDataPath = "StaticData/Levels";
        private const string StaticDataWindowPath = "StaticData/UI/Window";
        private const string HeroDataPath = "StaticData/Hero/Hero";
        
        private HeroStaticData _hero;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsDataPath)
                .ToDictionary(x => x.LevelKey, x => x);

            // _windowConfigs = Resources
            //     .Load<WindowStaticData>(StaticDataWindowPath)
            //     .Configs
            //     .ToDictionary(x => x.WindowId, x => x);
            
            _hero = Resources.Load<HeroStaticData>(HeroDataPath);
        }
        public HeroStaticData ForHero()
        {
            return _hero;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            return _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
        }

        public void Dispose()
        {
            _hero.Reset();
        }
    }
}