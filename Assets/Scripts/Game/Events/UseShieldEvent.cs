using Entities;

namespace Lessons.Game.Events
{
    public readonly struct UseShieldEvent : IEvent
    {
        public readonly IEntity Entity;
        public readonly IEntity Attacker;
        public readonly int DamageToBlock;

        public UseShieldEvent(IEntity entity, IEntity attacker, int damageToBlock)
        {
            Entity = entity;
            Attacker = attacker;
            DamageToBlock = damageToBlock;
        }
    }
}