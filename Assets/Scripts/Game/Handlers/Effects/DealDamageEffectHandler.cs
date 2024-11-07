using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class DealDamageEffectHandler : BaseHandler<DealDamageEffectEvent>
    {
        public DealDamageEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(DealDamageEffectEvent evt)
        {
            if (evt.Source.TryGet(out StatsComponent statsComponent))
            {
                int damage = statsComponent.Strength;
                EventBus.RaiseEvent(new DealDamageEvent(evt.Target, damage));
            }
        }
    }
}