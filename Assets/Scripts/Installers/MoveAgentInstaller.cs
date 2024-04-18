using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class MoveAgentInstaller : MonoInstaller
    {
        [SerializeField] private float _destinationReachedDistance = 0.25f;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MoveAgent>()
                .AsSingle()
                .WithArguments(_destinationReachedDistance);
        }
    }
}