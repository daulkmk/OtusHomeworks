using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    public sealed class PlayerSelectedAttackTargetEventHandler : BaseHandler<PlayerSelectedAttackTargetEvent>
    {
        public PlayerSelectedAttackTargetEventHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(PlayerSelectedAttackTargetEvent evt)
        {
            if (!evt.Entity.TryGet(out TargetSelectorComponent targetSelector))
                return;

            var behaviour = targetSelector.Effect;

            behaviour.Source = evt.Entity;
            behaviour.Target = evt.Target;

            EventBus.RaiseEvent(behaviour);
        }
    }
}