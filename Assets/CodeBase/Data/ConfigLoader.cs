using System.IO;
using UnityEngine;

namespace CodeBase.Data
{
    public static class ConfigLoader
    {
        private static readonly string s_configPath = $"{Application.dataPath}/Resources/config.json";

        public static string LoadInitialLevel()
        {
            if (File.Exists(s_configPath))
            {
                string json = File.ReadAllText(s_configPath);
                Config config = JsonUtility.FromJson<Config>(json);
                return config.InitialLevel;
            }
        
            Debug.LogWarning("Config file not found. Using default level.");
            return "Main";
        }

        [System.Serializable]
        private class Config
        {
            public string InitialLevel;
        }
    }
}