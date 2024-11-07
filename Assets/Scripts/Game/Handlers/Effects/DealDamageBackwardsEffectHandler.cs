using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class DealDamageBackwardsEffectHandler : BaseHandler<DealDamageBackwardsEffectEvent>
    {
        public DealDamageBackwardsEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(DealDamageBackwardsEffectEvent evt)
        {
            if (evt.Target.TryGet(out StatsComponent statsComponent))
            {
                int damage = statsComponent.Strength;
                EventBus.RaiseEvent(new DealDamageEvent(evt.Source, damage));
            }
        }
    }
}