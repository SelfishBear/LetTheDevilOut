using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string ProgressKey = "Progress";

		private readonly IPersistentProgressService _progressService;
		private readonly IGameFactory _gameFactory;
		private readonly IUIFactory _uiFactory;

		public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory, IUIFactory uiFactory)
		{
			_progressService = progressService;
			_gameFactory = gameFactory;
			_uiFactory = uiFactory;
		}

		public void SaveProgress()
		{
			foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.Progress);
			
			foreach (ISavedProgress progressWriter in _uiFactory.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.Progress);
			
			
			PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
		}

		public PlayerProgress LoadProgress()
		{
			return PlayerPrefs.GetString(ProgressKey)?
				.ToDeserialized<PlayerProgress>();
		}
	}
}