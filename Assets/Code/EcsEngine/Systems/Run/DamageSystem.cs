using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public sealed class DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health, Damage>> _filter;

        public void Run(IEcsSystems systems)
        {
            EcsPool<Health> poolHealth = _filter.Pools.Inc1;
            EcsPool<Damage> poolDamage = _filter.Pools.Inc2;

            foreach (int entity in _filter.Value)
            {
                Damage damage = poolDamage.Get(entity);
                ref Health health = ref poolHealth.Get(entity);

                if (health.Value > 0)
                {
                    health.Value = UnityEngine.Mathf.Min(0, health.Value - damage.Value);
                    poolDamage.Del(entity);
                }
            }
        }
    }
}