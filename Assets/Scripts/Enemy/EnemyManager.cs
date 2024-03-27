using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _attackTarget;

        public void TryToSpawnEnemy()
        {
            if (!_enemySpawner.Initialized)
                _enemySpawner.Initialize();

            var enemy = _enemySpawner.Spawn();
            if (enemy == null)
                return;

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition;

            var attackPosition = _enemyPositions.RandomAttackPosition();

            enemy.SetTargets(attackPosition, _attackTarget);
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