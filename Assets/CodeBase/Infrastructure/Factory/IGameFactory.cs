using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.PlayerLogic;
using CodeBase.UI.HUD;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
	public interface IGameFactory : IService, IDisposable
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		HUDPrefab HUD { get; set; }
		PlayerPrefab HeroPrefab { get; }
		
		PlayerPrefab CreateHero(GameObject at);
		HUDPrefab CreateHud();
		void Cleanup();
	}
}