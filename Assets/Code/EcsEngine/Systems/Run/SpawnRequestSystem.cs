using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public sealed class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, EntityPrefab>> _filter = EcsWorlds.EVENTS;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        private readonly Transform _container;

        public SpawnRequestSystem(Transform container)
        {
            _container = container;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int @event in _filter.Value)
            {
                Vector3 position = _filter.Pools.Inc2.Get(@event).Value;
                Quaternion rotation = _filter.Pools.Inc3.Get(@event).Value;
                Entity prefab = _filter.Pools.Inc4.Get(@event).Value;

                _eventWorld.Value.DelEntity(@event);

                _entityManager.Value.Create(prefab, position, rotation, _container);
            }
        }
    }
}
