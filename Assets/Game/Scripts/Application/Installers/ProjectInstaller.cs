using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetReference _gameSceneReference;

        public override void InstallBindings()
        {
            this.Container.Bind<ApplicationExiter>()
                .AsSingle()
                .NonLazy();
            
            this.Container.Bind<GameLoader>()
                .AsSingle()
                .WithArguments(_gameSceneReference)
                .NonLazy();

            this.Container.Bind<MenuLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}