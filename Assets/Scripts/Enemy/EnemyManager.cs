namespace ShootEmUp
{
    public sealed class EnemyManager : IEnemyManager
    {
        private readonly ISpawner<AICharacter> _enemySpawner;
        private readonly IEnemyPositions _enemyPositions;

        public EnemyManager(ISpawner<AICharacter> spawner, IEnemyPositions positions)
        {
            _enemySpawner = spawner;
            _enemyPositions = positions;
        }

        public void TryToSpawnEnemy(ITransform attackTarget)
        {
            if (!_enemySpawner.Initialized)
                _enemySpawner.Initialize();

            var enemy = _enemySpawner.Spawn();
            if (enemy == null)
                return;

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.SetPosition(spawnPosition);

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