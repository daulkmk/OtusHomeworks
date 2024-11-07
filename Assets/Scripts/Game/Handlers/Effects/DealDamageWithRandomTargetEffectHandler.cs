using System.Linq;
using Entities;
using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;
using Lessons.Game.Services;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class DealDamageWithRandomTargetEffectHandler : BaseHandler<DealDamageWithRandomTargetEffectEvent>
    {
        private readonly PlayersService _playersService;

        public DealDamageWithRandomTargetEffectHandler(EventBus eventBus, PlayersService playersService) : base(eventBus)
        {
            _playersService = playersService;
        }

        protected override void HandleEvent(DealDamageWithRandomTargetEffectEvent evt)
        {
            IEntity target = evt.Target;

            bool randomizeTarget = UnityEngine.Random.value < evt.HintRandomTargetChance;
            if (randomizeTarget)
            {
                var enemiesList = _playersService.Enemies.ToList();
                target = enemiesList[UnityEngine.Random.Range(0, enemiesList.Count)];

                EventBus.RaiseEvent(new AbilityUsedEvent(evt.Source));
            }

            if (evt.Source.TryGet(out StatsComponent statsComponent))
            {
                int damage = statsComponent.Strength;
                EventBus.RaiseEvent(new DealDamageEvent(target, damage));
            }
        }
    }
}