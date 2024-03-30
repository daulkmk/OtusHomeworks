using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EndGameController : MonoBehaviour
    {
        [Inject] private Character _character;
        [Inject] private IGameManager _gameManager;

        private void Awake()
        {
            _character.OnDeath += OnCharacterDeath;
        }

        private void OnCharacterDeath(Character _)
        {
            _gameManager.State = GameState.Finished;
        }
    }
}
