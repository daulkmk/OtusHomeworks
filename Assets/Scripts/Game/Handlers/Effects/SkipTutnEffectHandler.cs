using Entities;
using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class SkipTutnEffectHandler : BaseHandler<SkipTutnEffectEvent>
    {
        public SkipTutnEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(SkipTutnEffectEvent evt)
        {
            if (evt.Target.TryGet(out SkipTurnComponent _))
                return;

            if (evt.Target is not MonoEntityBase entityBase)
                return;

            entityBase.Add(new SkipTurnComponent());
        }
    }
}