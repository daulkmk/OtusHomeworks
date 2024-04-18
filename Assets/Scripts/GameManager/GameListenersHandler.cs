using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public class GameListenersHandler : IDisposable, IInitializable
    {
        private readonly IGameManager _gameManager;

        private readonly List<IGameManagerListener> _listeners = new();
        private readonly IGameManagerListener[] _monoListeners = null;

        public GameListenersHandler(IGameManager gameManager, IGameManagerListener[] monoListeners)
        {
            _gameManager = gameManager;
            _monoListeners = monoListeners;
        }

        [Inject]
        public void AddListeners([InjectLocal] IGameManagerListener[] listeners)
        {
            if (listeners != null)
            {
                _listeners.AddRange(listeners);
                _gameManager.AddAllListeners(listeners);
            }
        }

        void IInitializable.Initialize()
        {
            if (_monoListeners != null)
                _gameManager.AddAllListeners(_monoListeners);
        }

        void IDisposable.Dispose()
        {
            _gameManager.RemoveAllListeners(_listeners);
            if (_monoListeners != null)
                _gameManager.RemoveAllListeners(_monoListeners);
        }
    }
}