using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer
{

    internal sealed class BulletInstaller : EntityInstaller
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _moveSpeed = 3.0f;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveSpeed {Value = _moveSpeed});
            entity.AddData(new DamageSource {Value = _damage});
            entity.AddData(new Health {Value = 1});
            entity.AddData(new TransformView {Value = transform});
            entity.AddData(new MoveDirection { Value = transform.TransformDirection(Vector3.forward) });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}
