using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Entities;
using Lessons.Game.Events;
using Lessons.Game.Services;
using UnityEngine;
using VContainer;

namespace Lessons.Game.Turn.Logic.Tasks
{
    public sealed class PlayerTurnTask : Task
    {
        private readonly KeyboardInput _input;
        private readonly EventBus _eventBus;
        private readonly IEntity _player;

        private UniTaskCompletionSource _tcs;
        
        [Inject]
        public PlayerTurnTask(KeyboardInput input, EventBus eventBus, PlayerService playerService)
        {
            _input = input;
            _eventBus = eventBus;
            _player = playerService.Player;
        }
        
        protected override UniTask OnRun()
        {
            _tcs = new();
            _input.OnMove += OnMovePreformed;

            return _tcs.Task;
        }

        protected override UniTask OnFinish()
        {
            _input.OnMove -= OnMovePreformed;
            return UniTask.CompletedTask;
        }

        private void OnMovePreformed(Vector2Int direction)
        {
            _eventBus.RaiseEvent(new ApplyDirectionEvent(_player, direction));
            _tcs.TrySetResult();
        }
    }
}