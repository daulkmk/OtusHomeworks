using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UI.GameGUI _gameGUI;

        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameManager>().AsSingle();

            Container.BindInterfacesTo<Instantiator>().AsSingle();
            Container.BindInterfacesTo<InputManager>().AsSingle();

            BindInterfacesAndInject(_gameGUI);
            BindInterfacesAndInject(_bulletSpawner);
            
            Container.BindInterfacesTo<BulletSystem>().AsSingle().NonLazy();

            BindInterfacesAndInject(_enemySpawner);

            Container.BindInterfacesTo<EnemyManager>().AsSingle();
        }

        private void BindInterfacesAndInject<T>(T obj)
        {
            Container.BindInterfacesTo<T>().FromInstance(obj).AsSingle();
            Container.QueueForInject(obj);
        }
    }
}