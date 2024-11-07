using Entities;

namespace Lessons.Game.Events
{
    public readonly struct LowHealthEvent : IEvent
    {
        public readonly IEntity Entity;

        public LowHealthEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}