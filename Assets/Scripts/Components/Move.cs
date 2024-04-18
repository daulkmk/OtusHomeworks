using R3;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : IMove
    {
        private readonly float _speed = 5.0f;
        private readonly Rigidbody2D _rigidbody2D;

        public ReactiveProperty<bool> CanMove { get; }

        public MoveComponent(float speed, Rigidbody2D rigidbody2D)
        {
            CanMove = new ReactiveProperty<bool>(false);

            _speed = speed;
            _rigidbody2D = rigidbody2D;
        }

        public void FixedMove(Vector2 direction, float deltaTime)
        {
            if (CanMove.Value)
            {
                var nextPosition = _rigidbody2D.position + _speed * deltaTime * direction.normalized;
                _rigidbody2D.MovePosition(nextPosition);
            }
        }
    }
}