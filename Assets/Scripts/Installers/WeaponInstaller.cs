using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private bool _isPlayer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BulletArgsFactory>()
                .AsSingle()
                .WithArguments(_bulletConfig, _isPlayer);

            Container.BindInterfacesTo<Weapon>()
                .AsSingle()
                .WithArguments(new TransformInfo(_firePoint));
        }
    }
}