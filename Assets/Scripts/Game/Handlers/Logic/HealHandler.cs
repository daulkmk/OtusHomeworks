using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    [UsedImplicitly]
    public sealed class HealHandler : BaseHandler<HealEvent>
    {
        public HealHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(HealEvent evt)
        {
            if (!evt.Entity.TryGet(out HitPointsComponent hitPointsComponent))
            {
                return;
            }

            hitPointsComponent.Value += evt.Value;
        }
    }
}