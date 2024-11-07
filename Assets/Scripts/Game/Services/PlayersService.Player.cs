using System;
using System.Collections.Generic;
using Entities;
using Lessons.Entities.Common.Components;
using Lessons.Level;
using UI;

namespace Lessons.Game.Services
{
    public sealed partial class PlayersService
    {
        private class Player
        {
            private readonly HeroListView _heroListView;
            private readonly HeroesViewMap _heroesViewMap;
            private int _heroIndex = 0;

            public event Action<HeroView> OnHeroClicked
            {
                add => _heroListView.OnHeroClicked += value;
                remove => _heroListView.OnHeroClicked -= value;
            }

            public Player(HeroListView heroListView, HeroesViewMap heroesViewMap)
            {
                _heroListView = heroListView;
                _heroesViewMap = heroesViewMap;
            }

            public void NextHero()
            {
                _heroIndex++;

                int heroCount = _heroListView.GetViews().Count;

                for (int i = _heroIndex; i < heroCount; i++)
                {
                    if (CheckHeroIndex(i))
                        return;
                }

                for (int i = 0; i < _heroIndex; i++)
                {
                    if (CheckHeroIndex(i))
                        return;
                }

                _heroIndex = -1;

                bool CheckHeroIndex(int i)
                {
                    if (IsHeroAlive(_heroListView.GetView(i)))
                    {
                        _heroIndex = i;
                        return true;
                    }
                    return false;
                }
            }

            public MonoEntityBase GetHero()
            {
                if (_heroIndex == -1)
                    return null;

                if (!IsHeroAlive(_heroListView.GetView(_heroIndex)))
                    NextHero();

                if (_heroIndex == -1)
                    return null;

                var heroVeiw = _heroListView.GetView(_heroIndex);
                return _heroesViewMap.GetHeroByView(heroVeiw);
            }

            public IEnumerable<MonoEntityBase> GetAliveHeroes()
            {
                foreach (HeroView view in _heroListView.GetViews())
                {
                    if (IsHeroAlive(view))
                        yield return _heroesViewMap.GetHeroByView(view);
                }
            }

            private bool IsHeroAlive(HeroView heroView)
            {
                var hero = _heroesViewMap.GetHeroByView(heroView);
                return !hero.Get<DeathComponent>().IsDead;
            }
        }
    }
}