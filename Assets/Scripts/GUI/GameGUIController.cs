using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.UI
{
    public class GameGUIController : MonoBehaviour
    {
        [SerializeField] private SingleButtonScreen _startScreen;
        [SerializeField] private SingleButtonScreen _gameScreen;
        [SerializeField] private SingleButtonScreen _pauseScreen;
        [SerializeField] private int _countdownTime = 3;
        [SerializeField] private CountdownScreen _countdownScreen;

        private enum GuiState { Start, Countdown, Game, Pause }

        private void OnEnable()
        {
            SetState(GuiState.Start);
        }

        private void SetState(GuiState state)
        {
            switch (state)
            {
                case GuiState.Start:
                    _gameScreen.Hide();
                    _pauseScreen.Hide();
                    _countdownScreen.Hide();
                    _startScreen.Show(OnStartRequested);
                    break;
                case GuiState.Countdown:
                    _startScreen.Hide();
                    _gameScreen.Hide();
                    _pauseScreen.Hide();
                    _countdownScreen.Show(_countdownTime, OnCountdownComplete);
                    break;
                case GuiState.Game:
                    _startScreen.Hide();
                    _pauseScreen.Hide();
                    _countdownScreen.Hide();
                    _gameScreen.Show(OnPauseRequested);
                    break;
                case GuiState.Pause:
                    _startScreen.Hide();
                    _countdownScreen.Hide();
                    _gameScreen.Hide();
                    _pauseScreen.Show(OnResumeRequested);
                    break;
                default:
                    throw new System.NotImplementedException(state.ToString());
            }
        }

        private void OnStartRequested() => SetState(GuiState.Countdown);

        private void OnCountdownComplete() => SetState(GuiState.Game);

        private void OnPauseRequested() => SetState(GuiState.Pause);

        private void OnResumeRequested() => SetState(GuiState.Game);
    }
}