using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Spawner<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private int _count = 5;
        [SerializeField] private T _prefab;

        [SerializeField] private Transform _spawnedContainer;
        [SerializeField] private Transform _unspawnedContainer;

        private readonly Queue<T> _pool = new();
        private Action<T> _initializeObjectDelegate = null;

        [field: SerializeField]
        public bool ExceedCount { get; set; }

        public void Initialize(Action<T> initializeObjectDelegate = null)
        {
            _initializeObjectDelegate = initializeObjectDelegate;

            int spawnCount = _count - _pool.Count;
            for (var i = 0; i < spawnCount; i++)
            {
                var obj = SpawnInitialized();
                _pool.Enqueue(obj);
            }
        }

        private T SpawnInitialized()
        {
            var obj = Instantiate(_prefab, _unspawnedContainer);

            OnObjectSpawned(obj);

            _initializeObjectDelegate?.Invoke(obj);

            return obj;
        }

        protected virtual void OnObjectSpawned(T obj) { }

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

        public void Despawn(T enemy)
        {
            enemy.transform.SetParent(_unspawnedContainer);
            _pool.Enqueue(enemy);
        }
    }
}