using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Lessons.Level;
using UI;

namespace Lessons.Game.Services
{
    public sealed partial class PlayersService
    {
        private readonly UIService _uiService;
        private readonly HeroesViewMap _heroesViewMap;
        private readonly List<Player> _players = new();

        private int _playerIndex = 0;

        public event Action<IEntity> OnEnemyClicked;

        public int PlayersCount => _players.Count;
        public MonoEntityBase PlayerHero => _players[_playerIndex].GetHero();

        public IEnumerable<IEntity> Enemies => _players[GetEnemyIndex()].GetAliveHeroes();
        public IEnumerable<IEntity> Allies => _players[_playerIndex].GetAliveHeroes();

        public IEnumerable<IEntity> AliveHeroes => Enemies.Concat(Allies);

        public PlayersService(UIService uiService, HeroesViewMap heroesViewMap)
        {
            _uiService = uiService;
            _heroesViewMap = heroesViewMap;

            _players.Add(new Player(_uiService.GetBluePlayer(), heroesViewMap));
            _players.Add(new Player(_uiService.GetRedPlayer(), heroesViewMap));
        }

        private int GetEnemyIndex() => NextIndex(_playerIndex);

        public void NextHero()
        {
            foreach (var player in _players)
                player.NextHero();
        }

        public void NextPlayer()
        {
            int enemyIndex = NextIndex(_playerIndex);
            _players[enemyIndex].OnHeroClicked -= OnEnemyHeroClicked;

            _playerIndex = enemyIndex;

            enemyIndex = NextIndex(_playerIndex);
            _players[enemyIndex].OnHeroClicked += OnEnemyHeroClicked;
        }

        private int NextIndex(int index)
        {
            index++;
            if (index >= _players.Count)
                index = 0;
            return index;
        }

        private void OnEnemyHeroClicked(HeroView view)
        {
            var hero = _heroesViewMap.GetHeroByView(view);
            OnEnemyClicked?.Invoke(hero);
        }
    }
}