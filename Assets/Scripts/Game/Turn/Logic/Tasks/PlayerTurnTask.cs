using Cysharp.Threading.Tasks;
using Entities;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Services;
using Lessons.Level;
using VContainer;

namespace Lessons.Game.Turn.Logic.Tasks
{
    public sealed class PlayerTurnTask : Task
    {
        private readonly EventBus _eventBus;
        private readonly PlayersService _playerService;

        private UniTaskCompletionSource _tcs;
        
        [Inject]
        public PlayerTurnTask(EventBus eventBus, PlayersService playerService, HeroesViewMap map)
        {
            _eventBus = eventBus;
            _playerService = playerService;
        }

        protected override UniTask OnRun()
        {
            if (_playerService.PlayerHero.TryGet(out SkipTurnComponent skipTurn))
            {
                _playerService.PlayerHero.Remove(skipTurn);

                UnityEngine.Debug.Log("SKIP TURN");
                return UniTask.CompletedTask;
            }

            _tcs = new();
            _playerService.OnEnemyClicked += OnEnemyHeroClicked;

            _eventBus.RaiseEvent(new TurnStartedEvent());

            return _tcs.Task;
        }

        private void OnEnemyHeroClicked(IEntity enemy)
        {
            _eventBus.RaiseEvent(new PlayerSelectedAttackTargetEvent(_playerService.PlayerHero, enemy));
            _tcs.TrySetResult();
        }

        protected override UniTask OnFinish()
        {
            _playerService.OnEnemyClicked -= OnEnemyHeroClicked;
            return UniTask.CompletedTask;
        }
    }
}