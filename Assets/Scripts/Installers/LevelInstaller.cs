using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private GameObjectContext _characterContext;

        public override void InstallBindings()
        {
            Container.Bind<Character>()
                .FromSubContainerResolve()
                .ByInstanceGetter(GetCharacterContainer)
                .AsSingle();

            BindInterfacesAndInject(_levelBounds);
            BindInterfacesAndInject(_enemyPositions);
        }

        DiContainer GetCharacterContainer(InjectContext c)
        {
            _characterContext.Install(Container);
            return _characterContext.Container;
        }

        private void BindInterfacesAndInject<T>(T obj)
        {
            Container.BindInterfacesTo<T>().FromInstance(obj).AsSingle();
            Container.QueueForInject(obj);
        }
    }
}