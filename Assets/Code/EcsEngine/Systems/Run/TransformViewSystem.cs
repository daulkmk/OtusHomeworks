using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public sealed class TransformViewSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView>> _transformFilter;
        private readonly EcsPoolInject<Rotation> _rotationPool;
        private readonly EcsPoolInject<Position> _positionPool;

        public void PostRun(IEcsSystems systems)
        {
            EcsPool<TransformView> transformPool = _transformFilter.Pools.Inc1;

            foreach (int entity in _transformFilter.Value)
            {
                ref TransformView transform = ref transformPool.Get(entity);

                if (!transform.Value.gameObject.activeSelf)
                    continue;

                Vector3 position = transform.Value.position;
                if (_positionPool.Value.Has(entity))
                {
                    position = _positionPool.Value.Get(entity).Value;
                }

                Quaternion rotation = transform.Value.rotation;
                if (_rotationPool.Value.Has(entity))
                {
                    rotation = _rotationPool.Value.Get(entity).Value;
                }

                transform.Value.SetPositionAndRotation(position, rotation);
            }
        }
    }
}
