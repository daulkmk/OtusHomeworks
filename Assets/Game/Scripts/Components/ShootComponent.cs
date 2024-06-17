using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class ShootComponent
    {
        public AtomicEvent ShootRequest;
        public AtomicEvent ShootAction;
        public AtomicEvent ShootEvent;

        public AtomicFunction<bool> CanFire;
        public IAtomicValueObservable<Vector3> FirePointPosition => _firePosition;
        
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isReloading;
        [SerializeField] private bool _canFire;
        [SerializeField] private AtomicEntity _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        
        [ShowInInspector, ReadOnly]
        private float _reloadTimer;
        
        private readonly CompositeCondition _condition = new();
        private readonly AtomicVariable<Vector3> _firePosition = new();

        public void Compose()
        {
            ShootAction?.Subscribe(Shoot);
            CanFire.Compose(()=> _canFire && !_isReloading && _condition.IsTrue());
        }

        public void Update(float deltaTime)
        {
            if (_isReloading)
            {
                _reloadTimer -= deltaTime;

                if (_reloadTimer <= 0)
                {
                    _isReloading = false;
                }
            }

            _firePosition.Value = _firePoint.position;
        }
        
        public void Shoot()
        {
            if (!CanFire.Value)
            {
                return;
            }

            var bullet = GameObject.Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.gameObject.SetActive(true);

            if (bullet.TryGetVariable<Vector3>(MoveAPI.MoveDirection, out var moveDirection))
            {
                moveDirection.Value = _firePoint.forward;
            }
            
            _reloadTimer = _reloadTime;
            _isReloading = true;
            
            ShootEvent.Invoke();
            Debug.Log("Fire!");
        }
        
        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}