using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FindTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Side, Position, AttackRange>> _filterAttackers;
        private readonly EcsFilterInject<Inc<Side, Position>> _filterTargets;
        private readonly EcsPoolInject<AttackTarget> _targetPool;

        public void Run(IEcsSystems systems)
        {
            EcsPool<Side> sidesPool = _filterAttackers.Pools.Inc1;
            EcsPool<Position> positionsPool = _filterAttackers.Pools.Inc2;
            EcsPool<AttackRange> rangePool = _filterAttackers.Pools.Inc3;

            EcsPool<Side> targetSidesPool = _filterTargets.Pools.Inc1;
            EcsPool<Position> targetPositionsPool = _filterTargets.Pools.Inc2;

            EcsPool<AttackTarget> targetPool = _targetPool.Value;
            
            foreach (int entity in _filterAttackers.Value)
            {
                Side side = sidesPool.Get(entity);
                Position position = positionsPool.Get(entity);
                AttackRange range = rangePool.Get(entity);

                int? clothestTarget = null;
                float clothestDistance = float.MaxValue;

                foreach (int targetEntity in _filterTargets.Value)
                {
                    Side targetSide = targetSidesPool.Get(targetEntity);

                    if (targetSide.Value == side.Value)
                        continue;

                    Position targetPosition = targetPositionsPool.Get(targetEntity);
                    float distance = Vector3.Distance(position.Value, targetPosition.Value);

                    if (distance > range.Value)
                        continue;
                    
                    if (distance < clothestDistance)
                    {
                        clothestTarget = targetEntity;
                        clothestDistance = distance;
                    }
                }

                if (clothestTarget.HasValue)
                {
                    if (targetPool.Has(entity))
                    {
                        ref AttackTarget target = ref targetPool.Get(entity);
                        target.Entity = clothestTarget.Value;
                    }
                    else
                    {
                        ref AttackTarget target = ref targetPool.Add(entity);
                        target.Entity = clothestTarget.Value;
                    }
                }
                else
                {
                    targetPool.Del(entity);
                }
            }
        }
    }
}