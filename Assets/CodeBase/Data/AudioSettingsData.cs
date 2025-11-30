using System;

namespace CodeBase.Data
{
	[Serializable]
	public class AudioSettingsData
	{
		public float MasterVolume = 1f;
		public float MusicVolume = 0.8f;
		public float SfxVolume = 0.8f;
		public float VoiceVolume = 0.8f;
		public bool IsMuted;
	}
}