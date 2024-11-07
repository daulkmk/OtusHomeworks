using Entities;

namespace Lessons.Game.Events
{
    public readonly struct SelectHeroEvent : IEvent
    {
        public readonly IEntity Selected;
        public readonly IEntity Previous;

        public SelectHeroEvent(IEntity selected, IEntity previous)
        {
            Selected = selected;
            Previous = previous;
        }
    }
}