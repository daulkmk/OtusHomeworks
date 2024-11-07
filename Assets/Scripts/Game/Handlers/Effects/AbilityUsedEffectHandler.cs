using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class AbilityUsedEffectHandler : BaseHandler<AbilityUsedEffectEvent>
    {
        public AbilityUsedEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(AbilityUsedEffectEvent evt)
        {
            EventBus.RaiseEvent(new AbilityUsedEvent(evt.Source));
        }
    }
}