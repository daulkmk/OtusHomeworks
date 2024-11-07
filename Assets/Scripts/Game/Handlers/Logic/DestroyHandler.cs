using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    [UsedImplicitly]
    public sealed class DestroyHandler : BaseHandler<DestroyEvent>
    {
        
        public DestroyHandler(EventBus eventBus) : base(eventBus)
        {
        }
        
        protected override void HandleEvent(DestroyEvent evt)
        {
            if (evt.Entity.TryGet(out DeathComponent deathComponent))
            {
                deathComponent.Die();
            }
        }
    }
}