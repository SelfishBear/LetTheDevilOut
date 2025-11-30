using System;
using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
	public interface IPersistentProgressService : IService, IDisposable
	{
		PlayerProgress Progress { get; set; }
	}
}