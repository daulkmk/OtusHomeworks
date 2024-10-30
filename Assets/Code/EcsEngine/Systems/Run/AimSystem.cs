using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public sealed class AimSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackTarget, Rotation, RotationDirection, Accuracy>> _filter;
        private readonly EcsPoolInject<Position> _poolPositions;
        private readonly EcsPoolInject<FireRequest> _poolFireRequest;

        public void Run(IEcsSystems systems)
        {
            EcsPool<AttackTarget> targetPool = _filter.Pools.Inc1;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc2;
            EcsPool<RotationDirection> rotationDirectionPool = _filter.Pools.Inc3;
            EcsPool<Accuracy> accuracyPool = _filter.Pools.Inc4;

            foreach (int entity in _filter.Value)
            {
                Position position = _poolPositions.Value.Get(entity);
                Rotation rotation = rotationPool.Get(entity);

                AttackTarget target = targetPool.Get(entity);
                Position targetPosition = _poolPositions.Value.Get(target.Entity);

                Vector3 directionToTarget = targetPosition.Value - position.Value;
                Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

                ref RotationDirection rotationDirection = ref rotationDirectionPool.Get(entity);
                rotationDirection.Value = directionToTarget;

                float angleToTarget = Quaternion.Angle(rotationToTarget, rotation.Value);

                Accuracy accuracy = accuracyPool.Get(entity);
                if (accuracy.DegreesToEnemy > angleToTarget)
                {
                    if (!_poolFireRequest.Value.Has(entity))
                        _poolFireRequest.Value.Add(entity);
                }
            }
        }
    }
}