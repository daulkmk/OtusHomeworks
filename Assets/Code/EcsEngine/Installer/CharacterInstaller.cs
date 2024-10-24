using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer
{
    public sealed class CharacterInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private Entity _bulletPrefab;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveDirection {Value = Vector3.forward});
            entity.AddData(new MoveSpeed {Value = _moveSpeed});
            entity.AddData(new TransformView { Value = transform});
            entity.AddData(new BulletWeapon
            {
                FirePoint = _firePoint,
                BulletPrefab = _bulletPrefab
            });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}
