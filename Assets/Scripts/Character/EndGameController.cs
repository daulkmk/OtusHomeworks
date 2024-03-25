using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private GameManager _gameManager;

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
