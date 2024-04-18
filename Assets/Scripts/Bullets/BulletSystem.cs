using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    using static IBulletSystem;

    public sealed class BulletSystem : IBulletSystem, IInitializable, IUpdatable
    {
        private readonly ISpawner<Bullet> _bulletSpawner;
        private readonly IBounds _bounds;

        private readonly List<Bullet> _activeBullets = new();

        public BulletSystem(ISpawner<Bullet> spawner, IBounds bounds)
        {
            _bulletSpawner = spawner;
            _bounds = bounds;
        }

        void IInitializable.Initialize()
        {
            _bulletSpawner.Initialize();
        }

        public void FireBullet(Args args)
        {
            var bullet = _bulletSpawner.Spawn();

            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.SetVelocity(args.velocity);

            bullet.OnDeath += OnBulletDeath;

            _activeBullets.Add(bullet);
        }

        private void OnBulletDeath(Bullet bullet)
        {
            bullet.OnDeath -= OnBulletDeath;
            RemoveBullet(bullet);
        }

        void IUpdatable.OnUpdate(float deltaTime)
        {
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                var bullet = _activeBullets[i];
                if (!_bounds.InBounds(bullet.Position))
                    RemoveBullet(bullet);
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
                _bulletSpawner.Despawn(bullet);
        }
    }
}