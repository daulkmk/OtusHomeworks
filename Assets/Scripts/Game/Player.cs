using System;
using UnityEngine;

namespace Lessons.MetaGame.Inventory
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private int _maxHitPoints = 2;
        [SerializeField] private int _damage = 3;

        public event Action<float> OnSpeedChanged;
        public event Action<int> OnMaxHitPointsChanged;
        public event Action<int> OnDamageChanged;

        public float Speed 
        {
            get => _speed;
            set 
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnSpeedChanged?.Invoke(value);
                }
            }
        }
        public int MaxHitPoints
        {
            get => _maxHitPoints;
            set 
            {
                if (_maxHitPoints != value)
                {
                    _maxHitPoints = value;
                    OnMaxHitPointsChanged?.Invoke(value);
                }
            }
        }
        public int Damage
        {
            get => _damage;
            set 
            {
                if (_damage != value)
                {
                    _damage = value;
                    OnDamageChanged?.Invoke(value);
                }
            }
        }
    }
}