using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;
using UnityEngine;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class PushEffectHandler : BaseHandler<PushEffectEvent>
    {
        public PushEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(PushEffectEvent evt)
        {
            CoordinatesComponent coordinates = evt.Source.Get<CoordinatesComponent>();
            CoordinatesComponent targetCoordinates = evt.Target.Get<CoordinatesComponent>();

            Vector2Int direction = targetCoordinates.Value - coordinates.Value;

            EventBus.RaiseEvent(new ForceDirectionEvent(evt.Target, direction));
        }
    }
}