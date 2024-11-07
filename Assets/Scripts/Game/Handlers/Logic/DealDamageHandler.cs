using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;

namespace Lessons.Game.Handlers.Logic
{
    [UsedImplicitly]
    public sealed class DealDamageHandler : BaseHandler<DealDamageEvent>
    {
        public DealDamageHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void HandleEvent(DealDamageEvent evt)
        {
            if (evt.Entity.TryGet(out DamageHandlerComponent damageHandler))
            {
                foreach (var damageEffect in damageHandler.DamageHandler.Effects)
                {
                    damageEffect.Source = null;
                    damageEffect.Target = evt.Entity;
                    damageEffect.Damage = evt.Damage;

                    EventBus.RaiseEvent(damageEffect);
                }
            }
        }
    }
}