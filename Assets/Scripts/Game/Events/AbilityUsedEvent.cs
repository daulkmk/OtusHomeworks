using Entities;

namespace Lessons.Game.Events
{
    public readonly struct AbilityUsedEvent : IEvent
    {
        public readonly IEntity Entity;

        public AbilityUsedEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}