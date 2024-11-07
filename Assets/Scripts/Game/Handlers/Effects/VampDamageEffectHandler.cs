using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class VampDamageEffectHandler : BaseHandler<VampDamageEffectEvent>
    {
        public VampDamageEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(VampDamageEffectEvent evt)
        {
            if (evt.Source.TryGet(out StatsComponent statsComponent))
            {
                int damage = statsComponent.Strength;

                EventBus.RaiseEvent(new AbilityUsedEvent(evt.Source));
                EventBus.RaiseEvent(new HealEvent(evt.Source, damage));
            }
        }
    }
}