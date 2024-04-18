using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class HitPointsInstaller : MonoInstaller
    {
        [SerializeField] private int _maxHitPoints = 10;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<HitPoints>()
                .AsSingle()
                .WithArguments(_maxHitPoints);
        }
    }
}