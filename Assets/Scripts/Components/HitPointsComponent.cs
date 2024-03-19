using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IHitPoints
    {
        event Action OnEmptyHP;

        bool IsHitPointsExists { get; }

        void Restore();
        void AddValue(int value);
    }

    public sealed class HitPointsComponent : MonoBehaviour, IHitPoints
    {
        [SerializeField] private int _maxHitPoints = 10;
        private int _hitPoints;

        public event Action OnEmptyHP;

        public bool IsHitPointsExists => _hitPoints > 0;

        public void Restore() => _hitPoints = _maxHitPoints;

        public void AddValue(int value)
        {
            if (_hitPoints <= 0)
                return;

            _hitPoints += value;

            if (_hitPoints > _maxHitPoints)
            {
                _hitPoints = _maxHitPoints;
            }
            else if (_hitPoints <= 0)
            {
                _hitPoints = 0;
                OnEmptyHP?.Invoke();
            }
        }
    }
}
