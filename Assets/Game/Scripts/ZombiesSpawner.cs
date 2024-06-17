using System;
using System.Collections.Generic;
using System.Threading;
using Atomic.Elements;
using Atomic.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Components
{
    public class ZombiesSpawner : IInitializable, IDisposable
    {
        private readonly AtomicEntity _zombiePrefab;
        private readonly Transform _container;
        private readonly IReadOnlyList<Transform> _spawnPoints;
        private readonly float _spawnInterval;
        private readonly DiContainer _diContainer;
        private readonly int _maxActive;
        private readonly float _despawnDelaySec = 3;

        private readonly CancellationTokenSource _cts = new();
        private readonly List<AtomicEntity> _zombies = new();

        public ZombiesSpawner(AtomicEntity zombiePrefab
            , Transform container
            , IReadOnlyList<Transform> spawnPoints
            , int maxActive
            , float spawnInterval
            , DiContainer diContainer
        )
        {
            _zombiePrefab = zombiePrefab;
            _container = container;
            _spawnPoints = spawnPoints;
            _maxActive = maxActive;
            _spawnInterval = spawnInterval;
            _diContainer = diContainer;
        }

        void IInitializable.Initialize()
        {
            SpawnRoutine(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTask SpawnRoutine(CancellationToken ct)
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.WaitUntil(() => _zombies.Count < _maxActive);
                
                SpawnZombie();

                await UniTask.WaitForSeconds(_spawnInterval, cancellationToken: ct);
            }
        }

        private void SpawnZombie()
        {
            var point = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

            var zombie = GameObject.Instantiate(_zombiePrefab, _container);
            _zombies.Add(zombie);

            _diContainer.InjectGameObject(zombie.gameObject);

            zombie.transform.SetPositionAndRotation(point.position, point.rotation);

            var onDeath = zombie.Get<IAtomicValueObservable<bool>>(LifeAPI.IsDead);
            onDeath.Subscribe(isDead =>
            {
                if (isDead)
                    OnSomeZombieDie(zombie).Forget();
            });
        }

        private async UniTask OnSomeZombieDie(AtomicEntity zombie)
        {
            await UniTask.WaitForSeconds(_despawnDelaySec, cancellationToken: _cts.Token);

            _zombies.Remove(zombie);

            GameObject.Destroy(zombie.gameObject);
        }
    }
}