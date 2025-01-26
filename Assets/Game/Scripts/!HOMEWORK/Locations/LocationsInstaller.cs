using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class LocationsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _locationsContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LocationsLoader>()
                .AsSingle()
                .WithArguments(_locationsContainer)
                .NonLazy();
        }
    }
}