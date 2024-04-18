using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletArgsFactory : IFactory<Vector2, Vector2, IBulletSystem.Args>
    {
        private readonly IBulletConfig _bulletConfig;
        private readonly bool _isPlayer;

        public BulletArgsFactory(IBulletConfig bulletConfig, bool isPlayer)
        {
            _bulletConfig = bulletConfig;
            _isPlayer = isPlayer;
        }

        IBulletSystem.Args IFactory<Vector2, Vector2, IBulletSystem.Args>.Create(Vector2 position, Vector2 direction)
        {
            return new IBulletSystem.Args()
            {
                isPlayer = _isPlayer,
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = position,
                velocity = direction * _bulletConfig.Speed
            };           
        }
    }
}