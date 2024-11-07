using System.Collections.Generic;
using Entities;
using UI;

namespace Lessons.Level
{
    public sealed class HeroesViewMap
    {
        private readonly Dictionary<HeroView, MonoEntityBase> _enitytyByView = new();

        public void AddHero(MonoEntityBase entity, HeroView view)
        {
            _enitytyByView[view] = entity;
        }

        public MonoEntityBase GetHeroByView(HeroView view)
        {
            return _enitytyByView[view];
        }

        public HeroView GetViewByHero(IEntity hero)
        {
            foreach (var kvp in _enitytyByView)
            {
                if (ReferenceEquals(kvp.Value, hero))
                    return kvp.Key;
            }
            return null;
        }
    }
}