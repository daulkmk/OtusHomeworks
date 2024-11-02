using JetBrains.Annotations;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    [UsedImplicitly]
    public class CollideHandler : BaseHandler<CollideEvent>
    {
        public CollideHandler(EventBus eventBus) : base(eventBus)
        {
            
        }

        protected override void HandleEvent(CollideEvent evt)
        {
            EventBus.RaiseEvent(new DealDamageEvent(evt.Entity, 1));
            EventBus.RaiseEvent(new DealDamageEvent(evt.Target, 1));
        }
    }
}