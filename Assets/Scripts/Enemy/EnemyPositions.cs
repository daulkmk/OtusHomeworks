using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;

        [SerializeField] private Transform[] _attackPositions;

        private readonly List<Transform> _unusedAttackPositions = new List<Transform>();
        private readonly List<Transform> _unusedSpawnPositions = new List<Transform>();

        public Vector3 RandomSpawnPosition() => RandomPosition(_spawnPositions, _unusedSpawnPositions);
        public Vector3 RandomAttackPosition() => RandomPosition(_attackPositions, _unusedAttackPositions);

        private Vector3 RandomPosition(Transform[] source, List<Transform> unusedBuffer)
        {
            if (unusedBuffer.Count == 0)
                unusedBuffer.AddRange(source);

            var trPosition = unusedBuffer.GetRandomItem();
            unusedBuffer.Remove(trPosition);

            return trPosition.position;
        }
    }
}