using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    public sealed class SelectDefaultTargetEffectHandler : BaseHandler<SelectDefaultTargetEffectEvent>
    {
        public SelectDefaultTargetEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(SelectDefaultTargetEffectEvent evt)
        {
            EventBus.RaiseEvent(new AttackEvent(evt.Source, evt.Target));
        }
    }
}