using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer
{
    public sealed class UnitInstaller : EntityInstaller
    {
        [SerializeField] private int _health = 2;
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private float _rotationsSpeed = 90f;
        [SerializeField] private float _attackRange = 5f;
        [SerializeField] private float _shootInterval = 1f;
        [SerializeField] private float _accuracyDegreesToEnemy = 10f;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private bool _side;

        [SerializeField] private Entity _bulletPrefab;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Side {Value = _side});
            entity.AddData(new Health {Value = _health});
            
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new MoveDirection {Value = Vector3.zero});
            entity.AddData(new MoveSpeed {Value = _moveSpeed});

            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new RotationDirection {Value = Vector3.zero});
            entity.AddData(new RotationSpeed {Value = _rotationsSpeed});

            entity.AddData(new TransformView { Value = transform});
            
            entity.AddData(new AttackRange {Value = _attackRange});
            entity.AddData(new Accuracy {DegreesToEnemy = _accuracyDegreesToEnemy});

            entity.AddData(new BulletWeapon
            {
                FirePoint = _firePoint,
                BulletPrefab = _bulletPrefab,
                ShootInterval = _shootInterval,
                ShootLastTime = float.MinValue
            });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}
