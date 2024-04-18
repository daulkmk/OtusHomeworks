using System;
using Zenject;

namespace ShootEmUp
{
    public class EndGameController : IInitializable, IDisposable
    {
        private readonly IGameManager _gameManager;
        private readonly Character _character;

        public EndGameController(Character character, IGameManager gameManager)
        {
            _gameManager = gameManager;
            _character = character;
        }

        void IInitializable.Initialize()
        {
            _character.OnDeath += OnCharacterDeath;
        }

        void IDisposable.Dispose()
        {
            _character.OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(Character _)
        {
            _gameManager.State = GameState.Finished;
        }
    }
}
