using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceT<GameObject> _uiAssetReference;
        [SerializeField] Transform _uiContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UiLoader>()
                .AsSingle()
                .WithArguments(_uiAssetReference, _uiContainer)
                .NonLazy();
        }
    }
}