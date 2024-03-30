using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Character _character;

        public override void InstallBindings()
        {
            Container.Bind<Character>().FromInstance(_character).AsSingle();

            BindInterfacesAndInject(_levelBounds);
            BindInterfacesAndInject(_enemyPositions);
        }

        private void BindInterfacesAndInject<T>(T obj)
        {
            Container.BindInterfacesTo<T>().FromInstance(obj).AsSingle();
            Container.QueueForInject(obj);
        }
    }
}