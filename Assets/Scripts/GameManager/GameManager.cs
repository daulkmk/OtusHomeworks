using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public enum GameState { None, Playng, Paused, Finished }

    public interface IGameManager
    {
        GameState State { get; set; }

        void AddAllListeners(GameObject go);
        void AddUpdateListener(IUpdatable updatable);
    }

    //TODO RemoveAllListeners, RemoveUpdateListener, ILateUpdateListener, IFixedUpdateListener, IGameStateListener
    public sealed class GameManager : IGameManager, IInitializable, ITickable, IFixedTickable
    {
        private List<IPauseGameListener> _gamePauseListeners = new();
        private List<IFinishGameListener> _gameFinishListeners = new();
        private List<IStartGameListener> _gameStartListeners = new();

        private List<IUpdatable> _updateListeners = new();
        private List<IFixedUpdatable> _fixedUpdateListeners = new();

        private GameState _state = GameState.None;
        public GameState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                Debug.Log("[GAME] state = " + value.ToString());

                switch (value)
                {
                    case GameState.Playng:
                        if (_state == GameState.None)
                            StartGame();
                        else
                            ResumeGame();
                        break;
                    case GameState.Paused:
                        PauseGame();
                        break;
                    case GameState.Finished:
                        FinishGame();
                        break;
                    default: throw new System.NotImplementedException();
                }
            }
        }

        void IInitializable.Initialize()
        {
            FindSceneAllListeners();
        }

        void FindSceneAllListeners()
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            foreach (var go in scene.GetRootGameObjects())
                AddAllListeners(go);
        }

        public void AddAllListeners(GameObject go)
        {
            Add(_updateListeners);
            Add(_fixedUpdateListeners);
            Add(_gamePauseListeners);
            Add(_gameFinishListeners);
            Add(_gameStartListeners);

            void Add<T>(List<T> list)
            {
                foreach (var obj in go.GetComponentsInChildren<T>(true))
                {
                    if (!list.Contains(obj))
                        list.Add(obj);
                }
            }
        }

        void IGameManager.AddUpdateListener(IUpdatable updatable)
        {
            if (!_updateListeners.Contains(updatable))
                _updateListeners.Add(updatable);
        }

        private void StartGame()
        {
            if (_state != GameState.None)
                ThrowStateException(GameState.Playng);
            
            for (int i = 0; i < _gameStartListeners.Count; i++)
                _gameStartListeners[i].OnGameStarting();

            ResumeGame();
        }

        private void FinishGame()
        {
            if (_state == GameState.None)
                ThrowStateException(GameState.Finished);

            Debug.Log("Game over!");

            PauseGame();

            _state = GameState.Finished;

            for (int i = 0; i < _gameFinishListeners.Count; i++)
                _gameFinishListeners[i].OnGameFinished();
        }

        private void PauseGame()
        {
            if (_state != GameState.Playng)
                ThrowStateException(GameState.Paused);

            _state = GameState.Paused;
            Time.timeScale = 0;

            for (int i = 0; i < _gamePauseListeners.Count; i++)
                _gamePauseListeners[i].OnGamePaused(true);
        }

        private void ResumeGame()
        {
            bool canBeResumed = _state is GameState.None or GameState.Paused;
            if (!canBeResumed)
                ThrowStateException(GameState.Playng);

            _state = GameState.Playng;
            Time.timeScale = 1;

            for (int i = 0; i < _gamePauseListeners.Count; i++)
                _gamePauseListeners[i].OnGamePaused(false);
        }

        void ITickable.Tick()
        {
            if (_state != GameState.Playng)
                return;

            var dt = Time.deltaTime;

            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate(dt);
        }

        void IFixedTickable.FixedTick()
        {
            if (_state != GameState.Playng)
                return;

            var dt = Time.fixedDeltaTime;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate(dt);
        }

        void ThrowStateException(GameState value)
        {
            throw new System.NotImplementedException($"Cannot change state from {_state} to {value}");
        }
    }
}