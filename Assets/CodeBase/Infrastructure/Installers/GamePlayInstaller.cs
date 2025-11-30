using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class GamePlayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
        }
    }
}