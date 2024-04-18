using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletConfig
    {
        PhysicsLayer PhysicsLayer { get; }
        Color Color { get; }
        int Damage { get; }
        float Speed { get; }
    }
}