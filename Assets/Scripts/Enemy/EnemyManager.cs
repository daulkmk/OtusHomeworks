using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private float timeBetweenSpawns = 1;

        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private BulletSystem _bulletSystem;

        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _attackTarget;

        private void Awake()
        {
            _enemySpawner.Initialize(e => e.Initialize(_bulletSystem));
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);

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