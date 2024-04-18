using System.Collections;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Level : MonoBehaviour, IStartGameListener, IPauseGameListener
    {
        [SerializeField] private float timeBetweenSpawns = 1;

        private Character _character;
        private IEnemyManager _enemyManager;

        private Coroutine _spawnLoopCoroutine = null;
        private bool _pauseSpawn = true;

        [Inject]
        private void Construct(Character character, IEnemyManager enemyManager)
        {
            _character = character;
            _enemyManager = enemyManager;
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);

                while (_pauseSpawn)
                    yield return null;

                _enemyManager.TryToSpawnEnemy(_character.GameObject.Transform);
            }
        }

        void IStartGameListener.OnGameStarting()
        {
            if (_spawnLoopCoroutine != null)
                StopCoroutine(_spawnLoopCoroutine);

            _spawnLoopCoroutine = StartCoroutine(SpawnLoop());
        }

        void IPauseGameListener.OnGamePaused(bool paused)
        {
            _pauseSpawn = paused;
        }
    }
}