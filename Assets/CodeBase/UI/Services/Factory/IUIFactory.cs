using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		void CreateUIRoot();

		void CreateSettingsWindow();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
	}
}