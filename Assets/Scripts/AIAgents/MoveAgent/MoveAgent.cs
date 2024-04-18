using UnityEngine;
using R3;

namespace ShootEmUp
{
    public sealed class MoveAgent : IMoveAgent, IFixedUpdatable
    {
        private readonly float _destinationReachedDistance = 0.25f;

        private readonly IMove _moveComponent;
        private readonly ITransform _transform;

        private Vector2 _destination;

        public ReadOnlyReactiveProperty<bool> IsReached => _isReached;
        public ReactiveProperty<bool> _isReached;

        public MoveAgent(IMove moveComponent, ITransform transform, float destinationReachedDistance)
        {
            _isReached = new ReactiveProperty<bool>(false);

            _destinationReachedDistance = destinationReachedDistance;
            _moveComponent = moveComponent;
            _transform = transform;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached.Value = false;
        }

        void IFixedUpdatable.OnFixedUpdate(float deltaTime)
        {
            if (_isReached.Value || !_moveComponent.CanMove.Value)
                return;
            
            var vector = _destination - (Vector2)_transform.Position;
            if (vector.magnitude <= _destinationReachedDistance)
            {
                _isReached.Value = true;
                return;
            }

            var direction = vector.normalized;
            _moveComponent.FixedMove(direction, deltaTime);
        }
    }
}