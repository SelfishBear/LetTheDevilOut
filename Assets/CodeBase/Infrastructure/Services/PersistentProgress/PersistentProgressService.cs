using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
		public void Dispose()
		{
			Progress.Reset();
		}
	}
}