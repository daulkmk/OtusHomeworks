using System;
using Zenject;

namespace ShootEmUp
{
    public class PauseGameTrigger : IInitializable, IDisposable
    {
        private readonly UI.IGameGUI _gameGUI;
        private readonly IInputManager _inputManager;
        private readonly IGameManager _gameManager;

        public PauseGameTrigger(UI.IGameGUI gameGUI, IInputManager inputManager, IGameManager gameManager)
        {
            _gameGUI = gameGUI;
            _inputManager = inputManager;
            _gameManager = gameManager;
        }

        void IInitializable.Initialize()
        {
            _gameGUI.OnPauseRequested += OnPauseRequested;
            _gameGUI.OnResumeRequested += OnResumeRequested;

            _inputManager.OnEscape += OnEscapeClick;
        }

        void IDisposable.Dispose()
        {
            _gameGUI.OnPauseRequested -= OnPauseRequested;
            _gameGUI.OnResumeRequested -= OnResumeRequested;

            _inputManager.OnEscape -= OnEscapeClick;
        }

        private void OnEscapeClick()
        {
            if (_gameManager.State == GameState.Playng)
                _gameManager.State = GameState.Paused;
            else if (_gameManager.State == GameState.Paused)
                _gameManager.State = GameState.Playng;
        }

        private void OnResumeRequested()
        {
            _gameManager.State = GameState.Playng;
        }

        private void OnPauseRequested()
        {
            _gameManager.State = GameState.Paused;
        }
    }
}