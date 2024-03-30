using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public interface IEnemyManager
    {
        void TryToSpawnEnemy(Transform attackTarget);
    }

    public sealed class EnemyManager : IEnemyManager
    {
        private ISpawner<AICharacter> _enemySpawner;
        private IEnemyPositions _enemyPositions;

        public EnemyManager(ISpawner<AICharacter> spawner, IEnemyPositions positions)
        {
            _enemySpawner = spawner;
            _enemyPositions = positions;
        }

        public void TryToSpawnEnemy(Transform attackTarget)
        {
            if (!_enemySpawner.Initialized)
                _enemySpawner.Initialize();

            var enemy = _enemySpawner.Spawn();
            if (enemy == null)
                return;

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition;

            var attackPosition = _enemyPositions.RandomAttackPosition();

            enemy.SetTargets(attackPosition, attackTarget);
            enemy.Reset();

            enemy.OnDeath += OnDestroyed;
        }

        private void OnDestroyed(Character enemy)
        {
            enemy.OnDeath -= OnDestroyed;
            _enemySpawner.Despawn(enemy as AICharacter);
        }
    }
}