using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public interface IInstantiator
    {
        T Instantiate<T>(T original, Transform parent = null
            , bool injectDependencies = true
            , bool listenToGameManager = true
        ) where T : Object;
    }

    public class Instantiator : IInstantiator
    {
        private readonly DiContainer _diContainer;
        private readonly IGameManager _gameManager;

        public Instantiator(DiContainer diContainer, IGameManager gameManager)
        {
            _diContainer = diContainer;
            _gameManager = gameManager;
        }

        public T Instantiate<T>(T original, Transform parent = null
            , bool injectDependencies = true
            , bool listenToGameManager = true
            ) where T : Object
        {
            var instance = Object.Instantiate(original, parent);

            GameObject gameObject;

            if (instance is Component component)
                gameObject = component.gameObject;
            else if (instance is GameObject go)
                gameObject = go;
            else
                throw new System.NotImplementedException("Cannot obtain GameObject from " + typeof(T).Name);

            if (injectDependencies)
                _diContainer.InjectGameObject(gameObject);
            if (listenToGameManager)
                _gameManager.AddAllListeners(gameObject);

            return instance;
        }
    }
}