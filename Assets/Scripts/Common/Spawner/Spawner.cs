using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
    
namespace ShootEmUp
{
    public class Spawner<T> : ISpawner<T>
    {
        private readonly int _count = 5;
        private readonly GameObjectContext _objectContextPrefab;

        private readonly Transform _spawnedContainer;
        private readonly Transform _unspawnedContainer;

        private readonly IInstantiator _instantiator;

        private readonly Queue<(GameObject go, T obj)> _pool = new();
        private readonly List<(GameObject go, T obj)> _objectsInUse = new();

        public bool ExceedCount { get; set; }

        public bool Initialized { get; private set; }

        public Spawner(IInstantiator instantiator
            , int initialCount
            , bool exceedCount
            , GameObjectContext objectContextPrefab
            , Transform spawnedContainer
            , Transform unspawnedContainer
            )
        {
            _instantiator = instantiator;
            _count = initialCount;
            _objectContextPrefab = objectContextPrefab;
            _spawnedContainer = spawnedContainer;
            _unspawnedContainer = unspawnedContainer;

            ExceedCount = exceedCount;
        }

        public void Initialize()
        {
            int spawnCount = _count - _pool.Count;
            for (var i = 0; i < spawnCount; i++)
            {
                var obj = InstantiateObject();
                _pool.Enqueue(obj);
            }

            Initialized = true;
        }

        public T Spawn()
        {
            if (!_pool.TryDequeue(out var pair))
            {
                if (ExceedCount)
                    pair = InstantiateObject();
                else
                    return default;
            }

            _objectsInUse.Add(pair);

            pair.go.transform.SetParent(_spawnedContainer);
            return pair.obj;
        }

        public void Despawn(T obj)
        {
            int index = _objectsInUse.FindIndex(x => x.obj.Equals(obj));
            if (index == -1)
                throw new ArgumentException("Cannot despawn another's object");

            var pair = _objectsInUse[index];
            _objectsInUse.RemoveAt(index);

            pair.go.transform.SetParent(_unspawnedContainer);
            _pool.Enqueue(pair);
        }

        private (GameObject, T) InstantiateObject()
        {
            var context = _instantiator.Instantiate(_objectContextPrefab, _unspawnedContainer);            
            var obj = context.Container.Resolve<T>();

            return (context.gameObject, obj);
        }
    }
}