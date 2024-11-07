using Entities;

namespace Lessons.Game.Events
{
    public readonly struct PlayerSelectedAttackTargetEvent : IEvent
    {
        public readonly IEntity Entity;
        public readonly IEntity Target;

        public PlayerSelectedAttackTargetEvent(IEntity entity, IEntity target)
        {
            Entity = entity;
            Target = target;
        }
    }
}