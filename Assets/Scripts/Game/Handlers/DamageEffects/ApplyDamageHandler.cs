using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class ApplyDamageEffectHandler : BaseHandler<ApplyDamageEffectEvent>
    {
        public ApplyDamageEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(ApplyDamageEffectEvent evt)
        {
            if (evt.Target.TryGet(out HitPointsComponent hitPointsComponent) && hitPointsComponent.Value > 0)
            {
                hitPointsComponent.Value -= evt.Damage;

                if (hitPointsComponent.Value <= 0)
                {
                    EventBus.RaiseEvent(new DestroyEvent(evt.Target));
                }
                else
                {
                    float hpPercentRemaining = hitPointsComponent.Value / (float)hitPointsComponent.MaxHitPoints * 100;

                    if (hpPercentRemaining < 20)
                    {
                        EventBus.RaiseEvent(new LowHealthEvent(evt.Target));
                    }
                }
            }
        }
    }
}