using Zenject;

namespace ShootEmUp
{
    public class AICharacterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TransformInfo>()
                .AsSingle()
                .WithArguments(transform);

            Container.BindInterfacesTo<GameObjectInfo>()
                .AsSingle()
                .WithArguments(gameObject);

            Container.BindInterfacesAndSelfTo<AICharacter>()
                .AsSingle()
                .NonLazy();
        }
    }
}
