using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    using static IBulletSystem;

    public interface IBulletSystem
    {
        public void FireBullet(Args args);

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;

            public static Args Create(BulletConfig bulletConfig, Vector2 position, Vector2 direction, bool isPlayer)
            {
                return new Args
                {
                    isPlayer = isPlayer,
                    physicsLayer = (int)bulletConfig.PhysicsLayer,
                    color = bulletConfig.Color,
                    damage = bulletConfig.Damage,
                    position = position,
                    velocity = direction * bulletConfig.Speed
                };
            }
        }
    }

    public sealed class BulletSystem : MonoBehaviour, IBulletSystem
    {
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly List<Bullet> _activeBullets = new();
        
        private void Awake()
        {
            _bulletSpawner.Initialize();
        }

        public void FireBullet(Args args)
        {
            var bullet = _bulletSpawner.Spawn();

            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);

            bullet.OnCollisionEntered += OnBulletCollision;

            _activeBullets.Add(bullet);
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void DealDamage(Bullet bullet, GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
                damagable.ApplyDamage(bullet.damage, bullet.isPlayer);
        }

        private void FixedUpdate()
        {
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                var bullet = _activeBullets[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                    RemoveBullet(bullet);
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;

                _bulletSpawner.Despawn(bullet);
            }
        }
    }
}