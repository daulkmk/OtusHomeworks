using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private float timeBetweenSpawns = 1;
        [SerializeField] private EnemyManager _enemyManager;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
                _enemyManager.TryToSpawnEnemy();
            }
        }
    }
}