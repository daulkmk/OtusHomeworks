using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IStartGameListener, IPauseGameListener
    {
        [SerializeField] private float timeBetweenSpawns = 1;

        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _attackTarget;

        private Coroutine _spawnLoopCoroutine = null;
        private bool _pauseSpawn = true;

        void IStartGameListener.OnGameStarting()
        {
            _enemySpawner.Initialize();

            if (_spawnLoopCoroutine != null)
                StopCoroutine(_spawnLoopCoroutine);

            _spawnLoopCoroutine = StartCoroutine(SpawnLoop());
        }

        void IPauseGameListener.OnGamePaused(bool paused)
        {
            _pauseSpawn = paused;
        }

        private IEnumerator SpawnLoop()
        {
            Debug.Log("[ENEMIES] start spawn");

            while (true)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);

                while (_pauseSpawn)
                    yield return null;

                var enemy = _enemySpawner.Spawn();
                if (enemy == null)
                    continue;

                var spawnPosition = _enemyPositions.RandomSpawnPosition();
                enemy.transform.position = spawnPosition;

                var attackPosition = _enemyPositions.RandomAttackPosition();

                enemy.SetTargets(attackPosition, _attackTarget);
                enemy.Reset();

                enemy.OnDeath += OnDestroyed;
            }
        }

        private void OnDestroyed(Character enemy)
        {
            enemy.OnDeath -= OnDestroyed;
            _enemySpawner.Despawn(enemy as AICharacter);
        }
    }
}