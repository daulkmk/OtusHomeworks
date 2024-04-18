using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ShootEmUp
{
    public class GameListenersHandlerInstaller : MonoInstaller
    {
        private enum MonoListenersSource { Scene, GameObject }

        [SerializeField] private MonoListenersSource _monoListenersSource = MonoListenersSource.GameObject;

        public override void InstallBindings()
        {
            Container.Bind<GameListenersHandler>()
                .AsSingle()
                .WithArguments(GetMonoListeners())
                .NonLazy();
        }

        private IGameManagerListener[] GetMonoListeners()
        {
            switch (_monoListenersSource)
            {
                case MonoListenersSource.Scene:
                    {
                        var objects = new List<IGameManagerListener>();
                        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
                            objects.AddRange(go.GetComponentsInChildren<IGameManagerListener>(true));
                        return objects.ToArray();
                    }
                case MonoListenersSource.GameObject:
                    {
                        return GetComponentsInChildren<IGameManagerListener>(true);
                    }
                default:
                    throw new System.NotImplementedException(_monoListenersSource.ToString());
            }
        }
    }
}