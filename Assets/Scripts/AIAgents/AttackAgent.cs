using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IAttackAgent
    {
        void SetTarget(Transform target);
        void Reset();
    }

    public sealed class AttackAgent : MonoBehaviour, IAttackAgent, IUpdatable
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

        void IUpdatable.OnUpdate(float deltaTime)
        {
            if (!isActiveAndEnabled || !CanAttackDelegate())
                return;
            
            _currentTime -= deltaTime;
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