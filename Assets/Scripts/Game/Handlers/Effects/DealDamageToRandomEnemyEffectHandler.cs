using System.Linq;
using Entities;
using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;
using Lessons.Game.Services;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class DealDamageToRandomEnemyEffectHandler : BaseHandler<DealDamageToRandomEnemyEffectEvent>
    {
        private readonly PlayersService _playersService;

        public DealDamageToRandomEnemyEffectHandler(EventBus eventBus, PlayersService playersService) : base(eventBus)
        {
            _playersService = playersService;
        }

        protected override void HandleEvent(DealDamageToRandomEnemyEffectEvent evt)
        {
            var enemiesList = _playersService.Enemies.ToList();
            enemiesList.Remove(evt.Target);

            if (enemiesList.Count == 0)
                return;

            IEntity rantomTarget = enemiesList[UnityEngine.Random.Range(0, enemiesList.Count)];
            int damage = evt.Damage;

            EventBus.RaiseEvent(new AbilityUsedEvent(evt.Source));
            EventBus.RaiseEvent(new DealDamageEvent(rantomTarget, damage));
        }
    }
}