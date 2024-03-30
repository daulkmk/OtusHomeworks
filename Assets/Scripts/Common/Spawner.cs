using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
    
namespace ShootEmUp
{
    public interface ISpawner<T>
    {
        bool Initialized { get; }

        void Initialize(Action<T> initializeObjectDelegate = null);

        T Spawn();
        void Despawn(T obj);
    }

    public class Spawner<T> : MonoBehaviour, ISpawner<T> where T : Component
    {
        [SerializeField] private int _count = 5;
        [SerializeField] private T _prefab;

        [SerializeField] private Transform _spawnedContainer;
        [SerializeField] private Transform _unspawnedContainer;

        [Inject] private IInstantiator _instantiator;

        private readonly Queue<T> _pool = new();
        private Action<T> _initializeObjectDelegate = null;

        [field: SerializeField]
        public bool ExceedCount { get; set; }

        public bool Initialized { get; private set; }

        public void Initialize(Action<T> initializeObjectDelegate = null)
        {
            _initializeObjectDelegate = initializeObjectDelegate;

            int spawnCount = _count - _pool.Count;
            for (var i = 0; i < spawnCount; i++)
            {
                var obj = SpawnInitialized();
                _pool.Enqueue(obj);
            }

            Initialized = true;
        }

        public T Spawn()
        {
            if (!_pool.TryDequeue(out var obj))
            {
                if (ExceedCount)
                    obj = SpawnInitialized();
                else
                    return null;
            }

            obj.transform.SetParent(_spawnedContainer);
            return obj;
        }

        public void Despawn(T obj)
        {
            obj.transform.SetParent(_unspawnedContainer);
            _pool.Enqueue(obj);
        }

        private T SpawnInitialized()
        {
            var obj = _instantiator.Instantiate(_prefab, _unspawnedContainer);

            OnObjectSpawned(obj);

            _initializeObjectDelegate?.Invoke(obj);

            return obj;
        }

        protected virtual void OnObjectSpawned(T obj) { }
    }
}