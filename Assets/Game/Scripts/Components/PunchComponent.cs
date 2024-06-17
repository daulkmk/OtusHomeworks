using System;
using System.Collections.Generic;
using System.Threading;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class PunchComponent : IDisposable
    {
        public AtomicEvent AttackRequest = new();
        public AtomicEvent AttackAction = new();
        public AtomicEvent AttackEvent = new();

        public AtomicFunction<bool> CanAttack = new();
        public IAtomicValueObservable<bool> IsTargetInRange => _isTargetInRange;

        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isOnCooldown = false;
        [SerializeField] private bool _canAttack = true;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRadius = 1f;
        [SerializeField] private int _damage = 1;


        [ShowInInspector, ReadOnly]
        private float _cooldownTimer;

        private TriggerColliderEvents _triggerColliderEvents;

        private readonly CompositeCondition _condition = new();
        private readonly AtomicVariable<bool> _isTargetInRange = new();
        private readonly List<IAtomicEntity> _targetsInRange = new();

        public void Construct()
        {
            AttackAction.Subscribe(OnAttackAction);

            CanAttack.Compose(() => _canAttack && !_isOnCooldown && _condition.IsTrue());

            _triggerColliderEvents = _attackPoint.gameObject.AddComponent<TriggerColliderEvents>();
            _triggerColliderEvents.OnTriggerEntered += OnTriggerEnter;
            _triggerColliderEvents.OnTriggerExited += OnTriggerExit;
        }

        public void Dispose()
        {
            if (_triggerColliderEvents != null)
            {
                _triggerColliderEvents.OnTriggerEntered -= OnTriggerEnter;
                _triggerColliderEvents.OnTriggerExited -= OnTriggerExit;
            }
        }

        public void Update(float deltaTime)
        {
            if (_isOnCooldown)
            {
                _cooldownTimer -= deltaTime;

                if (_cooldownTimer <= 0)
                {
                    _isOnCooldown = false;
                }
            }
        }

        private void OnAttackAction()
        {
            Debug.Log("OnAttackAction " + CanAttack.Value);            
            Punch();
        }

        public void Punch()
        {
            if (!CanAttack.Value)
            {
                return;
            }

            //var colliders = Physics.OverlapSphere(_attackPoint.position, _attackRadius);

            foreach (var atomicEntity in _targetsInRange)
            {
                var action = atomicEntity.Get<IAtomicAction<int>>(LifeAPI.TakeDamageAction);
                action.Invoke(_damage);
            }

            _cooldownTimer = _reloadTime;
            _isOnCooldown = true;

            AttackEvent.Invoke();
            Debug.Log("Punch!");
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out IAtomicEntity atomicEntity))
            {
                if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TakeDamageAction, out _))
                {
                    _targetsInRange.Add(atomicEntity);

                    if (_targetsInRange.Count == 1)
                        _isTargetInRange.Value = true;
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out IAtomicEntity atomicEntity))
            {
                _targetsInRange.Remove(atomicEntity);

                if (_targetsInRange.Count == 0)
                    _isTargetInRange.Value = false;
            }
        }
    }
}