using System;

namespace CodeBase.Data
{
	[Serializable]
	public class SettingData
	{
		public AudioSettingsData AudioSettingsData = new AudioSettingsData();
		public GraphicSettingsData GraphicSettingsData = new GraphicSettingsData();
		public GameplaySettingsData GameplaySettingsData = new GameplaySettingsData();
	}
}