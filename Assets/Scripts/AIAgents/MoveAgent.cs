using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveAgent
    {
        bool IsReached { get; }
        void SetDestination(Vector2 endPoint);
    }

    public sealed class MoveAgent : MonoBehaviour, IMoveAgent
    {
        [SerializeField] private float _destinationReachedDistance = 0.25f;

        private IMoveComponent _moveComponent;

        private Vector2 _destination;
        private bool _isReached;

        public bool IsReached => _isReached;

        public void Initialize(IMoveComponent move)
        {
            _moveComponent = move;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached || !_moveComponent.CanMove)
                return;
            
            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= _destinationReachedDistance)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized;
            _moveComponent.FixedMove(direction);
        }
    }
}