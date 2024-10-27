using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RotationDirection, RotationSpeed, Rotation>> _filter;

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            EcsPool<RotationDirection> directionPool = _filter.Pools.Inc1;
            EcsPool<RotationSpeed> speedPool = _filter.Pools.Inc2;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc3;

            foreach (int entity in _filter.Value)
            {
                RotationDirection direction = directionPool.Get(entity);

                if (direction.Value == Vector3.zero)
                    continue;

                RotationSpeed speed = speedPool.Get(entity);

                ref Rotation rotation = ref rotationPool.Get(entity);

                Quaternion targetRotation = Quaternion.LookRotation(direction.Value);
                rotation.Value = Quaternion.RotateTowards(rotation.Value, targetRotation, speed.Value * deltaTime);
            }
        }
    }
}