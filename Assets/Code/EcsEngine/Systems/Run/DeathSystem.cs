using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client
{
    sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health, TransformView>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            EcsPool<Health> poolHealth = _filter.Pools.Inc1;
            EcsPool<TransformView> poolTransform = _filter.Pools.Inc2;

            foreach (int entity in _filter.Value)
            {
                Health health = poolHealth.Get(entity);

                if (health.Value <= 0)
                {
                    TransformView transform = poolTransform.Get(entity);
                    transform.Value.gameObject.SetActive(false);

                    _entityManager.Value.Destroy(entity);
                }
            }
        }
    }
}