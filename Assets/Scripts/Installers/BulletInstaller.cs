using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private bool _isPlayer;
        [SerializeField] private int _damage;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TransformInfo>()
                .AsSingle()
                .WithArguments(transform);

            Container.BindInterfacesTo<GameObjectInfo>()
                .AsSingle()
                .WithArguments(gameObject);

            Container.BindInterfacesAndSelfTo<Bullet>()
                .AsSingle()
                .WithArguments(_isPlayer, _damage, _rigidbody2D, _spriteRenderer);
        }
    }
}