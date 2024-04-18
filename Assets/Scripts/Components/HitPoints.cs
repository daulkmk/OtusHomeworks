using System;

namespace ShootEmUp
{
    public interface IHitPoints
    {
        event Action OnEmptyHP;
        event Action OnRestoreHP;

        bool IsHitPointsExists { get; }

        void Restore();
        void AddValue(int value);
    }

    public sealed class HitPoints : IHitPoints
    {
        private readonly int _maxHitPoints = 10;
        private int _hitPoints;

        public event Action OnEmptyHP;
        public event Action OnRestoreHP;

        public bool IsHitPointsExists => _hitPoints > 0;

        public HitPoints(int maxHitPoints)
        {
            _maxHitPoints = maxHitPoints;
            _hitPoints = _maxHitPoints;
        }

        public void Restore()
        {
            _hitPoints = _maxHitPoints;
            OnRestoreHP?.Invoke();
        }

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
