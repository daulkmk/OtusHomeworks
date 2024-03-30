using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class StartGameTrigger : MonoBehaviour
    {
        [Inject] private UI.IGameGUI _gameGUI;
        [Inject] private IGameManager _gameManager;

        private void Start()
        {
            _gameGUI.ShowStartGameSequence(OnGuiStartSequenceComplete);
        }

        private void OnGuiStartSequenceComplete()
        {
            _gameManager.State = GameState.Playng;
        }
    }
}