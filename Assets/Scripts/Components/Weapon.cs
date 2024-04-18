using UnityEngine;
using Zenject;
using R3;

namespace ShootEmUp
{
    public sealed class Weapon :  IWeapon
    {
        private readonly IBulletSystem _bulletSystem;
        private readonly ITransform _firePoint;
        private readonly IFactory<Vector2, Vector2, IBulletSystem.Args> _bulletArgsFactory;

        public Vector2 Position => _firePoint.Position;
        public Quaternion Rotation => _firePoint.Rotation;

        public ReactiveProperty<bool> CanFire { get; private set; }

        public Weapon(IBulletSystem bulletSystem, IFactory<Vector2, Vector2, IBulletSystem.Args> bulletArgsFactory, ITransform firePoint)
        {
            CanFire = new ReactiveProperty<bool>(false);

            _bulletArgsFactory = bulletArgsFactory;
            _bulletSystem = bulletSystem;
            _firePoint = firePoint;
        }

        public void Fire(Vector2 target)
        {
            if (!CanFire.Value)
                return;

            var direction = (target - Position).normalized;
            var args = _bulletArgsFactory.Create(Position, direction);

            _bulletSystem.FireBullet(args);
        }
    }
}