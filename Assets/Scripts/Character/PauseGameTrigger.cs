using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PauseGameTrigger : MonoBehaviour
    {
        [SerializeField] private UI.GameGUI _gameGUI;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private GameManager _gameManager;

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