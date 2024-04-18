using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UI.GameGUI _gameGUI;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameManager>().AsSingle();

            Container.BindInterfacesTo<Instantiator>().AsSingle();
            Container.BindInterfacesTo<InputManager>().AsSingle();

            BindInterfacesAndInject(_gameGUI);

            Container.BindInterfacesTo<BulletSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyManager>().AsSingle();

            Container.BindInterfacesTo<StartGameTrigger>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PauseGameTrigger>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EndGameController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<GameGUIController>().AsSingle();
        }

        private void BindInterfacesAndInject<T>(T obj)
        {
            Container.BindInterfacesTo<T>().FromInstance(obj).AsSingle();
            Container.QueueForInject(obj);
        }
    }
}