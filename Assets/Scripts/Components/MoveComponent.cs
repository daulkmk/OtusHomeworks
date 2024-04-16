using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveComponent
    {
        bool CanMove { get; }
        void FixedMove(Vector2 direction, float deltaTime);
    }

    public sealed class MoveComponent : MonoBehaviour, IMoveComponent
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Func<bool> CanMoveDelegate;

        public bool CanMove => CanMoveDelegate();

        public void FixedMove(Vector2 direction, float deltaTime)
        {
            if (CanMove == true)
            {
                var nextPosition = _rigidbody2D.position + _speed * deltaTime * direction.normalized;
                _rigidbody2D.MovePosition(nextPosition);
            }
        }
    }
}