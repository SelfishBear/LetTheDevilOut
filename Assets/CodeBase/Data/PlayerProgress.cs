using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public Stats HeroStats;
        public PlayerSettings Settings;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State();
            HeroStats = new Stats();
            Settings = new PlayerSettings();
        }

        public void Reset()
        {
            HeroState.Reset();
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        public float MusicVolume = 0.5f;
        public float SfxVolume = 0.5f;
    }
}