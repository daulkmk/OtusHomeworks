using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class ProjectileDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Collision, DamageSource, Health>> _filter;
        private readonly EcsPoolInject<Damage> _poolDamage;

        public void Run(IEcsSystems systems)
        {
            EcsPool<Collision> poolCollision = _filter.Pools.Inc1;
            EcsPool<DamageSource> poolDamageSource = _filter.Pools.Inc2;

            EcsPool<Damage> poolDamage = _poolDamage.Value;

            foreach (int entity in _filter.Value)
            {
                Collision collision = poolCollision.Get(entity);
                DamageSource damageSource = poolDamageSource.Get(entity);

                //Deal damage to target
                ApplyDamage(collision.Entity, damageSource.Value);
                //Deal damage to self (destroy projectile)
                ApplyDamage(entity, damageSource.Value);

                void ApplyDamage(int entityToDamage, int damageValue)
                {
                    if (poolDamage.Has(entityToDamage))
                    {
                        ref Damage damage = ref poolDamage.Get(entityToDamage);
                        damage.Value += damageSource.Value;
                    }
                    else
                    {
                        ref Damage damage = ref poolDamage.Add(entityToDamage);
                        damage.Value = damageSource.Value;
                    }
                }
            }
        }
    }
}