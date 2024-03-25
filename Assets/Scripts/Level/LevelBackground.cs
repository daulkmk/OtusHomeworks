using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IUpdatable
    {
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        private float _positionX;
        private float _positionZ;

        private void Awake()
        {
            var position = transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        void IUpdatable.OnUpdate(float deltaTime)
        {
            if (transform.position.y <= _endPositionY)
            {
                transform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            transform.position -= new Vector3(
                _positionX,
                _movingSpeedY * deltaTime,
                _positionZ
            );
        }
    }
}