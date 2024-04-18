using R3;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMove
    {
        ReactiveProperty<bool> CanMove { get; }
        void FixedMove(Vector2 direction, float deltaTime);
    }
}