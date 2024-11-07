using System.Linq;
using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;
using Lessons.Game.Services;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class ApplyDamageToAllOtherHeroesEffectHandler : BaseHandler<ApplyDamageToAllOtherHeroesEffectEvent>
    {
        private readonly PlayersService _playersService;

        public ApplyDamageToAllOtherHeroesEffectHandler(EventBus eventBus, PlayersService playersService) : base(eventBus)
        {
            _playersService = playersService;
        }

        protected override void HandleEvent(ApplyDamageToAllOtherHeroesEffectEvent evt)
        {
            var allHeroesToDamage = _playersService.AliveHeroes.Where(x => x != evt.Target);

            EventBus.RaiseEvent(new AbilityUsedEvent(evt.Target));

            foreach (var hero in allHeroesToDamage)
            {
                EventBus.RaiseEvent(new DealDamageEvent(hero, evt.Damage));
            }
        }
    }
}