using System.Linq;
using Entities;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;
using Lessons.Game.Services;

namespace Lessons.Game.Handlers.Effects
{
    public sealed class SelectRandomTargetWitchChanceEffectHandler : BaseHandler<SelectRandomTargetWitchChanceEffectEvent>
    {
        private readonly PlayersService _playersService;

        public SelectRandomTargetWitchChanceEffectHandler(EventBus eventBus, PlayersService playersService) : base(eventBus)
        {
            _playersService = playersService;
        }

        protected override void HandleEvent(SelectRandomTargetWitchChanceEffectEvent evt)
        {
            IEntity target = evt.Target;

            bool randomizeTarget = UnityEngine.Random.value < evt.Chance;
            if (randomizeTarget)
            {
                var enemiesList = _playersService.Enemies.ToList();
                target = enemiesList[UnityEngine.Random.Range(0, enemiesList.Count)];
            }

            EventBus.RaiseEvent(new AttackEvent(evt.Source, target));
        }
    }
}