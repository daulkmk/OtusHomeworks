using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class MoveInstaller : MonoInstaller
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MoveComponent>()
                .AsSingle()
                .WithArguments(_speed, _rigidbody2D);
        }
    }
}