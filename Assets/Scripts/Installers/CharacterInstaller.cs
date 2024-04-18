using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private bool _isPlayer = true;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TransformInfo>()
                .AsSingle()
                .WithArguments(transform);

            Container.BindInterfacesTo<GameObjectInfo>()
                .AsSingle()
                .WithArguments(gameObject);

            Container.BindInterfacesAndSelfTo<Character>()
                .AsSingle()
                .WithArguments(_isPlayer);
        }
    }
}