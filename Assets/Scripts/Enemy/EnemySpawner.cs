using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner : Spawner<AICharacter>
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private GameManager _gameManager;

        protected override void OnObjectSpawned(AICharacter obj)
        {
            _gameManager.AddAllListeners(obj.gameObject);

            obj.Initialize(_bulletSystem);
        }
    }
}