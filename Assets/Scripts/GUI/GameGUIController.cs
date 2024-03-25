using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameGUIController : MonoBehaviour, IPauseGameListener, IFinishGameListener
    {
        [SerializeField] private UI.GameGUI _gameGUI;

        void IPauseGameListener.OnGamePaused(bool paused)
        {
            if (paused)
                _gameGUI.ShowPause();
            else
                _gameGUI.ShowGame();
        }

        void IFinishGameListener.OnGameFinished()
        {
            _gameGUI.ShowGameOver();
        }
    }
}