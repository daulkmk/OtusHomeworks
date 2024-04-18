using UnityEngine;
using R3;

namespace ShootEmUp
{
    public interface IMoveAgent
    {
        ReadOnlyReactiveProperty<bool> IsReached { get; }
        void SetDestination(Vector2 endPoint);
    }
}