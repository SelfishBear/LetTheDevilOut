using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;

namespace CodeBase.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		public EndGameUI EndGameUI { get; }
		void CreateUIRoot();
		
		void CreateEndGameWindow();

		void CreateSettingsWindow();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
	}
}