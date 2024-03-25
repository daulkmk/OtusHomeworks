using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : Spawner<Bullet>
    {
        [SerializeField] private GameManager _gameManager;

        protected override void OnObjectSpawned(Bullet obj)
        {
            _gameManager.AddAllListeners(obj.gameObject);
        }
    }
}