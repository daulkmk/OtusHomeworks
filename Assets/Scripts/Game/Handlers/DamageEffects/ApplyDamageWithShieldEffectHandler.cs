using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class ApplyDamageWithShieldEffectHandler : BaseHandler<ApplyDamageWithShieldEffectEvent>
    {
        public ApplyDamageWithShieldEffectHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(ApplyDamageWithShieldEffectEvent evt)
        {
            if (evt.Target.TryGet(out ShieldComponent shield))
            {
                if (shield.AttacksToBlock.Value > 0)
                {
                    UnityEngine.Debug.Log("USE SHIELD");

                    EventBus.RaiseEvent(new AbilityUsedEvent(evt.Target));
                    EventBus.RaiseEvent(new UseShieldEvent(evt.Target, evt.Source, evt.Damage));
                }
                else
                {
                    EventBus.RaiseEvent(new ApplyDamageEffectEvent()
                    {
                        Target = evt.Target,
                        Source = evt.Source,
                        Damage = evt.Damage
                    });
                }
            }
        }
    }
}