using System;
using Cysharp.Threading.Tasks;
using Lessons.Game.Events;
using Lessons.Game.Services;
using UnityEngine;

namespace Lessons.Game.Turn.Logic.Tasks
{
    public sealed class StartTurnTask : Task
    {
        private readonly PlayersService _playersService;
        private readonly EventBus _eventBus;
        private int _turn = -1;

        public StartTurnTask(PlayersService playerService, EventBus eventBus)
        {
            _playersService = playerService;
            _eventBus = eventBus;
        }

        protected override UniTask OnRun()
        {
            Debug.Log("Pipeline Started!");

            var previousHero = _playersService.PlayerHero;

            _playersService.NextPlayer();

            _turn++;
            if (_turn == _playersService.PlayersCount)
            {
                _playersService.NextHero();
                _turn = 0;
            }

            var acitveHero = _playersService.PlayerHero;

            _eventBus.RaiseEvent(new SelectHeroEvent(acitveHero, previousHero));

            if (acitveHero == null)
            {
                Debug.Log("!!!!! GAME END !!!!!");
                throw new OperationCanceledException();
            }

            return UniTask.CompletedTask;
        }
    }
}