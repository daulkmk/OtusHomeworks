using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PathfindingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Side, MoveDirection>> _filter;

        public void Run(IEcsSystems systems)
        {
            EcsPool<Side> sidePool = _filter.Pools.Inc1;
            EcsPool<MoveDirection> moveDirectionPool = _filter.Pools.Inc2;

            foreach (int entity in _filter.Value)
            {
                Side side = sidePool.Get(entity);
                ref MoveDirection MoveDirection = ref moveDirectionPool.Get(entity);

                MoveDirection.Value = side.Value ? Vector3.right : Vector3.left;
            }
        }
    }
}