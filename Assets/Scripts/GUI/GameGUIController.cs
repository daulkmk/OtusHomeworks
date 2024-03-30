using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameGUIController : MonoBehaviour, IPauseGameListener, IFinishGameListener
    {
        [Inject] private UI.IGameGUI _gameGUI;

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