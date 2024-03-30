using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Level : MonoBehaviour, IStartGameListener, IPauseGameListener
    {
        [SerializeField] private float timeBetweenSpawns = 1;

        [Inject] private Character _character;
        [Inject] private IEnemyManager _enemyManager;

        private Coroutine _spawnLoopCoroutine = null;
        private bool _pauseSpawn = true;

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);

                while (_pauseSpawn)
                    yield return null;

                _enemyManager.TryToSpawnEnemy(_character.transform);
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