using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;

        public override void InstallBindings()
        {
            BindAndInject(_character.WeaponComponent);
            BindAndInject(_character.MoveComponent);
        }

        private void BindAndInject<T>(T obj)
        {
            Container.Bind<T>().FromInstance(obj).AsSingle();
            Container.QueueForInject(obj);
        }
    }
}