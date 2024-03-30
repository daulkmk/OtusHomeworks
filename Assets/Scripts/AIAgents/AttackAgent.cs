using System;
using UnityEngine;
using Zenject;

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

        [Inject] private IWeaponComponent _weapon;

        private Transform _target;
        private float _currentTime;

        public Func<bool> CanAttackDelegate;

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