using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    [UsedImplicitly]
    public sealed class UseShieldHandler : BaseHandler<UseShieldEvent>
    {
        public UseShieldHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(UseShieldEvent evt)
        {
            if (evt.Entity.TryGet(out ShieldComponent shieldComponent))
            {
                if (shieldComponent.AttacksToBlock.Value > 0)
                {
                    shieldComponent.AttacksToBlock.Value--;
                }
            }
        }
    }
}