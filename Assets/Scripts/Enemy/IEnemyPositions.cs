using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyPositions
    {
        Vector3 RandomSpawnPosition();
        Vector3 RandomAttackPosition();
    }
}