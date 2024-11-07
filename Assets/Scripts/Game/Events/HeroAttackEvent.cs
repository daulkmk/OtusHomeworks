using Entities;

namespace Lessons.Game.Events
{
    public readonly struct HeroAttackEvent : IEvent
    {
        public readonly IEntity Attacker;
        public readonly IEntity Target;

        public HeroAttackEvent(IEntity entity, IEntity target)
        {
            Attacker = entity;
            Target = target;
        }
    }
}