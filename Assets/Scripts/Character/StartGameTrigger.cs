using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    public class StartGameTrigger : MonoBehaviour
    {
        [SerializeField] private UI.GameGUI _gameGUI;
        [SerializeField] private GameManager _gameManager;

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