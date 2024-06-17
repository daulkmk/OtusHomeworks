using System.Collections;
using Lessons.Architecture.DI;
using UnityEngine;
using Zenject;
using Atomic.Objects;
using System.Collections.Generic;
using Lessons.Lesson_Components;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private AtomicEntity _player;
    [SerializeField] private AtomicEntity _zombiePrefab;
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField] private Transform _worldContainer;
    [SerializeField] private List<Transform> _zombieSpawnPoints;
    [SerializeField] private int _maxActiveZombies = 6;
    [SerializeField] private float _spawnZombiesInterval = 2;

    public override void InstallBindings()
    {
        Container.Bind<Camera>()
            .FromInstance(_camera)
            .AsSingle();

        Container.Bind<IAtomicEntity>()
            .WithId(ObjectType.Player)
            .FromInstance(_player)
            .AsSingle();

        Container.QueueForInject(_player);

        Container.BindInterfacesTo<CameraFollow>()
            .AsSingle()
            .WithArguments(_player.transform)
            .NonLazy();

        Container.BindInterfacesTo<PlayerCharacterController>()
            .AsSingle();

        Container.BindInterfacesTo<ZombiesSpawner>()
            .AsSingle()
            .WithArguments(_zombiePrefab, _worldContainer, _zombieSpawnPoints, _maxActiveZombies, _spawnZombiesInterval)
            .NonLazy();
    }
}
