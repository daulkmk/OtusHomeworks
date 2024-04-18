using System;

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
}