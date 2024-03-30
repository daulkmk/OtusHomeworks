using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PauseGameTrigger : MonoBehaviour
    {
        [Inject] private UI.IGameGUI _gameGUI;
        [Inject] private IInputManager _inputManager;
        [Inject] private IGameManager _gameManager;

        private void Awake()
        {
            _gameGUI.OnPauseRequested += OnPauseRequested;
            _gameGUI.OnResumeRequested += OnResumeRequested;

            _inputManager.OnEscape += OnEscapeClick;
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