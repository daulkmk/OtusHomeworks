using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class SpawnerInstaller<T> : MonoInstaller
    {
        [SerializeField] private int _count = 5;
        [SerializeField] private bool _exceedCount;
        [SerializeField] private GameObjectContext _objectContextPrefab;

        [SerializeField] private Transform _spawnedContainer;
        [SerializeField] private Transform _unspawnedContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Spawner<T>>()
                .AsSingle()
                .WithArguments(_count, _exceedCount, _objectContextPrefab, _spawnedContainer, _unspawnedContainer);
        }
    }
}