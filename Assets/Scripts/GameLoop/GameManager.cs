using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IPauseGameListener
    {
        void OnGamePaused();
    }

    public interface IResumeGameListener
    {
        void OnGameResumed();
    }

    public enum GameState { Start, }

    public class GameManager : MonoBehaviour
    {
        public bool IsGamePaused { get; private set; } = false;

        public void PauseGame()
        {
            if (IsGamePaused)
                return;

            IsGamePaused = true;
        }

        public void ResumeGame()
        {
            if (!IsGamePaused)
                return;

            IsGamePaused = false;
        }

        private void Update()
        {

        }
    }
}