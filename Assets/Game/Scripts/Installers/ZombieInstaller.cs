using Atomic.Objects;
using UnityEngine;
using Zenject;

public class ZombieInstaller : MonoInstaller
{
    [SerializeField] private AtomicEntity _zombie;
    [SerializeField] private float _targetReachedDistance = 1f;

    public override void InstallBindings()
    {
        Container.Bind<IAtomicEntity>()
            .FromInstance(_zombie)
            .AsSingle();

        Container.QueueForInject(_zombie);

        Container.BindInterfacesAndSelfTo<ZombieController>()
            .AsSingle()
            .WithArguments(_targetReachedDistance)
            .NonLazy();
    }
}
