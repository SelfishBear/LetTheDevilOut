using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services
{
	public class AllServices
	{
		private static AllServices _instance;
		public static AllServices Container => _instance ?? (_instance = new AllServices());

		private readonly List<IService> _registeredServices = new List<IService>();

		public void RegisterSingle<TService>(TService implementation) where TService : IService
		{
			Implementation<TService>.ServiceInstance = implementation;
			_registeredServices.Add(implementation);
		}

		public TService Single<TService>() where TService : IService =>
			Implementation<TService>.ServiceInstance;

		public void DisposeAll()
		{
			foreach (IService service in _registeredServices)
			{
				if (service is IDisposable disposable)
					disposable.Dispose();
			}
			_registeredServices.Clear();
		}

		private class Implementation<TService> where TService : IService
		{
			public static TService ServiceInstance;
		}
	}
}