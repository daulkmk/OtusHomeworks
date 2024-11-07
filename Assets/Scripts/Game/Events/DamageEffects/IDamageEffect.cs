using Entities;

namespace Lessons.Game.Events.Effects
{
    public interface IDamageEffect : IEvent
    {
        public IEntity Source { get; set; }
        public IEntity Target { get; set; }
        public int Damage { get; set; }
    }
}