using Entities;

namespace Lessons.Game.Events
{
    public readonly struct HealEvent : IEvent
    {
        public readonly IEntity Entity;
        public readonly int Value;

        public HealEvent(IEntity entity, int value)
        {
            Entity = entity;
            Value = value;
        }
    }
}