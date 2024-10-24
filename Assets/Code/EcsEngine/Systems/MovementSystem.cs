using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            #region MyRegion

            // EcsWorld world = systems.GetWorld();
            // EcsFilter filter = world.Filter<MoveDirection>().Inc<MoveSpeed>().Inc<Position>().End();
            //
            // EcsPool<MoveDirection> directionPool = world.GetPool<MoveDirection>();
            // EcsPool<MoveSpeed> speedPool = world.GetPool<MoveSpeed>();
            // EcsPool<Position> positionPool = world.GetPool<Position>();

            #endregion

            EcsPool<MoveDirection> directionPool = _filter.Pools.Inc1;
            EcsPool<MoveSpeed> speedPool = _filter.Pools.Inc2;
            EcsPool<Position> positionPool = _filter.Pools.Inc3;
            
            foreach (int entity in _filter.Value)
            {
                MoveDirection moveDirection = directionPool.Get(entity);
                MoveSpeed moveSpeed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}
