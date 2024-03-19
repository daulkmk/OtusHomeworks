using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IAttackAgent
    {
        void SetTarget(Transform target);
        void Reset();
    }

    public sealed class AttackAgent : MonoBehaviour, IAttackAgent
    {
        [SerializeField] private float countdown;

        private IWeaponComponent _weapon;

        private Transform _target;
        private float _currentTime;

        public Func<bool> CanAttackDelegate;

        public void Initialize(IWeaponComponent weapon)
        {
            _weapon = weapon;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        private void Update()
        {
            if (!CanAttackDelegate())
                return;
            
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += countdown;
            }
        }

        private void Fire()
        {
            _weapon.Fire(_target.position);
        }
    }
}