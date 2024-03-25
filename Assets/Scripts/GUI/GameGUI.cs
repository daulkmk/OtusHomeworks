using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.UI
{
    public interface IGameGUI
    {
        public event Action OnPauseRequested;
        public event Action OnResumeRequested;

        public void ShowStartGameSequence(Action onComplete);

        public void ShowPause();
        public void ShowGame();
        public void ShowGameOver();
    }

    public class GameGUI : MonoBehaviour, IGameGUI
    {
        [SerializeField] private SingleButtonScreen _startScreen;
        [SerializeField] private SingleButtonScreen _gameScreen;
        [SerializeField] private SingleButtonScreen _pauseScreen;
        [SerializeField] private int _countdownTime = 3;
        [SerializeField] private CountdownScreen _countdownScreen;
        [SerializeField] private GameObject _gameOverScreen;

        public event Action OnPauseRequested;
        public event Action OnResumeRequested;

        private event Action OnShowStartGameSequenceEnd;

        private enum GuiState { Start, Countdown, Game, Pause, GameOver }

        public void ShowStartGameSequence(Action onComplete)
        {
            OnShowStartGameSequenceEnd = onComplete;
            SetState(GuiState.Start);
        }

        public void ShowPause() => SetState(GuiState.Pause);
        public void ShowGame() => SetState(GuiState.Game);
        public void ShowGameOver() => SetState(GuiState.GameOver);

        private void HideAll()
        {
            _gameScreen.Hide();
            _pauseScreen.Hide();
            _countdownScreen.Hide();
            _startScreen.Hide();
            _gameOverScreen.SetActive(false);
        }

        private void SetState(GuiState state)
        {
            HideAll();

            Debug.Log("[GUI] state = " + state.ToString());

            switch (state)
            {
                case GuiState.Start:
                    _startScreen.Show(OnStartRequested);
                    break;
                case GuiState.Countdown:
                    _countdownScreen.Show(_countdownTime, OnCountdownComplete);
                    break;
                case GuiState.Game:
                    _gameScreen.Show(OnPauseRequest);
                    break;
                case GuiState.Pause:
                    _pauseScreen.Show(OnResumeRequest);
                    break;
                case GuiState.GameOver:
                    _gameOverScreen.SetActive(true);
                    break;
                default:
                    throw new NotImplementedException(state.ToString());
            }
        }

        private void OnStartRequested() => SetState(GuiState.Countdown);

        private void OnCountdownComplete()  
        {            
            OnShowStartGameSequenceEnd?.Invoke();
            OnShowStartGameSequenceEnd = null;
        }

        private void OnPauseRequest()
        {
            OnPauseRequested?.Invoke();
        }

        private void OnResumeRequest()
        {
            OnResumeRequested.Invoke();
        }
    }
}