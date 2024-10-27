using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class FireRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, BulletWeapon>> _filter;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<EntityPrefab> _prefabPool = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems)
        {
            float time = Time.time;

            EcsPool<BulletWeapon> weaponPool = _filter.Pools.Inc2;
            EcsPool<FireRequest> requestPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value)
            {
                ref BulletWeapon weapon = ref weaponPool.Get(entity);

                if (time < weapon.ShootLastTime + weapon.ShootInterval + Random.value)
                    continue;
                
                weapon.ShootLastTime = time;
                
                int spawnEvent = _eventWorld.Value.NewEntity();
                
                _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
                _positionPool.Value.Add(spawnEvent) = new Position {Value = weapon.FirePoint.position};
                _rotationPool.Value.Add(spawnEvent) = new Rotation {Value = weapon.FirePoint.rotation};
                _prefabPool.Value.Add(spawnEvent) = new EntityPrefab {Value = weapon.BulletPrefab};
                
                requestPool.Del(entity);
            }
        }
    }
}