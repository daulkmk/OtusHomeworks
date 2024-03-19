using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IWeaponComponent
    {
        Vector2 Position { get; }
        void Fire(Vector2 target);
    }

    public sealed class WeaponComponent : MonoBehaviour, IWeaponComponent
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform _firePoint;

        //TODO injection
        private IBulletSystem _bulletSystem;

        public bool IsPlayer { get; set; }

        public Vector2 Position => _firePoint.position;
        public Quaternion Rotation => _firePoint.rotation;

        public Func<bool> CanFireDelegate;

        public void Initialize(IBulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public void Fire(Vector2 target)
        {
            if (!CanFireDelegate())
                return;

            var args = IBulletSystem.Args.Create(
                bulletConfig: _bulletConfig,
                position: Position,
                direction: (target - Position).normalized,
                isPlayer: IsPlayer
            );

            _bulletSystem.FireBullet(args);
        }
    }
}