using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class AttackAgentInstaller : MonoInstaller
    {
        [SerializeField] private float _countdown;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AttackAgent>()
                .AsSingle()
                .WithArguments(_countdown);
        }
    }
}