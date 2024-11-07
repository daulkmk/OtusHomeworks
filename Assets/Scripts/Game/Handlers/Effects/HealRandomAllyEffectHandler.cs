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
    public sealed class HealRandomAllyEffectHandler : BaseHandler<HealRandomAllyEffectEvent>
    {
        private readonly PlayersService _playersService;

        public HealRandomAllyEffectHandler(EventBus eventBus, PlayersService playersService) : base(eventBus)
        {
            _playersService = playersService;
        }

        protected override void HandleEvent(HealRandomAllyEffectEvent evt)
        {
            var alliesToHeal = _playersService.Allies.Where(CanHeal).ToList();
            if (alliesToHeal.Count == 0)
                return;

            var allyToHeal = alliesToHeal[UnityEngine.Random.Range(0, alliesToHeal.Count)];

            EventBus.RaiseEvent(new AbilityUsedEvent(evt.Source));
            EventBus.RaiseEvent(new HealEvent(allyToHeal, evt.HealValue));

            bool CanHeal(IEntity entity)
            {
                if (entity == evt.Source)
                    return false;

                if (!entity.TryGet(out HitPointsComponent hitPoints))
                    return false;

                return hitPoints.Value < hitPoints.MaxHitPoints;
            }
        }
    }
}